using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;    // Singleton instance
    public GameObject gameOverUI;          // Game Over UI Canvas
    public GameObject player;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of the GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameOverUI);
        }
        else
        {
            Destroy(gameOverUI);
        }
    }
    private void Start() {
         // Find and assign the player object by tag
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogWarning("Player not found in the scene. Ensure the Player has the 'Player' tag.");
            }
        }
         
    }

    public void GameOver()
    {
        // Show the Game Over screen and pause the game
        Debug.Log("Game over");
         player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
    }

    public void RestartLevel()
    {
        // Restart the current level
         Debug.Log("Restart");
        Time.timeScale = 1f;  // Resume time
        gameOverUI.SetActive(false);
        //player.GetComponent<FirstPersonController>().enabled = true;
          player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
        {
            var firstPersonController = player.GetComponent<FirstPersonController>();
            if (firstPersonController != null)
            {
                firstPersonController.enabled = true; // Enable player control
            }

            // Reset player position (if needed)
            firstPersonController.transform.position = new Vector3(-5, 1, 5); // Example starting position
        }
        else
        {
            Debug.LogError("Player reference is null when restarting the level.");
        }

    
        SceneManager.LoadScene("Game");  // Reload current scene
    }

    public void GoToMainMenu()
    {
        // Return to the main menu
        Time.timeScale = 1f;  // Resume time
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");  // Load Main Menu scene

        // If you want to deactivate or reset player state
       if (player != null)
        {
            player.SetActive(false); // Deactivate player
        }
    }
}
