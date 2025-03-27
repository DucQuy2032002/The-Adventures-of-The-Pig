using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGravity : MonoBehaviour
{
    private Rigidbody2D Rigidbody2DComponent;

    private void Awake()
    {
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2DComponent.bodyType = RigidbodyType2D.Dynamic;
            Rigidbody2DComponent.gravityScale = 1f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2DComponent.gravityScale = 0f;
            Rigidbody2DComponent.velocity = Vector2.zero;
        }
    }
}
