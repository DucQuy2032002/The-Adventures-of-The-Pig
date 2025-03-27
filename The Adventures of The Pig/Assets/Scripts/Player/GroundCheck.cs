using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerControllers PlayerControllerScripts;

    private HashSet<string> ignoreNames = new HashSet<string> //HashSeT
    { 
        "DetectionZone",
        "SnailRecognitionZone",
        "Boss Skull",
        "Fire",
        "Vortex",
        "Ladder",
        "Item"
    };
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (!ignoreNames.Contains(collision.gameObject.name)) //Ignore collision
        {
            PlayerControllerScripts.isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerControllerScripts.isGrounded = false;
    }
}
