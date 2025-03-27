using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : Enemy
{
    protected private Transform target; //The target is Player

    [SerializeField] protected BoxCollider2D BoxCollider2DComponent;
    protected override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        TurnDirection();
    }
    protected override IEnumerator Move()
    {
        yield break;
    }

    protected override IEnumerator TakeDamage(float damageAmount)
    {
            currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount 
            AnimatorComponent.Play("Turtle Hit");
            yield return new WaitForSeconds(0.01f);
            AnimatorComponent.Play("Turtle Idle 1");
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

    protected override IEnumerator AnimationEnemy()
    {
        while (true)
        {          
            AnimatorComponent.Play("Turtle Idle 1");

            yield return new WaitForSeconds(3f);

            AnimatorComponent.Play("Turtle Spikes out");

            yield return new WaitForSeconds(0.3f);

            AnimatorComponent.Play("Turtle Idle 2");

            yield return new WaitForSeconds(3f);

            AnimatorComponent.Play("Turtle Spikes in");

            yield return new WaitForSeconds(0.3f);

            yield return null;
        }
    }

    
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (AnimatorComponent.GetCurrentAnimatorStateInfo(0).IsName("Turtle Idle 1"))
        {
            base.OnCollisionStay2D(collision);
        }
    }
    

    protected override void TurnDirection()
    {
        spriteRenderer.flipX = transform.position.x < target.position.x;
    }
}
