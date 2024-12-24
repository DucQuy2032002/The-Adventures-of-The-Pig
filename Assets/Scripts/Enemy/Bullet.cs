using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int BulletDame;

    [SerializeField] public SpriteRenderer BulletSpriteRenderer;

    protected virtual void Awake()
    {
        Physics2D.IgnoreLayerCollision(12, 9);
        Physics2D.IgnoreLayerCollision(12, 11);
    }

    public virtual void BulletRotate(bool bool_value)
    {
        BulletSpriteRenderer.flipX = bool_value;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BulletTakeDameToPlayer();
            
        }
        Destroy(this.gameObject);
    }
    
    protected virtual void BulletTakeDameToPlayer()
    {
        PlayerControllers.Singleton.PlayerHealthPointUpdate(BulletDame);
    }


}
