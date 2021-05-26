using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_soul_script : MonoBehaviour {

    //bool deleting = false;

    private Color defaultcolor;
    Player_Stats_Storage playerStats;

    public Transform player;
    public float bulletspeed;
    public float bulletspeedmin;
    public float bulletspeedmax;
	public float rotatespeed;
    public float rotatespeedconstant;
    public float rotatespeedmax;
    public float speeddistantconstant;
    public float startingspeedconstant;
    public float mindistancefollow;
    public float maxdistance;

    private float blinktimer = 0;
    private float blinktimermax = 12;
    private float blinktimermin = 5;
    public bool blinking;
    Color neutral = new Color(1F, 0.8F, 0.5F);
    Color orange = new Color(1F, 0.5F, .8F);
    public float t;
    public float rotationtime;
    public Rigidbody2D rb;
    public SpriteRenderer srr;
    public float soulnumber;
    private float ampVariation;
    private float amp;
    GameObject trainclone;
    private float trailxmin = -0.2f;
    private float trailxmax = 0.2f;
    private float trailymin = -0.2f;
    private float trailymax = 0.2f;
    private int directionRotate;
    private bool innerZone, outerZone;
    public GameObject trail;
    GameObject trailclone;

    private float sizeVariation;
    private int timeVariationx;
    private int timeVariationy;
    private float orbitSize;
    
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        float gos;
        gos = GameObject.FindGameObjectsWithTag("Travelling Soul").Length;
        if(gos > 20)
        {
            Destroy(gameObject);
        }

        soulnumber = gos;
        
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 2);

        ampVariation = (Random.Range(0.2f, 0.5f) + Random.Range(0.2f, 0.5f) + Random.Range(0.2f, 0.5f)) / 3;
        amp = 1.3f;
        sizeVariation = Random.Range(0.05f, 0.3f);
        orbitSize = Random.Range(0.1f, 5f);

        timeVariationx = (Random.Range(1, 3) + Random.Range(1, 3)) / 2 ;
        timeVariationy = (Random.Range(1, 3) + Random.Range(1, 3) ) / 2;
        srr = GetComponent<SpriteRenderer>();
        srr.color -= new Color(0f, 0f, 0f, Random.Range(0.5f, 0.8f));
        defaultcolor = srr.color;
        startingspeedconstant = speeddistantconstant;
        speeddistantconstant += (Random.Range(-speeddistantconstant, speeddistantconstant) + Random.Range(-speeddistantconstant, speeddistantconstant)) / 2;
        rotatespeedconstant += Random.Range(-2f, 2f);
        directionRotate = 1;
        innerZone = false; outerZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (t <= (2 * Mathf.PI))
            { t += (Mathf.PI / (rotationtime)); }
            else
            { t = 0; }

            transform.localScale = new Vector3(.6f + (sizeVariation * Mathf.Sin(t)), .6f + (sizeVariation * Mathf.Sin(t)), 1f);



            var distance1 = Vector3.Distance(player.transform.position, this.transform.position);
            float rotateAmount = 0;

            if (distance1 > mindistancefollow)
            {
                if(!outerZone)
                { outerZone = true; }
                if(innerZone)
                {
                    directionRotate = -directionRotate;
                    innerZone = false;
                }
                Vector2 direction = (Vector2)player.position - rb.position;
                rotateAmount = Vector3.Cross(direction, transform.up).z;
                direction.Normalize();


                if (rotateAmount == 0)
                {
                    rotateAmount += Random.Range(-45f, 45f);
                }

                bulletspeed = (distance1 * distance1) * speeddistantconstant;
                rotatespeed = (distance1 * distance1) * rotatespeedconstant;
            }
            else
            {
                if (!innerZone)
                { innerZone = true; }
                rotateAmount += directionRotate * Random.Range(0, 0.4f);
                bulletspeed = bulletspeedmin;
            }

            rb.angularVelocity = -rotateAmount * rotatespeed;


            rb.velocity = transform.up * bulletspeed;
            rb.velocity += new Vector2(orbitSize * Mathf.Cos(t), orbitSize * Mathf.Sin(t));



            if (bulletspeed > bulletspeedmax)
            {
                bulletspeed = bulletspeedmax;
            }

            if (rotatespeed > rotatespeedmax)
            {
                rotatespeed = rotatespeedmax;
            }

            if (distance1 > maxdistance)
            {
                transform.position = player.transform.position;
                speeddistantconstant = startingspeedconstant;
                speeddistantconstant += (Random.Range(-speeddistantconstant, speeddistantconstant) + Random.Range(-speeddistantconstant, speeddistantconstant)) / 2;
            }


            if (blinktimer < 1)
            {
                if (blinking)
                {
                    blinking = false;
                }
                else
                {
                    blinking = true;
                }
                blinktimer = Random.Range(blinktimermin, blinktimermax);
            }

            blinktimer--;

            if (!blinking)
                GetComponent<SpriteRenderer>().color = defaultcolor;
            else
                GetComponent<SpriteRenderer>().color = orange;

            //Deletion

            //soul collector
            if (PlayerController.socotouch)
            {
                if (playerStats.playerSouls == soulnumber)
                {
                    playerStats.playerSouls--;
                    Destroy(gameObject);
                }
            }

            if (soulnumber > playerStats.playerSouls)
            {
                for (int i = 5; i > 0; i--)
                    trailclone = Instantiate(trail, new Vector3(transform.position.x + Random.Range(trailxmin, trailxmax),
                    transform.position.y + Random.Range(trailymin, trailymax), transform.position.z),
                    Quaternion.identity) as GameObject;
                Destroy(gameObject);
            }

        }else
        {
            player = GameObject.FindWithTag("Player").transform;
            if(player == null)
            {
                Destroy(gameObject);
            }

        }
        




    }//Update

}
