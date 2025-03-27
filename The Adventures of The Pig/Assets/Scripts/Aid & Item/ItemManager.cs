using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int itemCount;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI finalItemText;

    public static ItemManager Instance { get; private set; }

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

    public void ItemCount()
    {
        itemCount++;
        itemText.text = finalItemText.text = itemCount.ToString();
    }
}
