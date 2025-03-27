using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateConTroller : MonoBehaviour
{
    Animator AnimatorComponent;
    public GameObject Gate;
    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
        //Gate = GameObject.FindGameObjectWithTag("Gate");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AnimatorComponent.Play("RockHead Blink");
            Destroy(Gate);
        }
    }
}
