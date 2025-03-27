using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;

    private void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        GameObject player = null;

        while (player == null)
        {
            player = GameObject.FindWithTag("Player");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player assigned to Follow");
        }

        virtualCam.Follow = player.transform;
    }
}
