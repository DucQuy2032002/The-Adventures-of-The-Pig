using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    public RectTransform RectTransformComponent;
    int HPBarWidthMax = 1000; //Maximum width of HP bar is 1000 pixel
    int PLayerHPMax = 100; //player's maximum health is 100

    public TextMeshProUGUI HPAmount;
    public void UpdatePlayerHPBar(int PlayerHP)
    {
        // new HPBar Width = Current PlayerHP * HPBarWidthMax / PlayerHPMax
        float newHPBarWidth = PlayerHP * HPBarWidthMax  / PLayerHPMax; 
        RectTransformComponent.sizeDelta = new Vector2(newHPBarWidth, RectTransformComponent.sizeDelta.y);

        HPAmount.text = PlayerHP + "/" + PLayerHPMax;
    }
}
