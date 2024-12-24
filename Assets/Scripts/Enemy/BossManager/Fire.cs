using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerControllers.Singleton.PlayerHealthPointUpdate(-10);
        }
    }
}
