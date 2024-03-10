using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private Transform playerTransform;

    private void Start()
    {
        FindPlayer();
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);
        }
        else
        {
            FindPlayer();
        }
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player"); // Assuming the player has the tag "Player"

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'.");
        }
    }
}
