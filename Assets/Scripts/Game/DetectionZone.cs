using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectionZone : MonoBehaviour
{
    public bool isPlayerInZone = false;
    [SerializeField] public BoxCollider2D BoxCollider2DCollider;
    [SerializeField] protected SpikeHead Trap;

    public void Awake()
    {
        Physics2D.IgnoreLayerCollision(13, 14);
        BoxCollider2DCollider = GetComponent<BoxCollider2D>();
        Trap = GetComponentInParent<SpikeHead>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = true;

            Trap.startPos = transform.parent.position;
            BoxCollider2DCollider.enabled = false;
            Trap.Rigibody2DComponent.gravityScale = 2;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInZone = false;
        }
    }
}
