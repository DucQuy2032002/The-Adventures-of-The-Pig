using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostParticles : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.07f);

    }
}
