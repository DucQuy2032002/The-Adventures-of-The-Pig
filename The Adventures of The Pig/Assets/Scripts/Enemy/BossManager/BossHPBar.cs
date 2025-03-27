using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] private RectTransform RectTransformComponent;
    int HPBarWidthMax = 1000; 
    int BossHPMax = 1000; 

    [SerializeField] private TextMeshProUGUI HPAmount;
    public void UpdateBossHPBar(int BossHP)
    {
        // new HPBar Width = Current BossHP * HPBarWidthMax / BossHPMax
        float newHPBarWidth = BossHP * HPBarWidthMax / BossHPMax;
        RectTransformComponent.sizeDelta = new Vector2(newHPBarWidth, RectTransformComponent.sizeDelta.y);

        HPAmount.text = BossHP + "/" + BossHPMax;
    }
}
