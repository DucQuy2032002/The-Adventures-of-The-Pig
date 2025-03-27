using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public SpriteRenderer BulletSpriteRender;

    public GameObject HitEffect;

    public float LaserDamage;

    private float maxRange = 19f;
    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, startPos) > maxRange)
        {
            Destroy(this.gameObject);
        }
    }

    public void BulletRotate(bool bool_value)
    {
        BulletSpriteRender.flipX = bool_value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamage", LaserDamage, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(this.gameObject);
        CreateHitEffect();
    }

    void CreateHitEffect()
    {
        Instantiate(HitEffect,transform.position, Quaternion.identity, null);
        Destroy(this.gameObject);
    }
}
