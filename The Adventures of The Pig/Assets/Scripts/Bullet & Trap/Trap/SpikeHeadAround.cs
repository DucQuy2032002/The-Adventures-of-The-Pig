using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeHeadAround : MoveToPaths
{
    [SerializeField] private int SpikeHeadDamage;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllers.Instance.PlayerHealthPointUpdate(SpikeHeadDamage);

        }
    }
}

