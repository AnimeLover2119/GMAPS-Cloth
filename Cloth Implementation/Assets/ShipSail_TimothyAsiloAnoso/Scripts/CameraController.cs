using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public Transform ship;
    public float rotationSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RotateCamera();
        ZoomCamera();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // calculate the desired rotation angles
            float XAngle = mouseX * rotationSpeed * Time.deltaTime;
            float YAngle = mouseY * rotationSpeed * Time.deltaTime;

            // Calculate the desired position in a circular orbit around the ship
            Vector3 origin = transform.position - ship.position;
            Quaternion XRotation = Quaternion.Euler(0f, XAngle, 0f);
            Quaternion YRotation = Quaternion.Euler(-YAngle, 0f, 0f);

            // calculating a new offset vector based on mouse movenemt
            Vector3 offset = YRotation * (XRotation * origin);

            // adjust camera position based on calculated offset vector
            transform.position = ship.position + offset;

            //set camera transform to keep its rotation facing the ship
            transform.LookAt(ship.position);
        }
    }

    void ZoomCamera()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // adjust the zoom based on the scroll wheel input, using a clamp to prevent the camera from zooming to far in and out
        float newZoom = Mathf.Clamp(Vector3.Distance(transform.position, ship.position) - scrollWheel * zoomSpeed, minZoom, maxZoom);

        // calculate the new camera position by scaling the directional vector between the camera and ship via scalar newZoom
        Vector3 newPosition = ship.position + (transform.position - ship.position).normalized * newZoom;

        // apply the new position
        transform.position = newPosition;
    }
}
