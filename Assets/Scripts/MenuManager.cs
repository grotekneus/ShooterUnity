using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Call this function when the start button is clicked
    public void ResetScore()
    {
        // Load the game scene or start the game
        

    }

    // Call this function when the exit button is clicked
    public void ResetWorld()
    {
        // Exit the game
        SceneManager.LoadScene(0);
    }
}
