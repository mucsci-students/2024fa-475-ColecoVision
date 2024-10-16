using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject settingsMenu;
    public GameObject player;
    void Start() {
         
        player.GetComponent<FirstPersonController>().enabled = false;
         Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    }
    public void Play()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        SceneManager.LoadScene("Game");
    }
    public void controls() {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    public void MainMenu() {
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);

    }
    public void settings() {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }
}
