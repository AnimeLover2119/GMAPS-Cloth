using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastController : MonoBehaviour
{

    public float rotationSpeed = 55f;
    public float maxRotation = 90f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Rotate(-rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.E)) 
        {
            Rotate(rotationSpeed);
        }
    }

    void Rotate(float rotationSpeed)
    {
        float angle = rotationSpeed * Time.deltaTime;

        float currentRotation = transform.eulerAngles.y;

        // Calculate the new rotation after applying the angle
        float newRotation = currentRotation + angle;

        // Ensure the new rotation is within the specified range
        if (newRotation > 180f)
        {
            newRotation -= 360f;
        }

        newRotation = Mathf.Clamp(newRotation, -maxRotation, maxRotation);

        // Set the new rotation
        transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }
}
