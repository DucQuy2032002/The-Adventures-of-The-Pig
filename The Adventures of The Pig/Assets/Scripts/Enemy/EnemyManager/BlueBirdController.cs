using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdController : Enemy
{
    protected private Transform target; //The target is Player

    protected override void Awake()
    {
        base.Awake();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentwayPoint = wayPoint[currentwayPointIndex];
    }

    protected override IEnumerator Move()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, target.position) < distance) //Check the distance from Enemy to Player
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); //Move to player
                yield return null;
            }
            else
            {
                if (Vector2.Distance(transform.position, currentwayPoint.position) < 0.01f)
                {
                    ChangePlatformDirectionMovement();
                }
                transform.position = Vector2.MoveTowards(transform.position, currentwayPoint.position, moveSpeed * Time.deltaTime); //Move to currentwayPoint

            }
            TurnDirection();

            yield return new WaitForSeconds(0.01f);
        }
    }

    protected override void TurnDirection()
    {
        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            spriteRenderer.flipX = transform.position.x < target.position.x;
        }
        else
        {
            base.TurnDirection();
        }
    }

    protected override IEnumerator AnimationEnemy()
    {
        AnimatorComponent.Play("Enemy Walk");

        yield return null;
    }
}
