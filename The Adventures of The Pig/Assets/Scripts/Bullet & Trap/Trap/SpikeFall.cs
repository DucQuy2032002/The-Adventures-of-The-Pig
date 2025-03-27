using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    private Rigidbody2D Rigidbody2DComponent;

    private void Awake()
    {
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
    }

    void Fall()
    {
        Rigidbody2DComponent.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody2DComponent.gravityScale = 3f;
        Destroy(this.gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Fall();
        }
    }
}
