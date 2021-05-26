using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnScript1 : MonoBehaviour
{

    //Script Purpose:
    //Turn on the light when player is within a certain radius.


    [SerializeField]
    private float playerCheckRadius;
    [SerializeField]
    private LayerMask whatIsPlayer;

    private bool touchingPlayer;
    private bool touchedPlayer;

    MeshRenderer mrr;
    private AudioSource as1;
    [SerializeField]
    private AudioClip lightOnSound;

    // Start is called before the first frame update
    void Start()
    {
        mrr = GetComponent<MeshRenderer>();
        as1 = GetComponent<AudioSource>();
        mrr.enabled = false;
        touchedPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!touchedPlayer)
        {
            touchingPlayer = Physics2D.OverlapCircle(this.transform.position, playerCheckRadius, whatIsPlayer);
            if(touchingPlayer)
            {
                touchedPlayer = true;
            }
        }

        
        //Turn on the light when player is within a certain radius
        if(touchedPlayer && !mrr.enabled)
        {
            as1.PlayOneShot(lightOnSound);
            //GameObject player = GameObject.FindWithTag("Player");
            //player.GetComponent<PlayerController>().enabled = false;
            mrr.enabled = true;
            //Time.timeScale = 0;
        }
    }
}
