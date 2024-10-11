using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Oculus;

public class GunController : MonoBehaviour
{
    public Transform muzzleTransform;  // The position from which the ball will be fired
    public float ballSpeed = 20f;  // Speed of the ball
    public OVRInput.Button shootButton = OVRInput.Button.PrimaryIndexTrigger;  // Button to shoot (e.g., trigger)
    public float ballSize = 0.2f; // Size of the ball

    // Update is called once per frame
    void Update()
    {
        // Check if the shoot button is pressed
        if (OVRInput.GetDown(shootButton))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create a sphere primitive
        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Set the ball's position and rotation to match the muzzle
        ball.transform.position = muzzleTransform.position;
        ball.transform.rotation = muzzleTransform.rotation;

        // Set the ball's size
        ball.transform.localScale = Vector3.one * ballSize;

        // Add a Rigidbody component to the ball
        Rigidbody ballRb = ball.AddComponent<Rigidbody>();

        // Set the velocity of the ball
        ballRb.velocity = muzzleTransform.forward * ballSpeed;

        // Optionally, add a collider if needed (a collider is automatically added when creating a primitive)
    }
}
