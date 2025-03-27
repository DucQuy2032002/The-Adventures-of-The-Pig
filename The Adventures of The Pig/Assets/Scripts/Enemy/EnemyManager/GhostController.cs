using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : Enemy
{
    [SerializeField] protected List<Transform> PointRight;
    [SerializeField] protected List<Transform> PointLeft;

    [SerializeField] protected GameObject GhostParticles;
    [SerializeField] protected BoxCollider2D BoxCollider2DComponent;


    protected override void Start()
    {
        base.Start();
        StartCoroutine(CreateGhostParticles());
    }
    protected override IEnumerator TakeDamage(float damageAmount)
    {

        currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount 
        AnimatorComponent.Play("Ghost Hit");
        yield return new WaitForSeconds(0.2f);
        AnimatorComponent.Play("Ghost Idle");
        Debug.Log("currentHealt is: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    IEnumerator CreateGhostParticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GhostParticlesRoutine();
        }
    }
    protected override IEnumerator AnimationEnemy()
    {
        while (true)
        {
            AnimatorComponent.Play("Ghost Idle");

            yield return new WaitForSeconds(1.5f);

            AnimatorComponent.Play("Ghost Desappear");

            yield return new WaitForSeconds(0.3f);

            spriteRenderer.enabled = false;
            BoxCollider2DComponent.enabled = false;

            yield return new WaitForSeconds(1.5f);

            AnimatorComponent.Play("Ghost Appear");

            spriteRenderer.enabled = true;
            BoxCollider2DComponent.enabled = true;

            yield return new WaitForSeconds(0.1f);

            yield return null;
        }     
    }

    void GhostParticlesRoutine()
    {
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();

        if (AnimatorComponent.GetCurrentAnimatorStateInfo(0).IsName("Ghost Idle"))
        {
            if (spriteRenderer.flipX == false)
            {
                foreach (Transform point in PointRight)
                {
                    BulletPosition = point.position;
                    BulletDirection = new Vector2(-1, 0);
                    Instantiate(GhostParticles, BulletPosition, Quaternion.identity, null); //Create a new Bullet

                }
            }
            else
            {
                foreach (Transform point in PointLeft)
                {
                    BulletPosition = point.position;
                    BulletDirection = new Vector2(1, 0);
                    Instantiate(GhostParticles, BulletPosition, Quaternion.identity, null); //Create a new Bullet

                }
            }
        }      
    }
}
