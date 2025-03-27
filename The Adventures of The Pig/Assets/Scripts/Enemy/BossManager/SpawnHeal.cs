using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHeal : MonoBehaviour
{
    [SerializeField] private GameObject HealPrefabs;
    [SerializeField] private Transform spawnHeal;

    private void Start()
    {
         StartCoroutine(CreateHeal());
    }

    IEnumerator CreateHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            GameObject Heal = Instantiate(HealPrefabs, spawnHeal.position, Quaternion.identity, null);
        }
    }
}
