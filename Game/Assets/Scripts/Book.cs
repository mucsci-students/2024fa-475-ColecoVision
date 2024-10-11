using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Book : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0f, Time.time * 100f, 0);
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
