using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private PlayerController playercontroller;

    private void Update()
    {
        playercontroller.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
