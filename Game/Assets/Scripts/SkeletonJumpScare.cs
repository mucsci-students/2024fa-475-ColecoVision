using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkeletonJumpScare : MonoBehaviour
{
   public GameObject[] skeletons;
   public float spawnDuration = 3f;

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
      SetSkeletonsActive(true);  // Make skeletons visible
        StartCoroutine(DespawnSkeletonsAfterTime());
    }
   

   public void SetSkeletonsActive(bool state) {
      foreach (GameObject skeleton in skeletons)
         {
             skeleton.SetActive(state);
         }
    
   }
     System.Collections.IEnumerator DespawnSkeletonsAfterTime()
    {
        Debug.Log("Despawning soon");
        yield return new WaitForSeconds(spawnDuration);
        SetSkeletonsActive(false);  // Make skeletons invisible again
    }

}
