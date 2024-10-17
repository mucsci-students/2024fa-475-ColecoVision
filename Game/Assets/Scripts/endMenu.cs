using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using TMPro;

public class endMenu : MonoBehaviour
{
    public GameObject endUI;
    public TextMeshProUGUI points;
    public GameObject player;
   
// displays the end menu with the number of points
   public void showEndMenu(int score) {
    endUI.SetActive(true);
    player.GetComponent<FirstPersonController>().enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
points.text = "Points: " + score.ToString();
   }
}
