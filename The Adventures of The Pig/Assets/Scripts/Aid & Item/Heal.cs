using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public int Healing = 20;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllers.Instance.RecoveryHealPlayer(Healing);
            Destroy(this.gameObject);
            AudioManager.Instance.PlaySoundCollectItem();
        }
    }
}
