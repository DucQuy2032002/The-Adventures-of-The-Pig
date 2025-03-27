using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TrapDisappear : MonoBehaviour
{
    [SerializeField] private Color colorDisappear = new Color(1, 1, 1, 0.5f);
    [SerializeField] private Tilemap tilemapRender;

    private void Awake()
    {
        tilemapRender = GetComponent<Tilemap>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tilemapRender.color = colorDisappear;
            Destroy(gameObject, 0.5f);
        }
    }
}