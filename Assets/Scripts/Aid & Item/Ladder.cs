using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isLadder = false;
    private bool isClimbing = false;
    private float climbSpeed = 3f;
    private PlayerControllers playerControllers;
    private void Awake()
    {
        playerControllers = FindObjectOfType<PlayerControllers>();
    }


    private void Update()
    {
        if (isLadder && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing)
        {
            Rigidbody2D playerRb = playerControllers.GetComponent<Rigidbody2D>();
            if (Input.GetKey(KeyCode.W))
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, climbSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, -climbSpeed);
            }
            else
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            }
        }
    } 
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isLadder = false;
        }
    }
}
