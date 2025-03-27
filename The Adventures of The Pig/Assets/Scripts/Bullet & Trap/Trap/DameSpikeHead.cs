using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameSpikeHead : Dame
{
    [SerializeField] protected Vector3 startPos;
    [SerializeField] private GameObject gameObjectSpikeHead;
    [SerializeField] private Rigidbody2D rigidbody2DSpikeHead;

    private void Awake()
    {
        startPos = transform.position;
        rigidbody2DSpikeHead = GetComponentInParent<Rigidbody2D>();
        gameObjectSpikeHead = transform.parent.gameObject;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigidbody2DSpikeHead.gravityScale = 0f;
            StartCoroutine(MovetoStartPos());
        }
    }

    IEnumerator MovetoStartPos()
    {
        yield return new WaitForSeconds(1.5f);
        gameObjectSpikeHead.transform.position = startPos;
    }
}
