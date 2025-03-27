using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : Enemy
{
    [SerializeField] private GameObject smallerRockPrefab;
    private int spawnCount = 2;
    
    //[SerializeField] protected GameObject rock3Prefab;

    protected override void Die()
    {
        base.Die();
        SpawnEnemySmaller();
    }

    void SpawnEnemySmaller()
    {
        if (smallerRockPrefab != null)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                float offsetX = (i - (spawnCount - 1) / 2.0f) * 2f; 
                Instantiate(smallerRockPrefab, new Vector2(transform.position.x + offsetX, transform.position.y - 0.5f), Quaternion.identity, null);
            }
        }
    }

    
}
