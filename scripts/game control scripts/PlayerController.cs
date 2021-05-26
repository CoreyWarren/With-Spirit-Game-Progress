using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {



    //Externally Referring Thingies
    public SpriteRenderer srr;
    public Rigidbody2D rb;
    public Animator a;
    Player_Stats_Storage playerStats;

    //Normal Sprites
    public Sprite defaultsprite1;
    public Sprite defaultsprite2;
    public Sprite leftsprite1;
    public Sprite leftspritewallride1;
    public Sprite leftspritewallridehi1;
    public Sprite rightsprite1;
    public Sprite rightspritewallride1;
    public Sprite rightspritewallridehi1;
    public Sprite fastfall1;
    public Sprite fastfallR1;
    public Sprite fastfallL1;
    public Sprite groundingRight;
    public Sprite groundingLeft;
    public Sprite groundingDown;

    public Sprite inPainSprite;
    public static string spr;

    private Vector3 defaultScale;

    //Audio
    private AudioSource audio1; //Jump
    public AudioSource audio2; //Quieter //*wow thanks, past me! */
    public AudioClip jumpsound;
    public AudioClip painsound;
    public AudioClip healthsound;
    public AudioClip walljumpsound;
    public AudioClip dyingsound;
    public AudioClip missileLaunch1;
    public AudioClip swordswing;
    public AudioClip no_ammo;
    public AudioClip landingsound;
    private float startingPitch;

    //On the Ground
    public Transform groundcheck;
    public float groundcheckradius;
    public LayerMask whatisground;
    private bool grounded;
    private bool airbourne;
    public GameObject trail2;
    GameObject landingDustMake;

    //Left/Right Movement
    public float moveacceleration;
    //Wallriding
    public float leftrightfriction;
    public Transform wallcheckleft;
    public Transform wallcheckright;
    public float wallcheckradius;
    public LayerMask whatiswall;
    private bool wallridingleft;
    private bool wallridingright;
    public float walljumphorizontal;
    public float walljumpheight;
    public static bool faceright;
    public static bool faceleft;
    //Grounding
    private bool grounding;
    private float groundingTimer;
    public float groundingTimerMax;


    //Physics
    public float gravity;
    public float movespeed;
    public float maxfallspeed;
    public float maxfallspeedwallriding;
    public float jumpheight;
    public float jumptime;
    public float jumptimemax;
    private bool walljumpingL;
    private bool walljumpingR;
    private bool doublejumped;
    private float gamespeed;
    private bool jumping;

    //Death
    public Transform deadcheck;
    public float deadcheckradius;
    public LayerMask whatisdeathobstacle;
    public bool dead;

    //Damage!
    private int myHealth;
    public Transform paincheck;
    private float paintimer;
    public int painspritetimer;
    public int painspritetimermax;
    private bool painspritebool = true;
    public float paintimermax;
    public LayerMask whatispain;
    public float paincheckradius;
    private bool pain;
    
    private int sprBT;
    public int sprBTmax1;
    public int sprBTmax2;
    public int sprBTmax3;
    Color neutral = new Color(1F, 1F, 1F);
    Color yellow = new Color(1F, 0.8F, 0.8F);
    Color orange = new Color(1F, 0.6F, 0.5F);
    Color red = new Color(1F, 0.5F, .8F);
    public AudioClip soullost;
    public float damageJumpFalling;
    public float damageJumpUpward;

    //Trail
    private float sparktimer;
    public float sparktimerset;
    private float sparktimeroffset;
    public float sparktimeroffsetmin;
    public float sparktimeroffsetmax;
    private float sparkzdifference = 2;
    public float trailxmin;
    public float trailxmax;
    public float trailymin;
    public float trailymax;
    public GameObject trail;
    GameObject trailclone;
    public int painTrailMult;

    //Objectives && Statuses
    public static bool soulgot;
    public float sococheckradius;
    public LayerMask whatissoco;
    public static bool socotouch;
    public Transform sococheck;
    public static int soulcount; // Helps with deleting the correct trav_soul
    public int soulcount1;
    GameObject clonetravsoul;
    public GameObject travsoul;
    public static int travelSoulMax = 15;
    public GameObject questionMark;
    
    public float energyRegen;


    [SerializeField]
    private float energyRegenPeriod, energyRegenPeriodMax, energyRegenBonus, energyRegenBonusThreshold;
    [SerializeField]
    private float[] energyWeaponCosts = new float[4];
    [SerializeField]
    private float[] energyWeaponEPause = new float[4];


    //Health Getting
    public static bool soultouch;
    public int soultimer;
    public int soultimermax = 16;
    public Transform soulPrefab;
    public GameObject plusOne;
    GameObject plusOneClone;


    //Blinking Sprites for Health Get
    Color healthget1 = new Color(0.8F, 0.5F, 1F);
    Color healthget2 = new Color(1F, 1F, 0.5F);
    public int blinktimer;
    public int blinktimermax = 4;
    public bool blinking;

    //Death 
    private int deathtimer;
    private int deathtimermax = 50;
    private bool dying = false;
    public Sprite dyingSprite;

    //Weapons
    GameObject makeWep;
    public GameObject missile1;
    public GameObject sword1;

    //Trail
    [SerializeField]
    private GameObject faderObject;
    [SerializeField]
    private float faderSpawnTime, faderFadeTime, faderStartingOpacity;
    private float faderSpawnClock;


    public static int TravelSoulMax
    {
        get
        {
            return travelSoulMax;
        }

        set
        {
            travelSoulMax = value;
        }
    }

    GameObject FindEnemy()
    {
        GameObject go;
        go = GameObject.FindGameObjectWithTag("Enemy");
        return go;
    }

    ///////////Initialization/////////////

    void Start ()
    {
        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
        myHealth = playerStats.playerHealth;
        soulcount = playerStats.playerSouls;
        rb = GetComponent<Rigidbody2D>();
        audio1 = GetComponent<AudioSource>();
        startingPitch = audio1.pitch;
        sparktimer = 0;
        deathtimer = deathtimermax;
        defaultScale = transform.localScale;
        grounding = false;
        a = GetComponent<Animator>();


        jumping = false;
        
    }

    //////////// Update--called once per frame////////////////

    void Update()
    {
        


        if (pauser1.paused == false)
        {
            gamespeed = Time.timeScale;
            Time.maximumDeltaTime = gamespeed;
            Time.fixedDeltaTime = gamespeed * 0.02f;

            if (dying)
            {
                srr.sprite = dyingSprite;
                spr = "dying";
                if (deathtimer == 0)
                { restartCurrentScene(); }
                deathtimer--;
                Gravity();
                horizontalFriction();
            }
            else
            {
                if (dead)
                {
                    restartCurrentScene();
                }
                grounded = Physics2D.OverlapCircle(groundcheck.position, groundcheckradius, whatisground);
                wallridingleft = Physics2D.OverlapCircle(wallcheckleft.position, wallcheckradius, whatiswall);
                wallridingright = Physics2D.OverlapCircle(wallcheckright.position, wallcheckradius, whatiswall);
                pain = Physics2D.OverlapCircle(paincheck.position, paincheckradius, whatispain);
                socotouch = Physics2D.OverlapCircle(sococheck.position, sococheckradius, whatissoco);

                soulcount1 = playerStats.playerSouls;
                horizontalFriction();
                

                

                if (airbourne && grounded 
                    //&& rb.velocity.y < (-maxfallspeed + (maxfallspeed / 3 * 2))
                    )
                {
                    audio1.pitch = startingPitch + Random.Range(-0.2f, 0.2f);
                    audio2.pitch = audio1.pitch;
                    audio2.PlayOneShot(landingsound);
                    for (int i = 0; i < 3; i++)
                    {
                        landingDustMake = Instantiate(trail2, new Vector3(transform.position.x + Random.Range(-.5f, .5f),
                           transform.position.y - .55f, transform.position.z + 1f), Quaternion.identity) as GameObject;
                    }
                    landingDustMake.transform.localScale = new Vector2(0.05f, 0.05f);
                    airbourne = false;
                    grounding = true;
                }

                if (grounded)
                {

                    if (Input.GetKeyDown("z"))
                    {
                        jumptime = jumptimemax;
                        jumping = true;
                    }
                }
                else
                {
                    airbourne = true;
                }
                



                //Constant Functions
                Gravity();




                //Jumping
                if (Input.GetKeyDown("z") && ((wallridingleft && Input.GetKey(KeyCode.LeftArrow)) ||
                    (wallridingright && Input.GetKey(KeyCode.RightArrow))))
                {
                    jumptime = jumptimemax;
                }

                if (!Input.GetKey("z"))
                {
                    jumptime = 0;
                    if(jumping)
                    {
                        if (rb.velocity.y > 0f)
                        { rb.velocity = new Vector2(rb.velocity.x, 0f); }
                        jumping = false;
                    }
                }


                if (jumptime == 0)
                {
                    walljumpingR = false;
                    walljumpingL = false;
                }
                //If the player should be jumping:
                else if (jumptime > 0 && Input.GetKey("z"))
                {
                    if (grounded)
                    {
                        Jump();
                    }
                    else if (wallridingleft && !grounded && Input.GetKey("z") && Input.GetKey(KeyCode.LeftArrow) 
                        && jumptime == jumptimemax)
                    {
                        //(If the wallriding transform detects a wall in its radius, and the player character is
                        //not grounded, and if they are holding Z, AND the jumptime == jumptimemax, which means
                        //that the player had released Z at some point, and pressed it again.

                        //NOTE: BIG ISSUE SOLVED
                        //GroundCheck radius was too large,
                        //which did not allow for proper walljumping.
                        //Walljumping was inhibited because the player character was always 'grounded.'
                        WallJumpL();
                        walljumpingL = true;
                    }
                    else if (wallridingright && !grounded && Input.GetKey("z") && Input.GetKey(KeyCode.RightArrow) 
                        && jumptime == jumptimemax)
                    {
                        WallJumpR();
                        walljumpingR = true;
                    }
                    else if (walljumpingL)
                    {
                        WallJumpL();
                    }
                    else if (walljumpingR)
                    {
                        WallJumpR();
                    }
                    else if (!grounded)
                    {
                        Jump();
                    }
                }

                //Left and Right Movement Options
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (!playercon2.strafing) //when strafing is off, change direction freely
                    {
                        faceleft = true;
                        faceright = false;
                    }
                    else //strafing is on, so only change direction on ground
                    {
                        if (grounded)
                        {
                        faceleft = true;
                        faceright = false;
                        }
                    }

                    if (!wallridingleft)
                        rb.velocity = new Vector2(rb.velocity.x - moveacceleration, rb.velocity.y);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (!playercon2.strafing)  //when strafing is off, change direction freely
                    {
                        faceleft = false;
                        faceright = true;
                    }
                    else //strafing is on, so only change direction on ground
                    {
                        if (grounded)
                        {
                            faceleft = false;
                            faceright = true;
                        }
                    }


                    if (!wallridingright)
                        rb.velocity = new Vector2(rb.velocity.x + moveacceleration, rb.velocity.y);
                }


                //Max Horiz Speed
                if (rb.velocity.x > movespeed)
                {
                    rb.velocity = new Vector2(movespeed, rb.velocity.y);
                }
                else if (rb.velocity.x < -movespeed)
                {
                    rb.velocity = new Vector2(-movespeed, rb.velocity.y);
                }
                

                //Max Fall Speed / Wallride Speed
                
                if (rb.velocity.y < -maxfallspeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -maxfallspeed);
                }
                if (rb.velocity.y < -maxfallspeedwallriding && ((wallridingleft && Input.GetKey(KeyCode.LeftArrow))
                    || (wallridingright && Input.GetKey(KeyCode.RightArrow))))
                {
                    rb.velocity = new Vector2(rb.velocity.x, -maxfallspeedwallriding);
                }





                //Instantiating Fader Trail

                
                    if (rb.velocity.y == -maxfallspeed)
                    {
                        if (faderSpawnClock <= 0)
                        {
                            faderSpawnClock = faderSpawnTime;
                            GameObject newFader;
                        if (rb.velocity.x == movespeed || rb.velocity.x == -movespeed)
                        {
                            Vector3 randoPosition = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 1f);
                            newFader = Instantiate(faderObject, randoPosition, Quaternion.identity);
                        }
                        else
                        {
                            newFader = Instantiate(faderObject, transform.position + new Vector3(0f, 0f, 1f), Quaternion.identity);
                        }
                        
                            newFader.GetComponent<SpriteRenderer>().sprite = srr.sprite;
                            newFader.GetComponent<faderScript>().fadeTime = faderFadeTime;
                            newFader.GetComponent<faderScript>().opacityPercent = faderStartingOpacity;
                            newFader.GetComponent<faderScript>().usingOpacityPercent = true;
                        }
                        else
                        {
                            faderSpawnClock -= Time.deltaTime * 100;
                        }
                    }
                    else if (rb.velocity.x == movespeed || rb.velocity.x == -movespeed)
                    {
                        if (faderSpawnClock <= 0)
                        {
                            faderSpawnClock = faderSpawnTime;
                            GameObject newFader;
                        
                            newFader = Instantiate(faderObject, transform.position + new Vector3(0f, 0f, 1f), Quaternion.identity);
                        
                            newFader.GetComponent<SpriteRenderer>().sprite = srr.sprite;
                            newFader.GetComponent<faderScript>().fadeTime = faderFadeTime;
                            newFader.GetComponent<faderScript>().opacityPercent = faderStartingOpacity;
                            newFader.GetComponent<faderScript>().usingOpacityPercent = true;

                        

                    }
                        else
                        {
                            faderSpawnClock -= Time.deltaTime * 100;
                        }
                    }
                    





                    //SPRITE CHANGES/////////////////////
                    if
                    ((rb.velocity.y < -maxfallspeed / 2) && (Input.GetKey(KeyCode.RightArrow)))
                {
                    //IF fall speed is fast enough and pressing D
                    srr.sprite = fastfallR1;
                    spr = "fastFallingRight";
                }
                else if
                    ((rb.velocity.y < -maxfallspeed / 2) && (Input.GetKey(KeyCode.LeftArrow)))
                {
                    //IF fall speed is fast enough and pressing A
                    srr.sprite = fastfallL1;
                    spr = "fastFallingLeft";
                }
                else if
                    (rb.velocity.y < -maxfallspeed / 2)
                {
                    //IF fall speed is fast enough, but not the above...
                    srr.sprite = fastfall1;
                    spr = "fastFallingDown";
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //If the player holds a,
                    //and exceeds over half movespeed in this direction,
                    //change the sprite to leftsprite1.
                    srr.sprite = leftsprite1;
                    spr = "Left";
                    a.Play("spirit_idle");
                    if (wallridingleft && grounded)
                        srr.sprite = leftspritewallride1;
                    else if (wallridingleft)
                    {
                        srr.sprite = leftspritewallridehi1;
                    }
                }
                else
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Same, but for right-facing sprites.
                    srr.sprite = rightsprite1;
                    a.Play("spirit_idle");
                    if (wallridingright && grounded)
                        srr.sprite = rightspritewallride1;
                    else if (wallridingright)
                    {
                        srr.sprite = rightspritewallridehi1;
                    }
                }
                else if(!grounded)
                {
                    srr.sprite = defaultsprite2;
                }else
                {
                    srr.sprite = defaultsprite1;
                }

                //Grounding anim
                if (grounding)
                {
                    if (groundingTimer <= 0)
                    {
                        groundingTimer = groundingTimerMax;
                    }
                    if (rb.velocity.x > 0)
                    {
                        srr.sprite = groundingRight;
                    }
                    else
                    if (rb.velocity.x < 0)
                    {
                        srr.sprite = groundingLeft;
                    }
                    else
                    {
                        srr.sprite = groundingDown;
                    }
                    groundingTimer--;
                    if (groundingTimer <= 0)
                    {
                        grounding = false;
                    }
                }

                if (jumptime == jumptimemax)
                {
                    if (grounded && !wallridingright && !wallridingleft)
                        audio1.PlayOneShot(jumpsound);
                    else if (wallridingright || wallridingleft)
                        audio1.PlayOneShot(walljumpsound);
                }
                jumptime -= Time.deltaTime * 10;
                //Sparks
                sparktimer--;
                if (sparktimer < 1)
                {
                    sparktimeroffset = Random.Range(sparktimeroffsetmin, sparktimeroffsetmax);
                    //trailclone = Instantiate(trail, new Vector3(transform.position.x + Random.Range(trailxmin, trailxmax),
                    //transform.position.y + Random.Range(trailymin, trailymax), transform.position.z + sparkzdifference),
                    //Quaternion.identity) as GameObject;
                    sparktimer = sparktimerset + sparktimeroffset;
                }
                if (pain && paintimer == 0)
                {
                    if (playerStats.playerHealth == 0)
                    {
                        dying = true;
                        audio1.PlayOneShot(dyingsound);
                        canvas_soulscript.uiPainTouch = true;
                    }
                    else
                    {
                        paintimer = paintimermax;
                        painspritetimer = painspritetimermax;
                        canvas_soulscript.uiPainTouch = true;
                        if (playerStats.playerSouls < 1)
                        {
                            playerStats.playerHealth--;
                            audio1.pitch += Random.Range(-.1f, .1f);
                            audio1.PlayOneShot(painsound);
                        }
                        else
                        {
                            if (playerStats.playerSouls >= 1)
                            { playerStats.playerSouls -= 1; }
                            //else
                            //{ soulcount = 0; }
                            audio1.PlayOneShot(soullost);
                        }
                        if(rb.velocity.y < 0)
                        { rb.velocity += new Vector2(0, damageJumpFalling); }
                        else
                        {
                          rb.velocity += new Vector2(0, damageJumpUpward);
                        }
                        
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                }

                //Sprite Color/Health
                painHealthAnim(); // (srr action)

                //SOUL TOUCH CHECK
                soulTouchCheck();

                if (blinktimer == 0 && soultimer > 0)
                {

                    if (blinking)
                    {
                        blinking = false;
                    }
                    else
                    {
                        blinking = true;
                    }
                    blinktimer = blinktimermax;
                }

                //HealthGet Blinking
                if (blinking && soultimer > 0)
                {
                    GetComponent<SpriteRenderer>().color = healthget1;
                    blinktimer--;
                }
                else if (!blinking && soultimer > 0)
                {
                    GetComponent<SpriteRenderer>().color = healthget2;
                    blinktimer--;
                }
                if (soultimer > 0)
                    soultimer--;

                if (Input.GetKey("x") && canvas_weaponUI.selectedW == 1)
                {
                    if (GameObject.FindGameObjectsWithTag("PlayerSword1").Length < 1 && playerStats.playerEnergy >= energyWeaponCosts[0])
                    {

                        playerStats.playerEnergy -= energyWeaponCosts[0];
                        energyRegenPeriod = energyWeaponEPause[0];

                        if ((faceright || wallridingleft) && !wallridingright && !grounded)
                        {
                            makeWep = Instantiate(sword1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 181)) as GameObject; //Right
                            audio2.PlayOneShot(swordswing);
                        }
                        else
                        if (faceleft || wallridingright && !grounded)
                        {
                            makeWep = Instantiate(sword1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 179)) as GameObject; //Left
                            audio2.PlayOneShot(swordswing);
                        }
                        else
                        {
                            makeWep = Instantiate(sword1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 181)) as GameObject; //Right
                            audio2.PlayOneShot(swordswing);
                        }
                    }else
                        if(playerStats.playerEnergy < energyWeaponCosts[0] && Input.GetKeyDown("x"))
                    {
                        audio1.PlayOneShot(no_ammo);
                    }
                }

                if (Input.GetKeyDown("x") && canvas_weaponUI.selectedW == 2 && playerStats.playerEnergy >= energyWeaponCosts[1])
                {
                    if (FindEnemy() != null)
                    {
                        playerStats.playerEnergy -= energyWeaponCosts[1];
                        energyRegenPeriod = energyWeaponEPause[1];
                        audio1.PlayOneShot(missileLaunch1);
                        makeWep = Instantiate(missile1, new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z - 1f), Quaternion.Euler(0, 0, 90)) as GameObject;
                        makeWep = Instantiate(missile1, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Quaternion.Euler(0, 0, 0)) as GameObject;
                        makeWep = Instantiate(missile1, new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z - 1f), Quaternion.Euler(0, 0, -90)) as GameObject;
                        makeWep = Instantiate(missile1, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Quaternion.Euler(0, 0, 180)) as GameObject;
                    }else
                    {
                        Instantiate(questionMark, transform.position, Quaternion.identity);
                        audio1.PlayOneShot(no_ammo);
                    }
                }
                else
                    if (Input.GetKeyDown("x") && playerStats.playerEnergy < energyWeaponCosts[1] && canvas_weaponUI.selectedW == 2)
                {
                    audio1.PlayOneShot(no_ammo);
                }

                
            }//end of alive actions
        }//end of non-paused actions
    }//end of UPDATE()
        
    public void IgnoreSoulCollisions()
    {
        Physics2D.IgnoreLayerCollision(11, 9);
        //9 is player
        //11 is soul
    }

    GameObject FindSword()
    {
        GameObject go;
        go = GameObject.FindGameObjectWithTag("PlayerSword1");
        return go;
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpheight);
    }
    public void WallJumpL()
    {
        rb.velocity = new Vector2(rb.velocity.x + walljumphorizontal, walljumpheight);
    }
    public void WallJumpR()
    {
        rb.velocity = new Vector2(rb.velocity.x - walljumphorizontal, walljumpheight);
    }
    public void Gravity()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y-gravity);
    }


    //Restarting due to death
    public void restartCurrentScene()
    {
        playerStats.playerDied = true;

    }

    public void soulTouchCheck()
    {
        if (soultouch) //is determined by deleted souls
        {
            if (playerStats.playerHealth == playerStats.playerHealthMax)
            {
                playerStats.playerSouls++;
                if(playerStats.playerSouls < 20)
                {clonetravsoul = Instantiate(travsoul, new Vector3(transform.position.x, transform.position.y, 4f), Quaternion.identity) as GameObject;}
            } else
            if (playerStats.playerHealth < playerStats.playerHealthMax)
            {
                playerStats.playerHealth++;
            }
            audio2.PlayOneShot(healthsound);
            plusOneClone = Instantiate(plusOne, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            soultimer = soultimermax;
            blinktimer = blinktimermax;
            blinking = true;
            soultouch = false;
        }
    }

    public void painHealthAnim()
    {
        if (playerStats.playerHealth == playerStats.playerHealthMax)
        {
            GetComponent<SpriteRenderer>().color = neutral; //
        }
        else if (playerStats.playerHealth == playerStats.playerHealthMax - 1)
        {
            GetComponent<SpriteRenderer>().color = yellow;
        }
        else if (playerStats.playerHealth <= playerStats.playerHealthMax - 3)
        {
            GetComponent<SpriteRenderer>().color = red; //
        }
        else
        {
            GetComponent<SpriteRenderer>().color = orange;
        }

        if (paintimer == paintimermax && playerStats.playerSouls < 1)
            for (int i = 15; i > 0; i--)
                trailclone = Instantiate(trail, new Vector3(transform.position.x + Random.Range(trailxmin * painTrailMult, trailxmax * painTrailMult),
                transform.position.y + Random.Range(trailymin * painTrailMult, trailymax * painTrailMult), transform.position.z + sparkzdifference),
                Quaternion.identity) as GameObject;
        if (paintimer > 0)
        {
            a.Play("spirit_idle");
            painspritetimer--;
            if (painspritetimer == 0 && painspritebool)
            {
                GetComponent<SpriteRenderer>().color = red;
                srr.sprite = defaultsprite1;
                painspritebool = false;
            }
            else
            {
                srr.sprite = inPainSprite;
                painspritebool = true;
            }
            paintimer--;
        }

        if (sprBT == 0 && playerStats.playerHealth == playerStats.playerHealthMax)
            sprBT = sprBTmax1;
    }

    ////////////Slower Update/////////////
    public void horizontalFriction()
    {

        
        //Horizontal Friction
        if (rb.velocity.x - leftrightfriction > 0)
            rb.velocity = new Vector2(rb.velocity.x - leftrightfriction, rb.velocity.y);
        else if (rb.velocity.x + leftrightfriction < 0)
            rb.velocity = new Vector2(rb.velocity.x + leftrightfriction, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
            
    }

    void FixedUpdate()
    {
        
        dead = Physics2D.OverlapCircle(deadcheck.position, deadcheckradius, whatisdeathobstacle);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, groundcheckradius);
    }
    /*private void EnergyRegenFunction()
    {
        if(energyRegenPeriod <= 0)
        {
            if (playerStats.playerEnergy < playerStats.playerEnergyMax)
            {
                if(playerStats.playerEnergy < energyRegenBonusThreshold)
                {
                    playerStats.playerEnergy += energyRegen;
                }else
                {
                    playerStats.playerEnergy += energyRegen + energyRegenBonus;
                }
                energyRegenPeriod = energyRegenPeriodMax;

                if (playerStats.playerEnergy > playerStats.playerEnergyMax)
                {
                    playerStats.playerEnergy = playerStats.playerEnergyMax;
                }
            }
        }else
        {
            energyRegenPeriod -= Time.deltaTime * 100;
        }

        
    }*/


}//end of public class "PlayerController : MonoBehaviour"
