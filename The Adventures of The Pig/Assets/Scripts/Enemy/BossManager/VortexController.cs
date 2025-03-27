using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class VortexController : MonoBehaviour
{
    [SerializeField] private float pullForce = 5f;

     public Transform vortexCenter;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                
                Vector2 direction = (vortexCenter.position - other.transform.position).normalized;
                float distance = Vector2.Distance(vortexCenter.position, other.transform.position);

                float threshold = 0.1f;

                if(distance > threshold)
                {
                    playerRb.AddForce(direction * pullForce * 1.7f);

                    playerRb.velocity *= 0.95f;

                    pullForce += Time.deltaTime * 0.5f;
                }
                else
                {
                    playerRb.velocity = Vector2.zero;
                    playerRb.position = vortexCenter.position;
                }
                
            }
        }
    }
    
}
