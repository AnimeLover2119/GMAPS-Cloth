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

            // Calculate the desired rotation angles
            float XAngle = mouseX * rotationSpeed * Time.deltaTime;
            float YAngle = mouseY * rotationSpeed * Time.deltaTime;

            // Calculate the desired position in a circular orbit around the ship
            Vector3 origin = transform.position - ship.position;
            Quaternion XRotation = Quaternion.Euler(0f, XAngle, 0f);
            Quaternion YRotation = Quaternion.Euler(-YAngle, 0f, 0f);

            // Apply rotations to the offset
            Vector3 offset = YRotation * (XRotation * origin);

            // Set the new position of the camera, preserving the y-axis position
            transform.position = new Vector3(ship.position.x + offset.x, ship.position.y + offset.y, ship.position.z + offset.z);

            // Make the camera look at the ship (you can skip this part if not needed)
            transform.LookAt(ship.position);
        }
    }

    void ZoomCamera()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the zoom based on the scroll wheel input
        float newZoom = Mathf.Clamp(Vector3.Distance(transform.position, ship.position) - scrollWheel * zoomSpeed, minZoom, maxZoom);

        // Calculate the new camera position
        Vector3 newPosition = ship.position + (transform.position - ship.position).normalized * newZoom;

        // Apply the new position
        transform.position = newPosition;
    }
}
