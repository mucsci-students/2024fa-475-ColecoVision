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
    public GameObject chooseUI;
    public GameObject bomb;
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI tierText; 
    public TextMeshProUGUI pointsText;
    public Dialogue dialogue; //references Dialogue script
    public int tier = 0;
    public int score = 0;
    public int count = 0;
    public bool hasBomb = false;

     // Store original positions
    private Vector3 originalPlayerPosition;
    private Quaternion initialRotation;
    private List<Vector3> originalEnemyPositions = new List<Vector3>();
    private List<Vector3> originalCoinPositions = new List<Vector3>();

    //For New Enemy/Trap Placements
    private GameObject[] nuEnemies;
    private GameObject[] nuTraps;


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
        Debug.Log("Initial player position: " + originalPlayerPosition.ToString());
        initialRotation = player.transform.rotation;

         // Store original positions of enemies
        StoreEnemyPositions();
        StoreCoinPositions();
        StoreNuPositions();
        
         tier = 0;

         dialogueUI.SetActive(true);
         dialogue.zeroTierScript();
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
        pointsText.text = score.ToString() + " points";
    }
    
    public void RestartLevel()
    {
        // Restart the current level
        Debug.Log("Restart");
        Time.timeScale = 1f;  // Resume time
        gameOverUI.SetActive(false);

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

        if (tier < 3)
        {
            tier++;
        }
        player.GetComponent<FirstPersonController>().enabled = false;

        //queue the next piece of dialogue.
        if (count < 4) 
        {

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
            bomb.SetActive(true);
             dialogue.thirdTierScript();
             bomb.SetActive(true);
            }
        }
        else 
        {
        player.GetComponent<FirstPersonController>().enabled = true;
         Time.timeScale = 1;
        }
        // Reset player and enemy positions to their original ones

        //player.GetComponent<FirstPersonController>().enabled = true;
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
       // ResetPositions();
        // player.GetComponent<FirstPersonController>().enabled = true;
        // Debug.Log("Initial player position b4  calling reset in onContinue: " + originalPlayerPosition.ToString());
         
        //  player.GetComponent<FirstPersonController>().enabled = false;
        Advance();
        //chooseUI.SetActive(true);
    }
    // runs when the player chooses an enemy
    public void enemyOption() {
        chooseUI.SetActive(false);
        newPlacement(0);
    }
    // runs when the player chooses a trap
    public void trapOption() {
        chooseUI.SetActive(false);
        newPlacement(1);

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
      //stores coin positions
     private void StoreCoinPositions()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        originalCoinPositions.Clear(); // Clear previous positions

        foreach (GameObject coin in coins)
        {
            originalCoinPositions.Add(coin.transform.position);
        }
    }
    //Stores all the objects the player has not placed yet. Ensures they are not active at start.
    private void StoreNuPositions()
    {
        nuEnemies = GameObject.FindGameObjectsWithTag("NuEnemy");
        nuTraps = GameObject.FindGameObjectsWithTag("NuTrap");

        for (int enIndex = 0; enIndex < nuEnemies.Length; enIndex++)
        {
            nuEnemies[enIndex].SetActive(false);
        }

        for (int trIndex = 0; trIndex < nuTraps.Length; trIndex++)
        {
            nuTraps[trIndex].SetActive(false);
        }
    }

    //for placing new enemies/traps. have ones that are set to false, and switch the one at a specific position to true.

    // enemy is when choice = 0
    // trap is when choice = 1
    public void newPlacement(int choice)
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        Time.timeScale = 1;
        System.Random rand = new System.Random();

        
        //enemy
        if (choice == 0)
        {
            int randEnIndex = rand.Next(nuEnemies.Length);
            GameObject newEnemy = nuEnemies[randEnIndex];
            newEnemy.tag = "Enemy";
            newEnemy.SetActive(true);
            StoreEnemyPositions();
            //does reset positions need to be called?
            //we need to remove the newly placed enemy from the nuEnemies array. But how to do it?
            //                                                                    do we actually remove it and resize the array?
            //                                                                 or do we keep it and prevent the index from being reused?
            Debug.Log("A new enemy has been placed.");
        }

        //trap
        if (choice == 1)
        {
            int randTrIndex = rand.Next(nuTraps.Length);
            GameObject newTrap = nuTraps[randTrIndex];
            newTrap.tag = "Trap";
            newTrap.SetActive(true);

            Debug.Log("A new trap has been placed.");
        }
    }

     public void ResetPositions()
    {
        Debug.Log("Original player position: " + originalPlayerPosition.ToString());
        player.SetActive(true);

        // Reset player position
        player.transform.position = originalPlayerPosition;
        player.transform.rotation = initialRotation;
        Debug.Log("Player new position: " + player.transform.position);
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
        // Reset enemy positions
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length && i < originalEnemyPositions.Count; i++)
        {
            enemies[i].transform.position = originalEnemyPositions[i];
        }
          // Reset coin positions
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        for (int i = 0; i < coins.Length && i < originalCoinPositions.Count; i++)
        {
            coins[i].transform.position = originalCoinPositions[i];
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
    // adds points to the score 
    public void addScore(int points)
    {
        points = points * (tier + 1);
        score += points;
         
     if (scoreText != null)
     {
        scoreText.text = "Score: " + score.ToString();
        FindObjectOfType<PointsDisplay>().AddPoints(points);
     }
        Debug.Log("Score added");
    }
    // called when the player collects the bomb
    public void setBombToTrue() {
        hasBomb = true;
        Debug.Log("hasBomb set to true");
    
    }
    
}
