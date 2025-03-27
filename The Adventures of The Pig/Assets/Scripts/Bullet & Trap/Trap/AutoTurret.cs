using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoTurret : MonoBehaviour
{
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedBullet;
    [SerializeField] private float fireRate;
    [SerializeField] private Vector2 shootDirection;

    private Animator animatorShootMachine;

    private void Awake()
    {
        animatorShootMachine = GetComponent<Animator>();
    }

    public void ActivateShoot()
    {
        animatorShootMachine.Play("Fire Hit");
        StartCoroutine(ShootContinuosly());
    }

    IEnumerator ShootContinuosly()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(prefabBullet, firePoint.position, Quaternion.identity, null);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = shootDirection * speedBullet;
    }
}
