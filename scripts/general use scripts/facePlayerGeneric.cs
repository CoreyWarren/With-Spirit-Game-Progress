using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facePlayerGeneric : MonoBehaviour
{
    //This script flips the scale of an object horizontally
    //so that it appears to always be facing the player.


    private Transform playerTrans;
    private Vector2 defaultScale;
    [SerializeField]
    private bool reverseDirection;


    // Start is called before the first frame update
    void Start()
    {
        defaultScale = new Vector2(transform.localScale.x, transform.localScale.y);
        if(reverseDirection)
        {
            defaultScale = new Vector2(-defaultScale.x, defaultScale.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if(GameObject.FindGameObjectWithTag("Player").transform != null)
        {
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }


        if(playerTrans != null)
        {

            {
                if (this.transform.position.x > playerTrans.position.x)
                {

                    if (transform.localScale != new Vector3(-defaultScale.x, transform.localScale.y))
                    {
                        transform.localScale = new Vector2(-defaultScale.x, transform.localScale.y);
                    }

                }
                else if (this.transform.position.x < playerTrans.position.x)
                {
                    if(transform.localScale != new Vector3(defaultScale.x, transform.localScale.y))
                    {
                        transform.localScale = new Vector2(defaultScale.x, transform.localScale.y);
                    }
                    
                }
            }
            
        }
       



    }
}
