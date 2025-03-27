using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyScore : MonoBehaviour
{
    public int enemiesKilled;
    public TextMeshProUGUI finalEnemyScoreText;

    public static EnemyScore Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyCount()
    {
        enemiesKilled++;
        finalEnemyScoreText.text =  enemiesKilled.ToString();
    }
}
