using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RadishController : Enemy
{
    private bool isHealing = false; 
    private void Update()
    {
        if (currentHealth < maxHealth && isHealing == false)
        {
            StartCoroutine(Healing());
        }
    }
    protected override IEnumerator TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount 
        if (currentHealth > maxHealth * 0.5f)
        {
            AnimatorComponent.Play("Radish Hit");
            yield return new WaitForSeconds(0.2f);
            AnimatorComponent.Play("Radish Idle 1");
        }
        else
        {
            AnimatorComponent.Play("Radish Hit");
            yield return new WaitForSeconds(0.2f);
            AnimatorComponent.Play("Radish Idle 2");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected override IEnumerator AnimationEnemy()
    {
        if (currentHealth > maxHealth * 0.5f)
        {
            AnimatorComponent.Play("Radish Idle 1");
        }
        else
        {
            AnimatorComponent.Play("Radish Idle 2");
        }

        yield return null;
    }

    IEnumerator Healing()
    {
        isHealing = true;

        while (currentHealth < maxHealth)
        {
            yield return new WaitForSeconds(3f);
            currentHealth += 5;

            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                break;
            }
            
            StartCoroutine(AnimationEnemy());
        }

        isHealing = false;
      
    }
}
