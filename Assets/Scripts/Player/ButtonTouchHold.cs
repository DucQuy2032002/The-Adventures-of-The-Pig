using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchHold : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHold;
    public PlayerControllers PlayerControllerscript;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHold = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(TouchEnd());
    }

    private void Update()
    {
        if (isHold == true)
        {
            if (gameObject.name == "Button Right")
            {
                PlayerControllerscript.isPressedButtonRight = true;
            }
            if (gameObject.name == "Button Left")
            {
                PlayerControllerscript.isPressedButtonLeft = true;
            }
            if (gameObject.name == "Button Run" ) 
            {
                PlayerControllerscript.PlayerRunOn();
            }
        }
    }

    IEnumerator TouchEnd()
    {
        yield return new WaitForSeconds(0.05f);
        isHold = false;
        if (gameObject.name == "Button Right")
        {
            PlayerControllerscript.isPressedButtonRight = false;
        }
        if (gameObject.name == "Button Left")
        {
            PlayerControllerscript.isPressedButtonLeft = false;
        }
        if (gameObject.name == "Button Run")
        {
            PlayerControllerscript.PlayerRunOff();
        }
    }
}
