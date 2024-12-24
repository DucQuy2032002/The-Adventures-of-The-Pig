using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : Enemy
{
    protected private Transform target; //The target is Player

    protected override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    protected override IEnumerator Move()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, target.position) < distance)
            {
                Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                yield return null;
            }

            TurnDirection();
            yield return new WaitForSeconds(0.01f);
        }    
    }
    protected override IEnumerator TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount 
        AnimatorComponent.Play("Enemy Hit");
        yield return new WaitForSeconds(0.3f);
        AnimatorComponent.Play("Enemy Idle");
        Debug.Log("currentHealt is: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
        
    
    protected override void TurnDirection()
    {
        spriteRenderer.flipX = transform.position.x < target.position.x;
    }
    protected override IEnumerator AnimationEnemy()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, target.position) < distance)
            {
                AnimatorComponent.Play("Enemy Run");
            }
            else
            {
                AnimatorComponent.Play("Enemy Idle");

            }
            yield return null;
        }
    }
}
