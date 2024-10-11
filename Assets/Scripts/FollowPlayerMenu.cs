using UnityEngine;

public class FollowPlayerMenu : MonoBehaviour
{
    public Transform playerHead; // Assign the player’s head (camera) transform here
    public float distanceFromPlayer = 2.0f; // Distance of the menu from the player
    public Vector3 menuOffset = new Vector3(0, 0.5f, 0); // Optional offset to adjust the position

    void Update()
    {
        // Position the menu in front of the player at the set distance
        Vector3 newPosition = playerHead.position + playerHead.forward * distanceFromPlayer;
        transform.position = newPosition + menuOffset;

        // Make the menu face the player
        transform.LookAt(playerHead);
        transform.Rotate(0, 180, 0); // Rotate by 180 degrees since LookAt makes the back face the player
    }
}
