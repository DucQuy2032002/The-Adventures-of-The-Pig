using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private Animator animatorComponent;
    private SpriteRenderer spriteRendererComponent;
    private CircleCollider2D circleCollider2DComponent;

    private void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        circleCollider2DComponent = GetComponent<CircleCollider2D>();
    }

    void Push(Rigidbody2D playerRb)
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Push(playerRb);
            StartCoroutine(RespawnArrow());
        }
    }

    IEnumerator RespawnArrow()
    {

        animatorComponent.Play("Arrow Hit");

        float animationTime = animatorComponent.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationTime);

        spriteRendererComponent.enabled = false;
        circleCollider2DComponent.enabled = false;

        yield return new WaitForSeconds(3f);

        spriteRendererComponent.enabled = true;
        circleCollider2DComponent.enabled = true;

        animatorComponent.Play("ArrowIdle");
    }
}
