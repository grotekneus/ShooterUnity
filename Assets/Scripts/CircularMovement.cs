using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float radius = 5.0f; // Radius of the circle
    public float speed = 1.0f; // Speed of rotation
    private Vector3 center; // Center point of the circle
    private float angle; // Current angle of the object

    void Start()
    {
        center = transform.position; // Set the center to the object's starting position
    }

    void Update()
    {
        angle += speed * Time.deltaTime; // Increment the angle based on speed and time
        float x = Mathf.Cos(angle) * radius; // Calculate the x position
        float z = Mathf.Sin(angle) * radius; // Calculate the z position
        transform.position = new Vector3(center.x + x, transform.position.y, center.z + z); // Update the object's position
    }
}
