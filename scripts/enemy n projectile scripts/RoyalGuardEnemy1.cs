using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalGuardEnemy1 : MonoBehaviour
{
    Weapon_Stats weaponStatsScript;
    private Rigidbody2D rb1;
    private AudioSource as1;
    private SpriteRenderer srr;
    [SerializeField]
    private Transform groundCheckTransform, wallCheckLeftTransform, wallCheckRightTransform;
    private Transform playerTransform;
    Player_Stats_Storage playerStats;

    [SerializeField]
    private GameObject projectile, soulDrop, explosion;
    [SerializeField]
    private AudioClip swordSound, damagedSound;
    [SerializeField]
    private Sprite throwingSprite, hurtSprite;
    private Sprite defaultSprite;

    [SerializeField]
    private float jumpHeight, jumpLength, moveSpeed, swordPeriod, swordDelay, maxHorizontalSpeed, swordCountMax, hitRecoilX, hitRecoilY, axeYOffset, pickupThrowVelocity; 

    private bool grounded, jumping, swordDamage, missileDamage, throwingSwords, nearPlayer, actionSet1, actionSet2, myDeath;
    private bool wallCheckLeft, wallCheckRight;

    [SerializeField]
    private int health, healthMax;

    [SerializeField]
    private float playerCheckRadius, groundCheckRadius, wallCheckRadius, damageCheckRadius;
    [SerializeField]
    private LayerMask whatIsPlayer, whatIsGround, whatIsSword, whatIsMissile;

    private float timer1, swordTimer, timer3, invulnTimer, swordCount, deathTimer;
    [SerializeField]
    private float timer1Max, timer2Max, timer3Max, invulnTimerMax, deathTimerMax, deathSoundPeriod, deathTimeDelayed;

    [SerializeField]
    private float energyReward;
    [SerializeField]
    private int soulDropReward;

    private Vector3 deathPosition;
    private int deathSoundCount;
    private float[] deathSoundTime = new float[3];


    //Extra stuffs
    Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        
        
        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
        health = healthMax;
        nearPlayer = false;
        actionSet1 = false;
        jumping = false;
        invulnTimer = 0f;
        grounded = false;
        throwingSwords = false;
        timer1 = 0; swordTimer = 0; timer3 = 0;
        rb1 = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        as1 = GetComponent<AudioSource>();
        srr = GetComponent<SpriteRenderer>();
        defaultColor = srr.color;
        myDeath = false;
        deathSoundTime[0] = deathTimerMax / 4 * 3;
        deathSoundTime[1] = deathTimerMax / 2;
        deathSoundTime[2] = deathTimerMax / 4;
        deathSoundCount = 0;
        defaultSprite = srr.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauser1.paused == false)
        {
            if (weaponStatsScript == null)
            {
                weaponStatsScript = GameObject.FindWithTag("Event System").GetComponent<Weapon_Stats>();
            }

            //Booleans
            //Booleans
            //Booleans

            nearPlayer      = Physics2D.OverlapCircle(this.transform.position, playerCheckRadius, whatIsPlayer);
            grounded        = Physics2D.OverlapCircle(groundCheckTransform.transform.position, groundCheckRadius, whatIsGround);
            wallCheckLeft   = Physics2D.OverlapCircle(wallCheckLeftTransform.transform.position, wallCheckRadius, whatIsGround);
            wallCheckRight  = Physics2D.OverlapCircle(wallCheckRightTransform.transform.position, wallCheckRadius, whatIsGround);
            swordDamage     = Physics2D.OverlapCircle(transform.position, damageCheckRadius, whatIsSword);
            missileDamage   = Physics2D.OverlapCircle(transform.position, damageCheckRadius, whatIsMissile);



            //Timers
            //Timers
            //Timers

            if (timer1 > 0)
            {
                timer1 -= Time.deltaTime;
            }
            else
            {
                timer1 = 0;
                actionSet1 = false;
            }





            //Action Sets
            //Action Sets
            //Action Sets
            if(!myDeath)
            {
                if (nearPlayer && !actionSet1)
                { actionSet1 = true; }

                if (!nearPlayer && actionSet1)
                {
                    actionSet1 = false;
                    actionSet2 = false;
                    swordCount = 0;
                    timer1 = 0;
                    swordTimer = 0;
                    throwingSwords = false;
                }

                if (actionSet1 && timer1 <= 0 && grounded)
                {
                    timer1 = timer1Max;

                    if (playerTransform.position.x > transform.position.x)
                    { rb1.velocity = new Vector2(jumpLength, jumpHeight); }

                    if (playerTransform.position.x < transform.position.x)
                    { rb1.velocity = new Vector2(-jumpLength, jumpHeight); }
                }


                if (actionSet1 && timer1 > 0)
                {

                    if (playerTransform.position.x > transform.position.x && !wallCheckRight)
                    { rb1.velocity = new Vector2(rb1.velocity.x + moveSpeed, rb1.velocity.y); }

                    if (playerTransform.position.x < transform.position.x && !wallCheckLeft)
                    { rb1.velocity = new Vector2(rb1.velocity.x - moveSpeed, rb1.velocity.y); }

                    //Facing direction left or right
                    if(rb1.velocity.x < 0 && playerTransform.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector2(1f, transform.localScale.y);
                    }else
                    if(rb1.velocity.x > 0 && playerTransform.position.x > transform.position.x)
                    {
                        transform.localScale = new Vector2(-1f, transform.localScale.y);
                    }

                    //Timer2 is encapsulated within timer1's range
                    if (!grounded && !throwingSwords && swordTimer <= 0)
                    {
                        throwingSwords = true;
                        swordCount = 0;
                        swordTimer = swordDelay;
                    }
                    else
                    if (grounded)
                    {
                        throwingSwords = false;
                        swordCount = 0;
                        swordTimer = 0;
                        if(timer1 > timer1Max - 2)
                        {
                            actionSet1 = false;
                        }
                    }

                    if (swordTimer <= 0 && throwingSwords && swordCount < swordCountMax)
                    {
                        GameObject axe;
                        axe = Instantiate(projectile, transform.position + new Vector3(0f, axeYOffset, 0f), Quaternion.identity);
                        Physics2D.IgnoreCollision(axe.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                        if (playerTransform.position.x > transform.position.x)
                        {
                            axe.GetComponent<axeProjectileEnemy>().goingRight = true;
                        }
                        else
                        {
                            axe.GetComponent<axeProjectileEnemy>().goingRight = false;
                        }
                        as1.PlayOneShot(swordSound);
                        swordCount++;
                        swordTimer = swordPeriod;
                    }

                    if (throwingSwords && swordTimer > 0)
                    {
                        swordTimer -= Time.deltaTime * 10;

                        if(swordTimer > swordPeriod/2 && srr.sprite != throwingSprite)
                        {
                            srr.sprite = throwingSprite;
                        }
                        if(swordTimer < swordPeriod/2 && srr.sprite != defaultSprite)
                        {
                            srr.sprite = defaultSprite;
                        }
                    }

                    if(!throwingSwords && srr.sprite != defaultSprite)
                    {
                        srr.sprite = defaultSprite;
                    }
                    
                }

                if (invulnTimer > 0)
                {
                    //Flash to indicate invulnerability
                    float green = (float)health / (float)healthMax;
                    srr.color = new Color(1f, green, 0f); //red
                    invulnTimer -= Time.deltaTime;

                    //SPRITE
                    if(srr.sprite != hurtSprite && invulnTimer > invulnTimerMax/2)
                    {
                        srr.sprite = hurtSprite;
                    }

                }

                else if (srr.color != defaultColor)
                {
                    srr.color = defaultColor;
                }

                if (swordDamage || missileDamage)
                {
                    
                    if (invulnTimer <= 0)
                    {
                        if (swordDamage)
                        {
                            //Debug.Log("Damage Taken:" + weaponStatsScript.weaponDamage[0]);
                            health -= weaponStatsScript.weaponDamage[0];
                        }
                        else if(missileDamage)
                        {
                            //Debug.Log("Damage Taken:" + weaponStatsScript.weaponDamage[1]);
                            health -= weaponStatsScript.weaponDamage[1];
                        }
                        
                        //Debug.Log("Damaged the Royal Guard once");
                        invulnTimer = invulnTimerMax;
                        as1.PlayOneShot(damagedSound);
                        if (playerTransform.position.x < transform.position.x)
                        {
                            rb1.velocity = new Vector2(hitRecoilX, hitRecoilY);
                        }
                        else
                        {
                            rb1.velocity = new Vector2(-hitRecoilX, hitRecoilY);
                        }
                    }
                }


                


            }//endof !myDeath check
            

            
            
            //Check-ins
            if(rb1.velocity.x > maxHorizontalSpeed)
            {
                rb1.velocity = new Vector2(rb1.velocity.x - (rb1.velocity.x - maxHorizontalSpeed)/2, rb1.velocity.y);
            }else if (rb1.velocity.x < -maxHorizontalSpeed)
            {
                rb1.velocity = new Vector2(rb1.velocity.x + (-maxHorizontalSpeed - rb1.velocity.x) / 2, rb1.velocity.y);
            }

            

            if(health <= 0 && !myDeath)
            {
                deathTimer = deathTimerMax;
                myDeath = true;
                as1.pitch -= 0.3f;
                deathPosition = transform.position;
                GetComponent<Collider2D>().enabled = false;
                rb1.velocity = new Vector3(0f, 0f, 0f);
            }

            if (myDeath)
            {

                //SPRITE
                if (srr.sprite != hurtSprite)
                {
                    srr.sprite = hurtSprite;
                }

                deathTimer -= Time.deltaTime * 10;

                float deathTimePercentage;
                deathTimePercentage = deathTimer / deathTimerMax;
                if (deathTimer < deathSoundTime[0])
                {
                    if (deathSoundCount == 0)
                    {
                        as1.PlayOneShot(damagedSound);
                        deathSoundCount = 1;

                        generateRewards();
                    }
                    else
                    {
                        if (deathSoundCount == 1)
                        {
                            if (deathTimer < deathSoundTime[1])
                            {
                                as1.PlayOneShot(damagedSound);
                                deathSoundCount = 2;
                                generateRewards();

                            }
                        }
                        else
                        {
                            if (deathSoundCount == 2)
                            {
                                if (deathTimer < deathSoundTime[2])
                                {
                                    as1.PlayOneShot(damagedSound);
                                    deathSoundCount = 3;
                                    generateRewards();
                                }
                            }
                        }
                    }
                }

                if (deathSoundCount >= 1)
                {
                    srr.color = new Color(Random.Range(0.8f, 1f), Random.Range(0f, 1f), Random.Range(0f, 0.3f), Random.Range(0.5f, 1f));
                    as1.pitch += 0.02f;
                }

                transform.position = new Vector3(deathPosition.x + Random.Range(-0.03f, 0.03f), deathPosition.y + Random.Range(-0.03f, 0.03f), deathPosition.z);

                if (deathTimer <= 0)
                {


                    srr.sprite = null;
                    if(deathTimer <= -deathTimeDelayed)
                    {
                        Destroy(gameObject);
                    }
                }
                
                }
            }
        }

    public void generateRewards()
    {
        if (playerTransform != null)
        {

            if (playerStats.playerEnergy + energyReward <= playerStats.playerEnergyMax)
            { playerStats.playerEnergy += energyReward; }
            else
            { playerStats.playerEnergy = playerStats.playerEnergyMax; }

        }
        else
        {
            Debug.Log("Player Transform doesn't exist, dude.");
        }

        Instantiate(soulDrop, transform.position, Quaternion.identity);
    }

}


