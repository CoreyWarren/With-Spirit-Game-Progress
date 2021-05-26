using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sense : MonoBehaviour
{
    public float checkRadius;
    public LayerMask checkLayers;

    //print order of targets based on distance

    private void Update()
    {
        if (Input.GetKey("w"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            Array.Sort(colliders, new DistanceComparer(transform));

            foreach (Collider item in colliders)
            {
                Debug.Log(item.name);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
