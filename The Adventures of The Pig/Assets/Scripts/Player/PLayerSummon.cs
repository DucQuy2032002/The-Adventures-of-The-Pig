using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerSummon : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem fogPrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private bool hasSpawned;

    private GameObject currentLine;
    
    private void Start()
    {
        currentLine = Instantiate(linePrefab, spawnPoint.position + new Vector3(0, 9.5f, 0), Quaternion.identity, null);
        AudioManager.Instance.PlaySoundAppearanceCool();    
    }
   
    
    private void FixedUpdate()
    {
        if (hasSpawned == false && Vector3.Distance(currentLine.transform.position, spawnPoint.position) <= 0.3f)
        {
            //Debug.Log("Line has passed spawnPoint");
            Destroy(currentLine);
            CreateFog();
            Invoke("SummonPlayer", 1.5f);
            hasSpawned = true;
        }
    }
        
    void SummonPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity, null);
    }

    void CreateFog()
    {
        //Debug.Log("Fog created");
        Instantiate(fogPrefab, spawnPoint.position, Quaternion.identity, null);
    }
    

}
