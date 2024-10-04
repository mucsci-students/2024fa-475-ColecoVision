using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour

   {
    public Transform player;       // The player's transform
    public float followRadius = 10f; // Radius within which the enemy will follow the player
    public float speed = 3f;       // Movement speed of the enemy

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the follow radius
        if (distanceToPlayer <= followRadius)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
