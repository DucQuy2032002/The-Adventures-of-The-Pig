using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dame : MonoBehaviour
{
    [SerializeField] protected int dameTrap;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllers.Instance.PlayerHealthPointUpdate(dameTrap);
        }
    }
}
