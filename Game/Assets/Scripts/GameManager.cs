using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;    // Singleton instance
    public GameObject gameOverUI;          // Game Over UI Canvas
    public GameObject player;
    public int tier;

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
        
         tier = 0;
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

    
        SceneManager.LoadScene("Game");  // Reload current scene (reset back to tier 0)
    }

    //if player gets the finish room object, call this method.
    public void Advance()
    {
        //DO NOT CALL RESTARTLEVEL - IT WILL RESET THE ENTIRE GAME

        //call ContinueMenu() method.
        ContinueMenu();

        //increment tier (as long as player is not at the final tier)
        if (tier < 3)
        {
            tier++;
        }

        //after that, queue the next piece of dialogue.
        Dialogue dialogue = new Dialogue();

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

        //reset player and enemy positions.
        //List<GameObject> enemies = new List<GameObject>();
        
        //the two arrays store all the current enemies and traps.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");

        GameObject currentObj = new GameObject();

        //store positions from enemies array.
        List<Vector3> enemyPositions = new List<Vector3>();
        for (int enIndex = 0; enIndex < enemies.Length; enIndex++)
        {
            currentObj = enemies[enIndex];
            //will this get the position they're at now or where they spawn in?
            Vector3 enemyPos = currentObj.transform.position;
            enemyPositions.Add(enemyPos);
        }

        //place the enemies at their original positions. (are the positions correct?)
        for (int enposIndex = 0; enposIndex < enemyPositions.Count; enposIndex++)
        {
            currentObj = enemies[enposIndex];
            transform.position = enemyPositions[enposIndex];
        }
    }

    //***brings up the menu*** that shows the player's current score/score for that level, and prompts the player to continue further in the game.
    //would this need to be a seperate script?
    public void ContinueMenu()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var firstPersonController = player.GetComponent<FirstPersonController>();
        firstPersonController.enabled = false;
        //prompt da menu
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
