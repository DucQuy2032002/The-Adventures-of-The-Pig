using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdController : MonoBehaviour
{
    private Rigidbody2D Rigidbody2DComponent;
    [SerializeField] private Animator AnimatorComponent;

    private void Awake()
    {
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
        AnimatorComponent = GetComponent<Animator>();
    }

    void Fall()
    {
        Rigidbody2DComponent.bodyType = RigidbodyType2D.Dynamic;
        AnimatorComponent.Play("FatBird Fall");
        Rigidbody2DComponent.gravityScale = 3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Fall();
        }
    }

    
}

