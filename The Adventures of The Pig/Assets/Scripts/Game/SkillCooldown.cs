using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] private Image CooldownCover;
    [SerializeField] private TextMeshProUGUI CooldownText;
    [SerializeField] private Image SkillIcon;
    [SerializeField] private float CooldownTime;


    private float CooldownTimer;
    public bool isCooldown;

    private void Start()
    {
        CooldownCover.fillAmount = 0;
        CooldownText.text = "";
        //SkillButton.onClick.AddListener(OnSkillButtonClicked);
    }

    private void Update()   
    {
        CooldownManager();
    }

    void CooldownManager()
    {
        if (isCooldown)
        {
            CooldownTimer -= Time.deltaTime;
            CooldownCover.fillAmount = CooldownTimer / CooldownTime;

            CooldownText.text = Mathf.Max(CooldownTimer, 0).ToString("F1"); //Mathf.Max function compares CooldownTimer and 0, it will return the larger value. Guaranteed to never be a negative value
                                                                            //F1: round to 1 decimal place

            if (CooldownTimer <= 0)
            {
                isCooldown = false;
                CooldownCover.fillAmount = 0;
                SkillIcon.color = new Color(1, 1, 1, 1);
                CooldownText.text = "";
            }
        }
    }

    public void StartCooldown()
    {
        if(!isCooldown)
        {
            isCooldown = true;
            CooldownTimer = CooldownTime;
            CooldownCover.fillAmount = 1;
            SkillIcon.color = new Color(1, 1, 1, 0.5f);
        }
    }

}

