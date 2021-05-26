using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTreasureScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(25, 23, true);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
