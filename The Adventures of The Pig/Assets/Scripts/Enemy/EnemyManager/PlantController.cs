using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class PlantController : TrunkController
{
    protected override IEnumerator ShootRoutine()
    {
        while (true)
        {
            AnimatorComponent.SetBool("Attack", true);
            yield return new WaitForSeconds(0.5f);
            AnimatorComponent.SetBool("Attack", false);
            yield return new WaitForSeconds(3f);
        }
    }

    protected override IEnumerator TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount       
        AnimatorComponent.Play("Enemy Hit");
        yield return new WaitForSeconds(0.2f);
        AnimatorComponent.Play("Enemy Idle");
        Debug.Log("currentHealt is: " + currentHealth);

        
        if (currentHealth <= 0)
        {
            Die();
        }
        
    } 
    protected override void Die()
    {
        base.Die(); 
    }

    //Not execute Move function
    protected override IEnumerator Move()
    {
        yield break;
    }

    //Not execute Move function
    protected override IEnumerator AnimationEnemy()
    {
        yield break; //Ending a coroutine immediately stops the entire execution of the coroutine.
    }


}
