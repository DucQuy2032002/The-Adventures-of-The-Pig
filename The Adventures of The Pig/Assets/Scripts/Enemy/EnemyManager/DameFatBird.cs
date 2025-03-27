using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DameFatBird : Dame
{
    [SerializeField] protected Vector3 startPos;
    [SerializeField] private GameObject gameObjectFatBird;
    [SerializeField] private Rigidbody2D rigidbody2DFatBird;
    [SerializeField] private Animator animatorFatBird;

    [SerializeField] private float maxHealth; //MaxHealth Enemy
    private float currentHealth; //CurrentHealth Enemy
    [SerializeField] private GameObject dieEffect;

    private void Awake()
    {
        currentHealth = maxHealth;
        startPos = transform.position;
        rigidbody2DFatBird = GetComponentInParent<Rigidbody2D>();
        gameObjectFatBird = transform.parent.gameObject;
        animatorFatBird = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigidbody2DFatBird.gravityScale = 0f;
            animatorFatBird.Play("FatBird Ground");
            StartCoroutine(MovetoStartPos());
        }
    }

    IEnumerator MovetoStartPos()
    {
        yield return new WaitForSeconds(2f);
        gameObjectFatBird.transform.position = startPos;
        animatorFatBird.Play("FatBird Idle");
        
    }

    IEnumerator TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; //CurrentHealth = CurrentHealth - damageAmount 
        animatorFatBird.Play("FatBird Hit");
        yield return new WaitForSeconds(0.2f);
        animatorFatBird.Play("FatBird Idle");
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
    void CreateDieEffect()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity, null);
    }
}
    
