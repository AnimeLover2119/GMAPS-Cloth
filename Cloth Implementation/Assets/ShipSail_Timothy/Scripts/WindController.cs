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

    void FixedUpdate()
    {

        blowWind();
        oscillateY();
    }


    void blowWind()
    {
        Vector3 windDirection = sail.transform.position - transform.position;
        windDirection.Normalize();




        Cloth sailCloth = sail.GetComponent<Cloth>();
        
        if (sailCloth != null )
        {
            // Apply base wind force
            sailCloth.externalAcceleration = windDirection * baseWindForce;

            Vector3 sailNormal =  - sail.transform.right;
            sailNormal.Normalize();

            if (baseWindForce > 0)
            {
                // Estimate the alignment of the sail with the wind
                float alignmentPercentage = Mathf.Clamp01(Vector3.Dot(windDirection, sailNormal));


                // Introduce fluttering effect using Perlin noise
                Vector3 flutterForce = new Vector3(
                    Mathf.PerlinNoise(Time.time * flutterFrequency, 0f) - 0.5f,
                    Mathf.PerlinNoise(0f, Time.time * flutterFrequency) - 0.5f,
                    Mathf.PerlinNoise(0f, Time.time * flutterFrequency) - 0.5f
                ) * flutterIntensity;

                // Apply fluttering force
                sailCloth.externalAcceleration += flutterForce;

                // Introduce wind area effect
                Vector3 windAreaForce = Random.onUnitSphere * baseWindForce * 0.15f; // Adjust the multiplier as needed

                Vector3 randomWindModifiers = flutterForce + windAreaForce;

                // Apply wind area force
                sailCloth.externalAcceleration += windAreaForce;

                Debug.DrawRay(transform.position, windDirection * 8f, Color.blue);



                alignmentText.text = "Wind Alignment: " + (alignmentPercentage * 100f).ToString("F1") + "%";
            }
            else
            {
                alignmentText.text = "Wind Alignment: No Wind!";
            }
        }
    }

    void oscillateY()
    {
        if (baseWindForce > 0)
        {
            float yOffset = amplitude * Mathf.Sin(Time.time * frequency);

            Vector3 newYPos = initialPos + new Vector3(0f, yOffset, 0f);

            transform.position = new Vector3(transform.position.x, newYPos.y, transform.position.z);
        }
    }


    void movePoint()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the relative position from the sail to the GameObject
        Vector3 origin = (transform.position - sail.transform.position);

        // Calculate the desired position in a circular orbit around the sail
        float angle = horizontalInput * rotationSpeed * Time.deltaTime;
        Vector3 offset = Quaternion.Euler(0f, angle, 0f) * origin;

        // Set the new position of the GameObject, preserving the y-axis position
        transform.position = new Vector3(sail.transform.position.x + offset.x,transform.position.y,sail.transform.position.z + offset.z);

        transform.LookAt(sail.transform);

    }

}
