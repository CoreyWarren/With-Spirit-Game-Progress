using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeProjectileEnemy : MonoBehaviour
{

    public bool goingRight;
    Rigidbody2D rb1;

    private Transform spriteChild;
    private SpriteRenderer srr;
    [SerializeField]
    private GameObject fader;
    

    [SerializeField]
    private float xVelocity, yVelocity, timer1Max, timer2Max, wallCheckRadius;

    [SerializeField]
    private float maxHorizontalSpeed, maxVerticalSpeed, zRotateAmount;

    private float timer1;
    private float timer2;
    private int whatStage;

    //Hit Wall
    bool hitWall, goLimp;

    [SerializeField]
    private float fadeMakePeriod, fadeChildTime;
    private float fadeMakeTimer;
    private bool makingFades;

    [SerializeField]
    private LayerMask whatIsWall;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        timer1 = timer1Max;
        whatStage = 1;
        goLimp = false; hitWall = false;
        makingFades = true;
        spriteChild = gameObject.transform.GetChild(0);
        srr = spriteChild.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauser1.paused)
        {

            hitWall = Physics2D.OverlapCircle(transform.position, wallCheckRadius, whatIsWall);


            spriteChild.transform.Rotate(new Vector3(0f,0f,1f) * Time.deltaTime * zRotateAmount);


            if(hitWall && !goLimp)
            {
                goLimp = true;
                gameObject.layer = 0;
                Collider2D my2dCollider;
                my2dCollider = GetComponent<Collider2D>();
                rb1.velocity = new Vector2(-rb1.velocity.x, -rb1.velocity.y);
                srr.color -= new Color(0.5f, 0.5f, 0.5f, 0.5f);
                my2dCollider.enabled = false;
                makingFades = false;
            }

            if(fadeMakeTimer <= 0)
            {
                GameObject f = Instantiate(fader, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity) as GameObject;
                SpriteRenderer faderSprite = f.GetComponent<SpriteRenderer>();
                faderSprite.sprite = srr.sprite;
                faderSprite.color = srr.color + new Color(0.2f, 0.2f, 0.2f, 0.2f);
                f.GetComponent<faderScript>().fadeTime = fadeChildTime;
                f.transform.rotation = spriteChild.transform.rotation;
                if(goLimp)
                {
                    f.GetComponent<faderScript>().goLimp = true;
                }

                fadeMakeTimer = fadeMakePeriod;
            }else
            {
                fadeMakeTimer -= Time.deltaTime * 10;
            }


            if (timer1 > 0 && whatStage == 1)
            {
                if (goingRight && !goLimp)
                {
                    rb1.velocity += new Vector2(xVelocity, yVelocity);
                }
                else if (!goingRight && !goLimp)
                {
                    rb1.velocity += new Vector2(-xVelocity, yVelocity);
                }

                timer1 -= Time.deltaTime * 10;

            }
            else if (timer1 <= 0 && whatStage == 1)
            {
                timer2 = timer2Max;
                whatStage = 2;
            }

            if (timer2 > 0 && whatStage == 2)
            {
                if (goingRight && !goLimp)
                {
                    rb1.velocity += new Vector2(xVelocity/2f, -yVelocity/3f);
                }
                else if (!goingRight && !goLimp)
                {
                    rb1.velocity += new Vector2(-xVelocity/2f, -yVelocity/3f);
                }
                timer2 -= Time.deltaTime * 10;
            }
            else if (whatStage == 2 && timer2 <= 0)
            {
                Destroy(gameObject);
            }



            //Check-ins
            {
                if(rb1.velocity.x > maxHorizontalSpeed)
                {
                    rb1.velocity = new Vector2(maxHorizontalSpeed, rb1.velocity.y);
                }else if
                    (rb1.velocity.x < -maxHorizontalSpeed)
                {
                    rb1.velocity = new Vector2(-maxHorizontalSpeed, rb1.velocity.y);
                }
            }


            {
                if (rb1.velocity.y > maxVerticalSpeed)
                {   
                    rb1.velocity = new Vector2(rb1.velocity.x, maxVerticalSpeed);
                }   
                else if
                   (rb1.velocity.y < -maxVerticalSpeed)
                {   
                    rb1.velocity = new Vector2(rb1.velocity.x, -maxVerticalSpeed);
                }   
            }


        }
        



    }
}
