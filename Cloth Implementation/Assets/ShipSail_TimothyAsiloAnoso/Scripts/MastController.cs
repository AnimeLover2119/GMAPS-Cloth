using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastController : MonoBehaviour
{

    public float rotationSpeed = 55f;
    private float maxRotation = 90f;


    // Update is called once per frame
    void Update()
    {
        //call method to Rotate the mast when pressing Q and E 
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

        // ensure the new rotation is within the specified range
        // this is done as rotating via Euler angles can exceed the 90 threshold when rotating in the negative direction (i.e -90 degrees = 270 degrees)
        // subtracting 360 ensures that the rotation angle will be - when rotating anti-clockwise
        if (newRotation > 180f)
        {
            newRotation -= 360f;
        }

        // setting the max rotating by clamping the rotation angle between -90 and 90 degrees
        newRotation = Mathf.Clamp(newRotation, -maxRotation, maxRotation);

        // Set the new rotation
        transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }
}
