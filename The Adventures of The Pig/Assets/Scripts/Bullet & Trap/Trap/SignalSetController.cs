using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalSetController : MonoBehaviour
{
    private Animator animatorComponent;
    [SerializeField] private GameObject shootingMachine;

    private void Awake()
    {
        animatorComponent = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animatorComponent.Play("Blocks HitTop");
            if(shootingMachine != null)
            {
                shootingMachine.GetComponent<AutoTurret>().ActivateShoot();
            }
            Destroy(this.gameObject, 1f);
        }
    }
}
