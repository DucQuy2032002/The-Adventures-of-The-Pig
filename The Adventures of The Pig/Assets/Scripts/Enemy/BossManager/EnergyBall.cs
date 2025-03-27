using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] private GameObject DarkExplosionEffect;

    [SerializeField] private float explosionRadius = 5f;

    [SerializeField] LayerMask affectedLayers;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllers.Instance.PlayerHealthPointUpdate(-25);
            CreateDarkExplosionEffect();
            Destroy(this.gameObject);
        }
    }

    void CreateDarkExplosionEffect()
    {
        Instantiate(DarkExplosionEffect, transform.position, Quaternion.identity, null);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
