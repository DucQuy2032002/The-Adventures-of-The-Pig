using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : PlantController
{
    
    protected override IEnumerator Move()
    {
        while (true)
        {
            while (Vector2.Distance(transform.position, currentwayPoint.position) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentwayPoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            ChangePlatformDirectionMovement();
            yield return new WaitForSeconds(0.01f);
        }
    }

    protected override void Shoot()
    {
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();

        if (spriteRenderer.flipX == false)
        {
            BulletPosition = firePointRight.position;
            BulletDirection = new Vector2(0, -1);
            EnemyBulletManagerScripts.BulletRotate(false);
        }
        else
        {
            BulletPosition = firePointLeft.position;
            BulletDirection = new Vector2(0, 1);
            EnemyBulletManagerScripts.BulletRotate(true);
        }
        var NewBullet = Instantiate(Bullet, BulletPosition, Quaternion.identity, null); //Create a new Bullet
        var NewBulletRigibody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigibody.velocity = BulletDirection * BulletSpeed;
    }
}
