using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw : MoveToPaths
{
    [SerializeField] private int SawDamage;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllers.Instance.PlayerHealthPointUpdate(SawDamage);
        }
    }
}
