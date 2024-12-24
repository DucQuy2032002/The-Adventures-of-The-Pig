using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public int itemCount;
    public Text itemText;

    void Update()
    {
        itemText.text = itemCount.ToString();
    }
}
