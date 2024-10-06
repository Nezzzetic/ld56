using UnityEngine;
using UnityEngine.SceneManagement; // To restart the level

public class ZoneManager : MonoBehaviour
{

    public ParticleSystem loseEffect; // Particle System for lose FX
    public ParticleSystem winEffect; // Particle System for win FX
    public AudioSource loseSound; // Sound for losing
    public AudioSource winSound; // Sound for winning
    public float loseDelay = 2f; // Delay before restarting on lose
    public float winDelay = 2f; // Delay before showing win message or loading next level
    public MeshRenderer[] renderers;
    private Rigidbody cubeRigidbody;
    public GameObject LoseBlackScreen;
    public int NextLvlID;

    private void Awake()
    {
        cubeRigidbody=GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("lose"))
        {
            OnLoseAction();
        }
        else if (other.CompareTag("win"))
        {
            Debug.Log("You reached the goal! You win!");

            // Disable the cube's movement
            DisableCubeMovement();

            // Trigger FX and play sound
            TriggerWinFX();

            // Show win message or transition after a short delay
            Invoke("ShowWinMessage", winDelay);
        }
    }

    public void OnLoseAction()
    {
        Debug.Log("You touched the bad zone! Actions before restart...");

        // Disable the cube's movement
        DisableCubeMovement();

        // Trigger FX and play sound
        TriggerLoseFX();

        // Restart the level after a short delay
        Invoke("RestartLevel", loseDelay);
    }


    private void Update()
    {
        if  (Input.GetKeyUp(KeyCode.R)) { OnLoseAction(); }
    }

    void DisableCubeMovement()
    {
        // Disable the cube's Rigidbody to stop movement
        if (cubeRigidbody != null)
        {
            cubeRigidbody.velocity= Vector3.zero;
            cubeRigidbody.angularVelocity= Vector3.zero;
            cubeRigidbody.isKinematic = true; // Disable physics
        }
    }

    void TriggerLoseFX()
    {
        // Play particle effect if assigned
        if (loseEffect != null)
        {
            foreach (var item in renderers)
            {
                item.enabled = false;
            }
            loseEffect.gameObject.SetActive(true);
            
        }
        // Play sound if assigned
        if (LoseBlackScreen != null)
        {
            LoseBlackScreen.SetActive(true);
        }

        // Play sound if assigned
        if (loseSound != null)
        {
            loseSound.Play();
        }
    }

    // Trigger win effects (particles and sound)
    void TriggerWinFX()
    {
        // Play win particle effect
        if (winEffect != null)
        {
            foreach (var item in renderers)
            {
                item.enabled = false;
            }
            winEffect.transform.position = cubeRigidbody.transform.position; // Set FX at cube position
            winEffect.gameObject.SetActive(true);
        }
        if (LoseBlackScreen != null)
        {
            LoseBlackScreen.SetActive(true);
        }
        // Play win sound effect
        if (winSound != null)
        {
            winSound.Play();
        }
    }

    void RestartLevel()
    {
        // Reload the current scene (restart the level)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowWinMessage()
    {
        Debug.Log("Congratulations! Level complete!");
        SceneManager.LoadScene(NextLvlID);
        // You can add code here to transition to the next level, or show a "You Win" message
    }
}
