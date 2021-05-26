using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCenterOfMassToTransform : MonoBehaviour
{
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.centerOfMass = this.transform.position;
        rb.centerOfMass = new Vector2(0f, 0f);
    }
    

    
}
