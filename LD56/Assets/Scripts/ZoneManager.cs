using UnityEngine;
using UnityEngine.SceneManagement; // To restart the level

public class ZoneManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the cube enters the Lose Zone
        if (other.CompareTag("lose"))
        {
            // Restart the level when the cube touches the lose zone
            Debug.Log("You touched the bad zone! Restarting level...");
            RestartLevel();
        }
        // Check if the cube enters the Win Zone
        else if (other.CompareTag("win"))
        {
            // Display a message about winning
            Debug.Log("You reached the goal! You win!");
        }
    }

    void RestartLevel()
    {
        // Reload the current scene (restart the level)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
