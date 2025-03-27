using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailRecognitionZone : MonoBehaviour
{
    [SerializeField] public bool isPlayerInZone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = false;

        }
    }
}
