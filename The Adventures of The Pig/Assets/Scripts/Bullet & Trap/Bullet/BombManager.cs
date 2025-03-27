using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;


public class BombManager : MonoBehaviour
{
    public float explosionRadius = 5f;
    public LayerMask affectedLayers;

    int TouchCount = 0;
    public GameObject HitEffect;


    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(9, 13);

    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CreateHitEffect();
            AudioManager.Instance.PlaySoundBombExplosion();
        }

        TouchCount++;
        if (TouchCount >= 3)
        {
            CreateHitEffect();
            AudioManager.Instance.PlaySoundBombExplosion();
        }


    }

    void CreateHitEffect()
    {
        Explode();
        var neweffect = Instantiate(HitEffect,transform.position,Quaternion.identity,null);
        neweffect.transform.localScale = new UnityEngine.Vector3(explosionRadius, explosionRadius, 0);

        Destroy(this.gameObject);
    }

    void Explode()
    {
        
        Collider2D[] Collider2DComponent = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);
        //Create blast zone
        //Physics2D.OverlapCircleAll: Create a 2D circle at the object's position (transform.position) with a radius of explosionRadius
        //affectedLayers: The layers of objects to be checked for collision. Only objects belonging to these layers will be affected by the explosion.
        //Collider2D[]: Array containing all colliding objects (Collider2D) located within the circular region.

        foreach (Collider2D nearbyObject in Collider2DComponent)
        {
            //Deals damage to a target that can take damage
             Debug.Log("Damaging of Bomb enemy: " + nearbyObject.name);
             nearbyObject.gameObject.SendMessage("TakeDamage", 15, SendMessageOptions.DontRequireReceiver); //Call damage function
        }
    }

    void OnDrawGizmosSelected()
    {
        //Draw a circle in Scene View to see the blast radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
