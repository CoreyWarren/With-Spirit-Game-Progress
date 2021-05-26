using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spinner_script : MonoBehaviour {

    public Transform righttransform;
    public static bool righttouching;
    public float rightcheckradius;
    public LayerMask whatisright;
    public bool goingright;

    public Transform lefttransform;
    public static bool lefttouching;
    public float leftcheckradius;
    public LayerMask whatisleft;

    private float blinktimer;
    public float blinktimermax;
    private bool blinkon;
    private float blinkontimer;
    public float blinkontimermax;

    private Rigidbody2D rb;
    SpriteRenderer srr;
    Color defaultcolor;

    public float xspeed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(xspeed, 0f);
        blinkon = false;
        blinktimer = blinktimermax;
        blinkontimer = blinkontimermax;
        srr = GetComponent<SpriteRenderer>();
        defaultcolor = srr.color;
	}

	
	// Update is called once per frame
	void Update () {
        righttouching = Physics2D.OverlapCircle(righttransform.position, rightcheckradius, whatisright);
        lefttouching = Physics2D.OverlapCircle(lefttransform.position, leftcheckradius, whatisleft);

        if (righttouching)
        {
            rb.velocity = new Vector2(-xspeed, 0f);
            goingright = false;
        }

        if (lefttouching)
        {
            rb.velocity = new Vector2(xspeed, 0f);
            goingright = true;
        }

        if (goingright && rb.velocity.x < xspeed)
        {
            rb.velocity = new Vector2(xspeed, 0f);
        }
        if (!goingright && rb.velocity.x > -xspeed)
        {
            rb.velocity = new Vector2(-xspeed, 0f);
        }

        if(blinktimer == 0 && !blinkon)
        {
            blinktimer = blinktimermax;
            blinkon = true;
            srr.color = new Color(1f, 1f, 1f, 1f);
        }
        else if(blinkontimer == 0 && blinkon)
        {
            blinkontimer = blinkontimermax;
            blinkon = false;
            srr.color = defaultcolor;
        }

        if(!blinkon)
        {
            blinktimer--;
        }
        else
        {
            blinkontimer--;
        }
    }

}
