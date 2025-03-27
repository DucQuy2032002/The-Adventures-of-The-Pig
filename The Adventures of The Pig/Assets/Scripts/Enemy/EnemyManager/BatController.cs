using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BatController : Enemy  
{
    protected private Transform target; //The target is Player

    protected private Vector3 startPos;

    [SerializeField] protected Rigidbody2D Rigidbody2DComponent;

    protected override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startPos = transform.position;
    }

    protected override IEnumerator Move()
    {  
        while (true)
        {
            if (Vector3.Distance(transform.position, target.position) < distance)
            {
                AnimatorComponent.Play("Enemy Walk");
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                yield return null;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, startPos) < 0.01f)
                {
                    AnimatorComponent.Play("Enemy Idle");
                }
                yield return null;
            }
           
            TurnDirection() ;

            yield return new WaitForSeconds(0.01f);
        }
    }

    protected override void TurnDirection()
    {
        //If the object is near the target, apply flip logic according to position relative to the target
        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            spriteRenderer.flipX = transform.position.x < target.position.x;
        }      
        else
        {
            spriteRenderer.flipX = transform.position.x < startPos.x;
        }

    }

    protected override IEnumerator AnimationEnemy()
    {
        AnimatorComponent.Play("Enemy Idle");

        yield return null;
    }

}
