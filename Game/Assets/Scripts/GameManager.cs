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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start() {
         player = GameObject.FindGameObjectWithTag("Player");
    }

    public void GameOver()
    {
        // Show the Game Over screen and pause the game
         player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
    }

    public void RestartLevel()
    {
        // Restart the current level
         
        Time.timeScale = 1f;  // Resume time
        gameOverUI.SetActive(false);
        player.GetComponent<FirstPersonController>().enabled = true;
          player = GameObject.FindGameObjectWithTag("Player");
          if (FirstPersonController.instance != null)
    {
        // Set this to your starting position
        FirstPersonController.instance.transform.position = new Vector3(-5, 1, 5); // Example starting position
    }
        SceneManager.LoadScene("Game");  // Reload current scene
    }

    public void GoToMainMenu()
    {
        // Return to the main menu
        Time.timeScale = 1f;  // Resume time
        SceneManager.LoadScene("MainMenu");  // Load Main Menu scene
        // If you want to deactivate or reset player state
    if (FirstPersonController.instance != null)
    {
        FirstPersonController.instance.gameObject.SetActive(false); // Deactivate player
    }
    }
}
