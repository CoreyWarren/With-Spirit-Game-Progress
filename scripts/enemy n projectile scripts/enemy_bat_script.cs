using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bat_script : MonoBehaviour
{
    //Reward
    [SerializeField]
    private float energyReward;
    Player_Stats_Storage playerStats;

    public int currentHealth;
    public int maxHealth;

    private Weapon_Stats weaponStats;

    Rigidbody2D rb;
    public Transform player;
    public float movespeed;
    private bool active;
    private int activetimer = 0;
    public int activetimemax = 50;
    public int resttime = 120;
    public int restingtimer = 0;
    public AudioSource aso;
    public AudioClip squeaksound;
    public AudioClip invincible;
    SpriteRenderer srr;
    [SerializeField]
    private AudioClip hitSound;
    public AudioClip death;
    private float playeryPosVar;
    private float playerxPosVar;
    private Vector2 defaultScale;
    private Vector2 restingScale;
    //Explosion
    public GameObject explosion;
    GameObject explode;
    //Soul Pickup
    public GameObject soul;
    GameObject makesoul;

    private bool toofar;
    public int toofarValue;

    //Damage Checkers
    public float damageCheckRadius;
    public Transform damageCheck;

    public LayerMask whatIsSword;
    private bool swordDamage;
    
    public LayerMask whatIsMissile;
    private bool missileDamage;

    //Death
    private int deathTimer = 0;
    public int deathTimermax;
    private bool startDeath = false;
    float xposdeathRand = 0;
    float yposdeathRand = 0;
    float zrotdeathRand = 0;

    //Colors
    Color blink = new Color(0.5f, 0.5f, 0.5f, 1f);
    Color normal = new Color(1f, 1f, 1f);

    //invincibility
    float invinTimer = 0;
    float invinTimerMax = 20;
    bool invinShow;

    //Wall Collision

    //Movement
    public float jumpHeight;
    public float jumpHeightMultiplier;
    public float jumpWidth;
    Animator a;
    private float defaultGravity;
    public float maxXSpeed;
    public float maxYSpeed;
    private bool animIdle;
    public Sprite idleSprite;
    public Sprite flyingSprite;


    // Use this for initialization
    void Start()
    {
        weaponStats = GameObject.FindWithTag("Event System").GetComponent<Weapon_Stats>();
        damageCheck = this.gameObject.transform;
        player = GameObject.FindWithTag("Player").transform;
        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
        aso = GetComponent<AudioSource>();
        playerxPosVar = 0;
        playeryPosVar = 0;
        invinShow = false;
        defaultScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
        restingScale = new Vector2(this.transform.localScale.x / 1.5f, this.transform.localScale.y / 1.5f);
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
        animIdle = true;
        srr = GetComponent<SpriteRenderer>();
        deathTimer = 0;
        currentHealth = maxHealth;
        Physics.IgnoreLayerCollision(11, 14);
    }

    // Update is called once per frame
    void Update()
    {
        if (pauser1.paused == false)
        {
            if(player != null)
            {
                ///////MOVEMENT///////////
                if (Mathf.Abs(transform.position.y - player.position.y) > toofarValue ||
                Mathf.Abs(transform.position.x - player.position.x) > toofarValue)
                {
                    toofar = true;
                    srr.color = blink;
                    rb.gravityScale = 0;
                    if (!animIdle)
                    {
                        //a.Play(flying.clip.name);
                        srr.sprite = idleSprite;
                        animIdle = true;
                    }
                    rb.velocity = new Vector2(0f, 0f);
                }
                else
                {
                    toofar = false;
                    srr.color = normal;
                    rb.gravityScale = defaultGravity;
                    if (animIdle)
                    {
                        animIdle = false;
                        srr.sprite = flyingSprite;
                    }
                }


                if (!toofar)
                {
                    if (active && !startDeath)
                    {
                        if (animIdle)
                        {
                            //a.Play(idle.clip.name);
                            srr.sprite = idleSprite;
                        }
                        else
                        { srr.sprite = flyingSprite; }

                        if (activetimer % 20 == 0)
                        {
                            //X
                            if (player.position.x + playerxPosVar < transform.position.x) //If player TARGET is to the left...
                            {
                                rb.velocity += new Vector2(-jumpWidth, 0f);
                            }
                            else if (player.position.x + playerxPosVar > transform.position.x)  //If player TARGET is to the right...
                            {
                                rb.velocity += new Vector2(jumpWidth, 0f);
                            }
                            //Y
                            if (player.position.y + playeryPosVar < transform.position.y) //If player TARGET is below...
                            {
                                rb.velocity += new Vector2(0f, jumpHeight / jumpHeightMultiplier);
                            }
                            else if (player.position.y + playeryPosVar > transform.position.y) //If player TARGET is above...
                            {
                                rb.velocity += new Vector2(0f, jumpHeight);
                            }
                        }

                        if (rb.velocity.x > maxXSpeed)
                        {
                            rb.velocity = new Vector2(maxXSpeed, rb.velocity.y);
                        }
                        else
                        if (rb.velocity.x < -maxXSpeed)
                        {
                            rb.velocity = new Vector2(-maxXSpeed, rb.velocity.y);
                        }

                        if (rb.velocity.y > maxYSpeed)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, maxYSpeed);
                        }
                        else
                        if (rb.velocity.y < -maxYSpeed)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, -maxYSpeed);
                        }



                        if (Mathf.Abs(transform.position.y - player.position.y) < movespeed) //If player TARGET is very close (Y)
                        {
                            if (transform.position.y > player.position.x)
                                transform.position = new Vector3(transform.position.x, transform.position.y - Mathf.Abs(transform.position.y - player.position.y), transform.position.z);
                            if (transform.position.y < player.position.x)
                                transform.position = new Vector3(transform.position.x, transform.position.y - Mathf.Abs(transform.position.y - player.position.y), transform.position.z);
                        }
                        if (Mathf.Abs(transform.position.x - player.position.x) < movespeed) //If player TARGET is very close (X)
                        {
                            if (transform.position.x > player.position.x)
                                transform.position = new Vector3(transform.position.x - (transform.position.x - player.position.x), transform.position.y, transform.position.z);
                            if (transform.position.x < player.position.x)
                                transform.position = new Vector3(transform.position.x + (transform.position.x - player.position.x), transform.position.y, transform.position.z);
                        }


                        activetimer--;
                        if (activetimer % 20 == 0)
                        {
                            aso.PlayOneShot(squeaksound);

                        }

                        if (activetimer < 1)
                        {
                            restingtimer = resttime;
                            active = false;

                        }
                    }//end of if (active)


                    if (!active)
                    {
                        //Sit Still.
                        //a.Play(idle.clip.name
                        srr.sprite = idleSprite;
                        if (transform.localScale != new Vector3(restingScale.x, restingScale.y, transform.localScale.z))
                        {
                            transform.localScale = restingScale;
                        }


                        restingtimer--;
                        srr.color = new Color(1f, 1f, .8f, 0.5f);
                        if (restingtimer < 1)
                        {
                            active = true;
                            activetimer = activetimemax;
                            activetimer += Random.Range(-5, 5);
                            playerxPosVar = Random.Range(-1f, 1f);
                            playeryPosVar = Random.Range(-1f, 1f);
                            transform.localScale = defaultScale;
                        }

                    }

                }//end if (!toofar)


                ///////////DEATH////////
                swordDamage = Physics2D.OverlapCircle(damageCheck.position, damageCheckRadius, whatIsSword);
                missileDamage = Physics2D.OverlapCircle(damageCheck.position, damageCheckRadius, whatIsMissile);
                //Start Death Sequence

                //Regular Damage
                if (swordDamage == true && startDeath == false && !toofar && active)
                {
                    aso.PlayOneShot(hitSound);


                    if (currentHealth <= 0 || (currentHealth - weaponStats.weaponDamage[0] <= 0))
                    {
                        currentHealth = 0;
                        startDeath = true;
                        gameObject.layer = 17; //"EnemyDead"
                        gameObject.tag = "Untagged";
                        Physics2D.IgnoreLayerCollision(17, 11);
                    }
                    else
                    {
                        active = false;
                        restingtimer = 20;
                        currentHealth -= weaponStats.weaponDamage[0];
                    }
                }
                else if (swordDamage == true && startDeath == false && !toofar && !active && invinTimer == 0)
                {
                    //INVINCIBLE
                    aso.PlayOneShot(invincible);
                    invinTimer = 20;
                }

                //Bonus Damage
                if (missileDamage == true && startDeath == false && !toofar && active)
                {
                    aso.PlayOneShot(hitSound);

                    if (currentHealth <= 0 || (currentHealth - weaponStats.weaponDamage[1] <= 0))
                    {
                        currentHealth = 0;
                        startDeath = true;
                        gameObject.layer = 17; //"EnemyDead"
                        gameObject.tag = "Untagged";
                        Physics2D.IgnoreLayerCollision(17, 11);
                    }
                    else
                    {
                        currentHealth -= weaponStats.weaponDamage[1];
                    }
                }
                else if (missileDamage == true && startDeath == false && !toofar && !active && invinTimer == 0)
                {
                    //INVINCIBLE
                    aso.PlayOneShot(invincible);
                    invinTimer = 20;
                }


                if (invinTimer > 0)
                {
                    srr.color = new Color(1f, 0f, 1f, 1f);
                    invinTimer--;
                }

                //Death Sequence
                if (startDeath)
                {
                    if (deathTimer == 0)
                    {
                        makesoul = Instantiate(soul,
                            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(0, 0, Random.Range(-30, 30))) as GameObject;
                        xposdeathRand = Random.Range(-0.08f, 0.08f);
                        yposdeathRand = Random.Range(-0.04f, 0f);
                        zrotdeathRand = Random.Range(-1f, 1f);
                        srr.color = new Color(.2f, .2f, 0f, 1f);
                        if (playerStats.playerEnergy + energyReward <= playerStats.playerEnergyMax)
                        {
                            playerStats.playerEnergy += energyReward;
                            Debug.Log("Giving you energy.");
                        }
                        else
                        { playerStats.playerEnergy = playerStats.playerEnergyMax; }
                    }

                    deathTimer++;

                    if (deathTimer % 2 == 0)
                    {
                        explode = Instantiate(explosion,
                            new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z),
                            Quaternion.Euler(0, 0, Random.Range(0, 180))) as GameObject;
                        srr.color = new Color(1f, 0f, 1f, 1f);
                        transform.position = new Vector3(transform.position.x + xposdeathRand + Random.Range(-0.2f, 0.2f),
                                                    transform.position.y + yposdeathRand + Random.Range(-0.2f, 0.2f), transform.position.z);

                        // X scale
                        if (transform.localScale.x >= 0)
                        {
                            transform.localScale = new Vector2(transform.localScale.x + Random.Range(-0.2f, 0.1f), transform.localScale.y);
                        }
                        else
                        {
                            transform.localScale = new Vector2(0.05f, transform.localScale.y);
                            transform.localScale = new Vector2(transform.localScale.x + Random.Range(-0.2f, 0.1f), transform.localScale.y);
                        }

                        // Y scale
                        if (transform.localScale.y >= 0)
                        {
                            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + Random.Range(-0.4f, 0.1f));
                        }
                        else
                        {
                            transform.localScale = new Vector2(transform.localScale.x, 0.05f);
                            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + Random.Range(-0.4f, 0.1f));
                        }

                    }
                    else
                    {
                        srr.color = new Color(.2f, .2f, 0f, 1f);
                    }




                    transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + zrotdeathRand + Random.Range(-0.5f, 0.5f));

                    if (transform.localScale.x >= 0 && transform.localScale.y >= 0)
                    {
                        transform.localScale = new Vector2(transform.localScale.x - 0.008f, transform.localScale.y - 0.008f);
                    }


                    if (deathTimer == deathTimermax)
                    {
                        //Deletion (by player killing this)
                        Destroy(gameObject, 0);

                    }

                }


            }
        }
            
    }//end of update
}