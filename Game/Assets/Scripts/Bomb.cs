using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    
    public GameManager gameManager; 

    public static event Action OnCollected;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
            gameManager.setBombToTrue();
        }
    }
   
}
