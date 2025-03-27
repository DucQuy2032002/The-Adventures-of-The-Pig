using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    Animator AnimatorComponent;
    public float BounceJump = 10f;

    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
    }
    void BounceJumpUp(Rigidbody2D playerRb)
    {
        playerRb.velocity = new Vector2 (playerRb.velocity.x, 0);

        AnimatorComponent.SetTrigger("Jump");

        playerRb.AddForce(new Vector2(0, 1) * BounceJump, ForceMode2D.Impulse);
         
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            BounceJumpUp(playerRb);
        }
    }
}
