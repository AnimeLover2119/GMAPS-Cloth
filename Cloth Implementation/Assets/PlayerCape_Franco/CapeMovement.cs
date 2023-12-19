using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCapeMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Speed of cape movement

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Limit vertical movement to zero
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            verticalInput = 0.0f;
        }

        // Calculate movement direction based on input
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Apply movement to the cape along its local axes (only horizontal movement)
        transform.Translate(transform.right * movement.x + transform.forward * movement.z);
    }
}