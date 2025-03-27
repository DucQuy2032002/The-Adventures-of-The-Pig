using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : Enemy
{
    [SerializeField] protected SnailRecognitionZone SnailRecognitionZone;
    public bool isMoving;
    [SerializeField] protected Rigidbody2D Rigibody2DComponent;
    protected int currentWayPointIndex = 0;

    protected override void Start()
    {
        StartCoroutine(AnimationEnemy());
    }
    private void Update()
    {
        Moving();
    }
    protected override IEnumerator AnimationEnemy()
    {
        while (true)
        {
            if (!isMoving)
            {
                //Debug.Log("Player is in Zone");
                AnimatorComponent.Play("Snail Shell Idle");
                Rigibody2DComponent.velocity = Vector2.zero;
            }
            else
            {
                AnimatorComponent.Play("Enemy Walk");
                transform.position = Vector2.MoveTowards(transform.position, currentwayPoint.position, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, currentwayPoint.position) <= 0.01f)
                {
                    ChangePlatformDirectionMovement();
                    TurnDirection();
                }
            }
            yield return null;
        }
    }
    
    void Moving()
    {
        if (SnailRecognitionZone.isPlayerInZone == true)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }


    /*
    void UpdateWayPoint()
    {
        currentWayPointIndex++;

        if (currentWayPointIndex >= wayPoint.Count) 
        {
            currentWayPointIndex = 0;
        }

        currentwayPoint = wayPoint[currentWayPointIndex]; 
        TurnDirection(); 
    }
    */
}
