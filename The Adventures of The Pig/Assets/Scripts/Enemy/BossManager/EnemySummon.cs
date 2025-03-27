using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummon : BatController
{
    protected override IEnumerator Move()
    {
        while (true)
        {  
            AnimatorComponent.Play("Enemy Walk");
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;

            TurnDirection();
        }
    }

    protected override void TurnDirection()
    {
        Vector2 direction = target.position - transform.position;
        spriteRenderer.flipX = direction.x > 0;   
    }
}
