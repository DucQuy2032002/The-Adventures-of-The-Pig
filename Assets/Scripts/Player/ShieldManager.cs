using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && PlayerControllers.Singleton.isShielded == true )
        {
            PlayerControllers.Singleton.PlayerHealthPointUpdate(0);
        }
    }
}
