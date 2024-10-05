using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class NewBehaviourScript : MonoBehaviour
{
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

    public void quit()
    {
        Application.Quit();
    }
}
