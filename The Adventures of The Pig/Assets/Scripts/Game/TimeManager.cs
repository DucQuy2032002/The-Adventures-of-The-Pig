using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTimeText;
    public float remainingTime;
    private bool isRuning = true;
    private Color defaultColor;

    private void Start()
    {
        // Convert HEX color #6A0000 to Color
        ColorUtility.TryParseHtmlString("#6A0000", out defaultColor);
        timerText.color = defaultColor; // Reset original color
    }

    private void Update()
    {
        if (remainingTime > 0 && isRuning == true)
        {
            remainingTime -= Time.deltaTime;
        }
        if(remainingTime <= 0 && isRuning == true )
        {
            remainingTime = 0;
            isRuning = false;
            PlayerControllers.Instance.GameOver();
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = finalTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Flashing effect when 10s left
        if (remainingTime <= 10)
        {
            float alpha = Mathf.PingPong(Time.time * 2f, 1); //Flashing Speed
            timerText.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, alpha); // Keep original color, only change transparency
        }
        else
        {
            timerText.color = defaultColor; // Return to original color
        }
    }

    public void StopTime()
    {
        isRuning = false;
    }
}
