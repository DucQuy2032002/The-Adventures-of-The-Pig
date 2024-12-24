using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] protected DetectionZone DetectionZone;
    [SerializeField] float speed = 3f;
    [SerializeField] int SpikeHeadDamage;

    [SerializeField] public Rigidbody2D Rigibody2DComponent;
    [SerializeField] public BoxCollider2D BoxCollider2DComponent;
    [SerializeField] public Vector3 startPos;

    void Awake()
    {
        DetectionZone = GetComponentInChildren<DetectionZone>();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Ground"))
        {
            this.StartCoroutine(MoveToStartPos());
            Rigibody2DComponent.gravityScale = 0;
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllers.Singleton.PlayerHealthPointUpdate(SpikeHeadDamage);
        }
    }
    
    
    IEnumerator MoveToStartPos()
    {
        while(Vector3.Distance(Rigibody2DComponent.position, startPos) > 0.1f)
        {
            Vector2 direction = (startPos - transform.position).normalized;
            Rigibody2DComponent.velocity = direction * speed;
            yield return null;
        }

        Rigibody2DComponent.velocity = Vector2.zero;
        DetectionZone.BoxCollider2DCollider.enabled = true;  
    }
}
