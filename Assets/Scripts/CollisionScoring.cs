using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollisionScoring : MonoBehaviour
{
    public Transform diskCenter; // Reference to the center of the disk
    public float maxDistance = 1; // Maximum distance from the center to the edge of the disk

    private TextMeshPro scoreText; // Reference to the TextMeshPro component
    private int totalScore; // Variable to keep track of the total score

    void Start()
    {
        if (diskCenter == null)
        {
            diskCenter = this.transform; // Set the diskCenter to the object's transform if not assigned
        }

        // Find the TextMeshPro component in the child object
        scoreText = GetComponentInChildren<TextMeshPro>();

        if (scoreText == null)
        {
            Debug.LogError("No TextMeshPro component found in children.");
        }

        // Initialize the total score
        totalScore = 0;
        UpdateScoreText();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (scoreText == null) return;

        // Get the collision point
        Vector3 collisionPoint = collision.contacts[0].point;

        // Calculate the distance from the collision point to the center of the disk
        float distance = Vector3.Distance(collisionPoint, diskCenter.position);

        // Calculate the points based on the distance
        int points = CalculatePoints(distance);

        // Update the total score
        totalScore += points;

        // Update the text with the new total score
        UpdateScoreText();
    }

    int CalculatePoints(float distance)
    {
        if (distance < 0.5f * maxDistance)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    void UpdateScoreText()
    {
        // Update the text component with the new score
        scoreText.text = totalScore + "";
    }
}
