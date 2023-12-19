using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public GameObject sail;
    public TextMeshProUGUI alignmentText;
    public float baseWindForce = 10f;
    public float windAreaSize = 5f;
    public float flutterIntensity = 1f;
    public float flutterFrequency = 1f;
    public float amplitude = 1.0f;    
    public float frequency = 1.0f;
    public float rotationSpeed = 45f;


    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        movePoint();
    }

    // applying wind in fixedUpdate for a more consistent physics simulation of cloth
    void FixedUpdate()
    {
        blowWind();
        oscillateY();
    }


    void blowWind()
    {
        // assign wind direction to the vector between the WindZone and sail objects
        Vector3 windDirection = sail.transform.position - transform.position;
        windDirection.Normalize();

        Cloth sailCloth = sail.GetComponent<Cloth>();
        
        if (sailCloth != null )
        {
            // apply a general wind force in the windDirection vector
            sailCloth.externalAcceleration = windDirection * baseWindForce;

            Vector3 sailNormal =  - sail.transform.right;
            sailNormal.Normalize();

            if (baseWindForce > 0)
            {
                // estimate the alignment of the sail with the wind by calculating the dot product between the wind force and the normal of the sail object (going toward the front of the ship)
                // i also clamped the alignment to be between 0 and 1 even though dot product can be negative,
                // as this assumes that the percentage will represent how much forward force could be applied to the ship
                float sailAlignment = Mathf.Clamp01(Vector3.Dot(windDirection, sailNormal));


                // create a "fluttering" effect for wind force, by randomly applying a random force in the general direction of the wind
                // to do this, i implemented the use of perlin noise due to its pseudo-randomness
                // in this implementation the fluttering can be adjusted by changing the frequency and intensity values to simulate stronger/rougher wind patterns
                Vector3 flutterForce = new Vector3(
                    // as perlin noise generates a value from 0 - 1, i subtract 0.5f to allow negative values
                    // this can help in varying the wind force to appear more symmetric and natural in nature
                    Mathf.PerlinNoise(Time.time * flutterFrequency, 0f) - 0.5f,
                    Mathf.PerlinNoise(0f, Time.time * flutterFrequency) - 0.5f,
                    Mathf.PerlinNoise(0f, Time.time * flutterFrequency) - 0.5f
                ) * flutterIntensity;

                // add the new flutterForce vector to the cloth component's external accelaration 
                sailCloth.externalAcceleration += flutterForce;

                // to add another element of randomness to the wind, i make use of a vector that points to a random point of a unit sphere
                // this is then multiplied to the baseWindForce and another arbitrary float (in this case 0.15f
                Vector3 windAreaForce = Random.onUnitSphere * baseWindForce * 0.15f; // Adjust the multiplier as needed

                // the windAreaForce is also added to apply as a force on the sail's cloth component
                sailCloth.externalAcceleration += windAreaForce;

                // a debug ray to depict the direction of the wind in scene view
                Debug.DrawRay(transform.position, windDirection * 8f, Color.blue);

                // a percentage representation of the alignment of the sail relative to the wind direction
                alignmentText.text = "Wind Alignment: " + (sailAlignment * 100f).ToString("F1") + "%";
            }
            else
            {
                // display "No Wind" when wind force is set to 0 in runtime
                alignmentText.text = "Wind Alignment: No Wind!";
            }
        }
    }


    // this method is to oscillate the position of WindZone to vary the vertical direction of the wind
    // this is done by simply translating the y position of the camera to the shape of a Sine wave
    // simulates the "wavier" nature of wind at sea
    void oscillateY()
    {
        
        if (baseWindForce > 0)
        {
            float yOffset = amplitude * Mathf.Sin(Time.time * frequency);

            Vector3 newYPos = initialPos + new Vector3(0f, yOffset, 0f);

            transform.position = new Vector3(transform.position.x, newYPos.y, transform.position.z);
        }
    }

    // this method allows the WindZone to rotate around the sail to showcase the functionality of the cloth component from different angles
    void movePoint()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        // calculate the vector from the sail to the GameObject
        Vector3 origin = (transform.position - sail.transform.position);

        // calculate the desired position in a circular orbit around the sail
        float angle = horizontalInput * rotationSpeed * Time.deltaTime;
        Vector3 offset = Quaternion.Euler(0f, angle, 0f) * origin;

        // Set the new position of the GameObject, preserving the y-axis position
        transform.position = new Vector3(sail.transform.position.x + offset.x,transform.position.y,sail.transform.position.z + offset.z);

        // rotate WindZone gameObject to maintain its rotation pointed at the sail
        transform.LookAt(sail.transform);

    }

}
