using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isLadder = false;
    private bool isClimbing = false;
    private float climbSpeed = 4f;
    private GameObject player;

    private bool moveUp;


    private void Start()
    {
        StartCoroutine(FindPlayer());
    }

    private void Update()
    {
        //player = GameObject.FindWithTag("Player");
        #if UNITY_STANDALONE || UNITY_WEBGL
           EscalationForPC();
        #elif UNITY_IOS || UNITY_ANDROID
            EscalationForMobile();
        #endif

    }

    void EscalationForPC()
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
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
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

    void EscalationForMobile()
    {
        if (isLadder && moveUp)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
        if (isClimbing)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (moveUp)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, climbSpeed);
            }
            else
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            }
        }
    }

    
    public void OnMobileClimbButtonDownPressed()
    {
        moveUp = true;
        Debug.Log("Button Down: moveUp = " + moveUp);
    }

    public void OnMobileClimbButtonUpPressed()
    {
        moveUp = false;
        Debug.Log("Button Up: moveUp = " + moveUp);
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

    IEnumerator FindPlayer()
    {
        player = null;
        
        while(player == null)
        {
            player = GameObject.FindWithTag("Player");
            yield return new WaitForSeconds(1f);
        }
    }
}
