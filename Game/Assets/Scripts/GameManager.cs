using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;    // Singleton instance
    public GameObject gameOverUI;          // Game Over UI Canvas
    public GameObject player;
    public GameObject continueMenuUI;
    public GameObject dialogueUI;
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI tierText; 
    public Dialogue dialogue; //references Dialogue script
    public int tier = 0;
    public int score = 0;
    public int count = 0;

     // Store original positions
    private Vector3 originalPlayerPosition;
    private List<Vector3> originalEnemyPositions = new List<Vector3>();

    //create lists of predetermined enemy and trap positions for when new ones get placed. remove position when used.

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
        // Store the original player position
        originalPlayerPosition = player.transform.position;

         // Store original positions of enemies
        StoreEnemyPositions();
        
         tier = 0;

         dialogueUI.SetActive(true);
         dialogue.zeroTierScript();
         //showContinueMenu();
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

            // Reset player position
            player.transform.position = originalPlayerPosition; // Reset to original position
        }
        else
        {
            Debug.LogError("Player reference is null when restarting the level.");
        }

    
        SceneManager.LoadScene("Game");  // Reload current scene (reset back to tier 0)
    }

    //if player gets the finish room object, call this method.
    public void Advance()
    {
        count++;
        //DO NOT CALL RESTARTLEVEL - IT WILL RESET THE ENTIRE GAME
Debug.Log("Advance was called");
 //showContinueMenu();
        //increment tier (as long as player is not at the final tier)

       // dialogueUI.SetActive(true);
        if (tier < 3)
        {
            tier++;
        }
       //player.GetComponent<FirstPersonController>().enabled = false;
        //after that, queue the next piece of dialogue.
        //Dialogue dialogue = new Dialogue();
if (count < 4) {
    dialogueUI.SetActive(true);
        if (tier == 1)
        {
            dialogue.firstTierScript();
        }
        if (tier == 2)
        {
            dialogue.secondTierScripts();
        }
        if (tier == 3)
        {
            dialogue.thirdTierScript();
        }
}
else {
    player.GetComponent<FirstPersonController>().enabled = true;
    Time.timeScale = 1;
}

        // Reset player and enemy positions to their original ones
        ResetPositions();
    }

     public void showContinueMenu()
    {

        player.GetComponent<FirstPersonController>().enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
        Debug.Log("Showing continue menu");
        // Set the UI text to show the score and tier
        if (tierText != null)
        {
            tierText.text = "Tier: " + tier.ToString();
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }

        // Show the continue menu
        continueMenuUI.SetActive(true);

    }
     public void OnContinue()
    {
        Debug.Log("Continue button pressed!"); // Add this line
        // Logic to continue the game
        continueMenuUI.SetActive(false); // Hide continue menu
        Advance();
    }
    //stores enemy positions
     private void StoreEnemyPositions()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        originalEnemyPositions.Clear(); // Clear previous positions

        foreach (GameObject enemy in enemies)
        {
            originalEnemyPositions.Add(enemy.transform.position);
        }
    }

     private void ResetPositions()
    {
        // Reset player position
        player.transform.position = originalPlayerPosition;

        // Reset enemy positions
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length && i < originalEnemyPositions.Count; i++)
        {
            enemies[i].transform.position = originalEnemyPositions[i];
        }
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
