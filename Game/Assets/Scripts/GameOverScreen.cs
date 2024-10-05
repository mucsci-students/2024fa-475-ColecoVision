using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameOverScreen : MonoBehaviour
{
    public static bool gameOver = false;
    public GameObject PauseMenuCanvas;
    public GameObject player;
    public Text pointsText;

    public void printScore(int score) {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + "POINTS";
    }
    public void Start() {
        Time.timeScale = 0f;
       //player.GetComponent<FirstPersonController>().enabled = false;
       // Cursor.lockState = CursorLockMode.None;
   // Cursor.visible = true;
   // gameOver = true;
    }
    public void Update() {

    }
}
