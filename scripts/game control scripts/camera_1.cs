using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_1 : MonoBehaviour {

    GameObject player;
    Vector2 posPlay;

    private float xSpeed, ySpeed;
    public float xvar1, yvar1;
    private bool canUpdate;
    // Use this for initialization
    void Start()
    {

        player = GameObject.FindWithTag("Player");
        posPlay = new Vector2(player.transform.position.x, player.transform.position.y);
        if(player != null)
        {
            transform.position = new Vector3(posPlay.x, posPlay.y, transform.position.z);
            canUpdate = true;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                canUpdate = true;
            }
        }
        if (canUpdate)
        {
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            posPlay = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 posDiff = new Vector2(posPlay.x - myPos.x, posPlay.y - myPos.y);

            xSpeed = xvar1 * Mathf.Abs(posDiff.x);
            ySpeed = yvar1 * Mathf.Abs(posDiff.y);

            if (myPos.x != posPlay.x)
            {
                if (Mathf.Abs(posPlay.x - myPos.x) <= xSpeed)
                {
                    myPos = new Vector2(posPlay.x, myPos.y);
                }
                else
                if (myPos.x > posPlay.x)
                {
                    myPos = new Vector2(myPos.x - xSpeed, myPos.y);
                }
                else
                {
                    myPos = new Vector2(myPos.x + xSpeed, myPos.y);
                }
            }



            if (myPos.y != posPlay.y)
            {
                if (Mathf.Abs(posPlay.y - myPos.y) <= ySpeed)
                {
                    myPos = new Vector2(myPos.x, posPlay.y);
                }
                else
                if (myPos.y > posPlay.y)
                {
                    myPos = new Vector2(myPos.x, myPos.y - ySpeed);
                }
                else
                {
                    myPos = new Vector2(myPos.x, myPos.y + ySpeed);
                }
            }

            transform.position = new Vector3(myPos.x, myPos.y, transform.position.z);

            /////////test



            transform.position = new Vector3(posPlay.x, posPlay.y, transform.position.z);
        }


    }//end update
}
