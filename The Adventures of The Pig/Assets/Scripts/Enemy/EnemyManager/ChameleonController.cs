using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChameleonController : Enemy
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine(AnimationAttackChameleon());
    }

    IEnumerator AnimationAttackChameleon()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AnimatorComponent.Play("Enemy Attack");
            yield return new WaitForSeconds(0.8f);
            AnimatorComponent.Play("Enemy Walk");
        }
    }
}
