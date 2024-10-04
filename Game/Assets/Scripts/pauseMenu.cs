using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class pauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    public GameObject player;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Paused = true;
    }

    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
       player.GetComponent<FirstPersonController>().enabled = true;
        Paused = false;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
