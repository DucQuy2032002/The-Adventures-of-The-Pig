using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapFalling : MonoBehaviour
{
    private Rigidbody2D Rigibody2DComponent;
    private float fallDelay = 0.25f;
    private float destroyDelay = 3f;
    private bool isFalling = false;

    private void Awake()
    {
        Rigibody2DComponent = GetComponent<Rigidbody2D>();
    }

    IEnumerator Falling()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);
        Rigibody2DComponent.bodyType = RigidbodyType2D.Dynamic;
        Destroy(this.gameObject, destroyDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Falling());
        }
    }
}
