using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeController : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] originalVertices;

    public float gravity = -9.81f; // Strength of gravity force
    public Vector3 windDirection = new Vector3(1.0f, 0.0f, 0.0f); // Direction of wind
    public float windStrength = 1.0f; // Strength of wind force

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        originalVertices = mesh.vertices;
    }

    void Update()
    {
        // Simulate cloth-like movement
        for (int i = 0; i < vertices.Length; i++)
        {
            // Apply gravity to each vertex
            vertices[i] += Vector3.up * gravity * Time.deltaTime;

            // Apply wind force to each vertex
            vertices[i] += windDirection * windStrength * Time.deltaTime;

            // Update vertices (example: here, a sine wave is added for demonstration)
            vertices[i] += new Vector3(0.0f, Mathf.Sin(Time.time + i * 0.1f), 0.0f) * 0.1f;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}