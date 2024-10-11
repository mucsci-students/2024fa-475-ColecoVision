using UnityEngine;
using TMPro; 

public class PointsDisplay : MonoBehaviour
{
    
    public TextMeshProUGUI pointsText;

    private int playerPoints;

    void Start()
    {
        playerPoints = 0; // Start with 0 points or your initial value
        UpdatePointsText();
    }

    // Method to increase points (you can call this from other scripts)
    public void AddPoints(int points)
    {
        playerPoints += points;
        UpdatePointsText();
    }

    // Update the UI text with the current points value
    void UpdatePointsText()
    {
        pointsText.text = "Points: " + playerPoints.ToString();
    }
}