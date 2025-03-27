using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string enemyName; //Name Enemy
    [SerializeField] protected private float moveSpeed; //Speed Enemy
    [SerializeField] protected private int enemyDamage; //Damage Enemy

    [SerializeField] protected float maxHealth; //MaxHealth Enemy
    protected float currentHealth; //CurrentHealth Enemy

    [SerializeField] protected private float distance; 

    [SerializeField] protected private SpriteRenderer spriteRenderer;
    [SerializeField] public Animator AnimatorComponent;
 
    [SerializeField] public List<Transform> wayPoint;
    protected private int currentwayPointIndex = 0; //first position in the List wayPoint
    protected Transform currentwayPoint; //index current in List wayPoint

    [SerializeField] protected GameObject dieEffect;
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        currentwayPoint = wayPoint[currentwayPointIndex]; //first position in List wayPoint 
    }
    protected virtual void Start()
    {
        StartCoroutine(Move());
        StartCoroutine(AnimationEnemy());
    }

    //*******CHANGE PLATFORM DIRECTION MOVEMENT*******
    protected virtual void ChangePlatformDirectionMovement()
    {       
       currentwayPointIndex = currentwayPointIndex + 1;
       {
          if (currentwayPointIndex >= wayPoint.Count)
             currentwayPointIndex = 0;
       }
         currentwayPoint = wayPoint[currentwayPointIndex];
    }

    //*******TURN DIRECTION ENEMY = FLIP.X********
    protected virtual void TurnDirection()
    {
        if (currentwayPoint.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    //******FUNCTION ENEMY MOVEMENT********
    protected virtual IEnumerator Move()
    {
        while (true)
        {
            while (Vector2.Distance(transform.position, currentwayPoint.position) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentwayPoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            ChangePlatformDirectionMovement();
            TurnDirection();
            yield return new WaitForSeconds(0.01f);
        }
    } 

    //******FUNCTION ATTACK PLAYER******
    protected virtual void TakeDamageToPlayer()
    {
        PlayerControllers.Instance.PlayerHealthPointUpdate(enemyDamage);
    }

    protected virtual void AttackPlayer()
    {
        TakeDamageToPlayer();
    }
    

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
   
        }
    }

    //********FUNCTION TAKE DAMAGE OF ENEMY********
    protected virtual IEnumerator TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount 
        AnimatorComponent.Play("Enemy Hit");
        yield return new WaitForSeconds(0.2f);
        AnimatorComponent.Play("Enemy Walk");
        Debug.Log("currentHealt is: " + currentHealth);

        if (currentHealth <= 0)
        { 
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(transform.parent.gameObject);
        CreateDieEffect();
        EnemyScore.Instance.EnemyCount();
    }

    protected virtual IEnumerator AnimationEnemy()
    { 
        AnimatorComponent.Play("Enemy Idle");

        yield return new WaitForSeconds(0.01f);

        AnimatorComponent.Play("Enemy Walk");
    }
    void OnDrawGizmosSelected()
    {
        //Draw a circle in Scene View to see the blast radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    void CreateDieEffect()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity, null);
    }

}
