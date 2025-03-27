using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectionZone : MonoBehaviour
{
    
    public bool isPlayerInZone = false;
    [SerializeField] public BoxCollider2D BoxCollider2DCollider;

    public void Awake()
    {
        BoxCollider2DCollider = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = true;

            BoxCollider2DCollider.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }
    
}
