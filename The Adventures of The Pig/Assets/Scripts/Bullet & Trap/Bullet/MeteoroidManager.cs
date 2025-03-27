using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MeteoroidManager : MonoBehaviour
{
    public float explosionRadius = 10f;
    public LayerMask affectedLayers;

    public GameObject MeteoroidEffect;

    private CinemachineImpulseSource impulseSource;
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(9, 13);

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CreateMeteoroidEffect();
            AudioManager.Instance.PlaySounddMeteoriteExplosion();
        }

        CameraShakeManager.Instance.CameraShake(impulseSource);
    }


    void CreateMeteoroidEffect()
    {
        Blast();
        Instantiate(MeteoroidEffect, transform.position, Quaternion.identity, null);
        Destroy(this.gameObject);
    }

    void Blast()
    {
        Collider2D[] Collider2DComponent = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D nearbyObject in Collider2DComponent)
        {
            Debug.Log("Damaging of Meteoroid enemy: " + nearbyObject.name);
            nearbyObject.gameObject.SendMessage("TakeDamage", 30, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnDrawGizmosSelected()
    {
        //Draw a circle in Scene View to see the blast radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
