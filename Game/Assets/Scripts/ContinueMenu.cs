using UnityEngine;
using UnityEngine.UI; // Use this for regular UI Text
using TMPro; // Uncomment if using TextMeshPro

public class ContinueMenu : MonoBehaviour
{
    public GameObject continueMenuPanel; // Assign this in the Inspector
   
    
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI tierText; 
    private void Start()
    {
        // Ensure the menu is hidden at the start
        continueMenuPanel.SetActive(false);
    }

    public void ShowMenu(int score, int tier)
    {
        continueMenuPanel.SetActive(true); // Show the continue menu
        UpdateScoreDisplay(score); // Update the score display
        UpdateTierDisplay(tier); // Update the tier display
    }

    private void UpdateScoreDisplay(int score)
    {
        scoreText.text = "Score: " + score.ToString(); // Update the score text
    }

    private void UpdateTierDisplay(int tier)
    {
        tierText.text = "Tier Completed: " + tier.ToString(); // Update the tier text
    }

    private void OnContinue()
    {
        continueMenuPanel.SetActive(false); // Hide the continue menu
        GameManager.instance.Advance(); // Proceed to the next tier
    }

    private void OnCancel()
    {
        continueMenuPanel.SetActive(false); // Hide the continue menu
        // Additional cancel logic if needed
    }
}