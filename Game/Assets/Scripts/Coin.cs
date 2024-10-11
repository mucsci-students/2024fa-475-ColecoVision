using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Coin : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }

    public static event Action OnCollected;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
