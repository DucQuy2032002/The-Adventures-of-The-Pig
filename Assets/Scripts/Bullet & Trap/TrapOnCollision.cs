using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOnCollision : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            PlayerControllers.Singleton.PlayerHealthPointUpdate(-5);
        }
    }
}
