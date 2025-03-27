using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrunkController : Enemy
{
    [SerializeField] protected private float BulletSpeed = 1f;

    [SerializeField] public GameObject Bullet;

    [SerializeField] protected Transform firePointRight;
    [SerializeField] protected Transform firePointLeft;

    public Bullet EnemyBulletManagerScripts;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootRoutine());
    }
    protected virtual IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            AnimatorComponent.Play("Enemy Attack");
            yield return new WaitForSeconds(0.5f);
            Shoot();
            yield return new WaitForSeconds(0.2f);
            AnimatorComponent.Play("Enemy Walk");
        }
    }

    protected virtual void Shoot()
    {
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();

        if (spriteRenderer.flipX == false)
        {
            BulletPosition = firePointRight.position;
            BulletDirection = new Vector2(-1, 0);
            EnemyBulletManagerScripts.BulletRotate(false);
        }
        else
        {
            BulletPosition = firePointLeft.position;
            BulletDirection = new Vector2(1, 0);
            EnemyBulletManagerScripts.BulletRotate(true);
        }
        var NewBullet = Instantiate(Bullet, BulletPosition, Quaternion.identity, null); //Create a new Bullet
        var NewBulletRigibody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigibody.velocity = BulletDirection * BulletSpeed;
    }
}
