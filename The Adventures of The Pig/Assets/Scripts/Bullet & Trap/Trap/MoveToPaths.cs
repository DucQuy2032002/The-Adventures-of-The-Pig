using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPaths : MonoBehaviour
{
    [SerializeField] public List<Transform> wayPoint;
    private  int currentwayPointIndex = 0; 
    private Transform currentwayPoint; 

    Rigidbody2D Rigibody2DComponent;
    public float MoveSpeed = 2f;
    public float WaitTime = 1f;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void Awake()
    {
        Rigibody2DComponent = GetComponent<Rigidbody2D>();
        currentwayPoint = wayPoint[currentwayPointIndex];
    }
    public void ChangePlatformDirectionMovement()
    {
        currentwayPointIndex = currentwayPointIndex + 1;
        {
            if (currentwayPointIndex >= wayPoint.Count)
                currentwayPointIndex = 0;
        }
        currentwayPoint = wayPoint[currentwayPointIndex];
    }
    public IEnumerator Move()
    {
        while (true)
        {
            while (Vector2.Distance(transform.position, currentwayPoint.position) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentwayPoint.position, MoveSpeed * Time.deltaTime);
                yield return null;
            }

            Rigibody2DComponent.velocity = Vector2.zero;
            yield return new WaitForSeconds(WaitTime);

            ChangePlatformDirectionMovement();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}
