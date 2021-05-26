using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spin_2 : MonoBehaviour {

    public Transform righttransform;
    public static bool righttouching;
    public float rightcheckradius;
    public LayerMask whatisright;
    public bool goingright;

    public Transform lefttransform;
    public static bool lefttouching;
    public float leftcheckradius;
    public LayerMask whatisleft;

    private Rigidbody2D rb;

    public float xspeed;
    Transform player;
    public float awakedistance;
    private float lasertimer;
    public float lasertimermax;
    public GameObject laser;
    GameObject laserclone;
    
    public GameObject explosion;
    GameObject explosionclone;
    public GameObject soul;
    GameObject soulclone;

    public AudioSource as1;
    public AudioClip shoot;
    public AudioClip hitSound;

    //Damage Checkers
    public Transform damagecheck;
    public float damagecheckradius;
    public LayerMask whatisdamage;
    private bool damage;
    private float health;
    public float healthMax;
    public float invincibilityTime;
    private float invincibilityTimer;
    Color Blink1 = new Color(1f, 1f, 0.5f, 1f);
    Color Blink2 = new Color(1f, 0.5f, 1f, 0.1f);
    Color defaultColor;
    private float deathTimer;
    public float deathTime;
    private bool dying;
    

    SpriteRenderer srr;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(xspeed, 0f);
        player = GameObject.FindWithTag("Player").transform;
        lasertimer = 0;
        health = healthMax;
        srr = GetComponent<SpriteRenderer>();
        defaultColor = srr.color;
        deathTimer = deathTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (!pauser1.paused)
        {
            righttouching = Physics2D.OverlapCircle(righttransform.position, rightcheckradius, whatisright);
            lefttouching = Physics2D.OverlapCircle(lefttransform.position, leftcheckradius, whatisleft);
            var distance1 = Vector3.Distance(player.position, this.transform.position);
            damage = Physics2D.OverlapCircle(transform.position, damagecheckradius, whatisdamage);




            //DYING
            

            if (dying)
            {
                rb.velocity = new Vector2(0f, 0f);
                if (deathTimer == deathTime)
                {
                    Destroy(gameObject.GetComponent<Rigidbody>());
                }
                deathTimer--;
                if (deathTimer % 10 == 0 || deathTimer == 0)
                {
                    var xvar = transform.position.x + Random.Range(-1f, 1f);
                    var yvar = transform.position.y + Random.Range(-1f, 1f);
                    soulclone = Instantiate(soul, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    explosionclone = Instantiate(explosion, new Vector3(xvar, yvar, transform.position.z - 1f), Quaternion.identity);
                    transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z + Random.Range(-45f, 45f));
                    as1.PlayOneShot(hitSound);
                }
                if (deathTimer <= 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        var xvar = transform.position.x + Random.Range(-1f, 1f);
                        var yvar = transform.position.y + Random.Range(-1f, 1f);
                        explosionclone = Instantiate(explosion, new Vector3(xvar, yvar, transform.position.z - 1f), Quaternion.identity);
                    }
                    Destroy(gameObject);
                }else
                if (deathTimer % 2 == 0)
                {
                    if (deathTimer % 4 == 0)
                    {
                        srr.color = Color.black;
                    }
                    else
                    {
                        srr.color = Color.white;
                    }
                }
            }





            if (!dying)
            {
                if (distance1 < awakedistance)
                {
                    lasertimer--;

                    if (lasertimer <= 0)
                    {
                        laserclone = Instantiate(laser, new Vector2(rb.position.x, rb.position.y), Quaternion.identity);
                        as1.PlayOneShot(shoot);
                        lasertimer = lasertimermax;
                    }

                }


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

                //Damage and Invincibility
                if (invincibilityTimer > 0)
                {
                    rb.velocity = new Vector2(0f, 0f);
                    invincibilityTimer--;
                    if (invincibilityTimer % 2 == 0)
                    {
                        if (invincibilityTimer % 4 == 0)
                        {
                            srr.color = Blink1;
                        }
                        else
                        {
                            srr.color = Blink2;
                        }
                    }
                }
                else
                {

                    srr.color = defaultColor;
                }


            
                


                if (damage && invincibilityTimer <= 0)
                {
                    health--;
                    invincibilityTimer = invincibilityTime;
                    for (int i = 0; i < 3; i++)
                    {
                        var xvar = transform.position.x + Random.Range(-1f, 1f);
                        var yvar = transform.position.y + Random.Range(-1f, 1f);
                        explosionclone = Instantiate(explosion, new Vector3(xvar, yvar, transform.position.z - 1f), Quaternion.identity);

                    }
                    as1.PlayOneShot(hitSound);

                    if (health <= 0)
                    {
                        dying = true;
                        transform.gameObject.tag = "Untagged";
                        gameObject.layer = 0;
                    }

                }
            }


        }
            
            
    }
}
