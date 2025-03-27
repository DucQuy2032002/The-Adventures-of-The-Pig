using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : Enemy
{
    [SerializeField] private Transform Point1;
    [SerializeField] private Transform Point2;

    [SerializeField] protected GameObject Mucus;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(CreateMucus());
    }
    protected override IEnumerator TakeDamage(float damageAmount)
    {
        
            currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount 
            AnimatorComponent.Play("Slime Hit");    
            yield return new WaitForSeconds(0.2f);
            AnimatorComponent.Play("Slime Idle-Run");
            Debug.Log("currentHealt is: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        
    }

    IEnumerator CreateMucus()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            MucusRoutine();
        }
    }
    protected override IEnumerator AnimationEnemy()
    {
        AnimatorComponent.Play("Slime Idle-Run");

        yield return null;
    }

    void MucusRoutine()
    {
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();

        if (spriteRenderer.flipX == false)
        {
            BulletPosition = Point1.position;
            BulletDirection = new Vector2(-1, 0);
        }
        else
        {
            BulletPosition = Point2.position;
            BulletDirection = new Vector2(1, 0);
        }
        Instantiate(Mucus, BulletPosition, Quaternion.identity, null); //Create a new Bullet
    }


}
