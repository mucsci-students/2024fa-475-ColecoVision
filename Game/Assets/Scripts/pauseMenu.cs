using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class pauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    public GameObject controlsCanvas;
    public GameObject dialogue;
    public GameObject player;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !dialogue.activeSelf)
        {
            if (Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }
    
    void Stop()
    {
        PauseMenuCanvas.SetActive(true); 
        Time.timeScale = 0f;
        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
        Paused = true;
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
       player.GetComponent<FirstPersonController>().enabled = true;
        Paused = false;
    }
    public void PauseMenu() {
        controlsCanvas.SetActive(false);
        PauseMenuCanvas.SetActive(true);
    }
    public void controls() {
        PauseMenuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}