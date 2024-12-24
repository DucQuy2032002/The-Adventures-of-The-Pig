using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRotation : MonoBehaviour
{
    public Transform chain;

    public float roatationSpeed = 50f;


    private void Update()
    {
        RotateTrap();
    }

    void RotateTrap()
    {
        transform.RotateAround(chain.position, Vector3.forward, roatationSpeed * Time.deltaTime);
    }
}
