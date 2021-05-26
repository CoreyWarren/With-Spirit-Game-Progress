using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class souldrop : MonoBehaviour {

    bool deleting = false;

    private float blinktimer = 0;
    private float blinktimermax = 12;
    private float blinktimermin = 5;
    public bool blinking;
    public SpriteRenderer srr;
    Color neutral = new Color(1F, 0.8F, 0.5F);
    Color orange = new Color(1F, 0.5F, .8F);
    public Rigidbody2D rb;
    public float lifetime;
    private float t;
    Color defaultColor;
    public float soulDefaultScale;
    public float maxInitialYSpeed;
    public float minInitialYSpeed;

    [SerializeField]
    private float minInitialXSpeed, maxInitialXSpeed, playerAttraction, playerAttractionRange, playerCheckPeriodMax;
    private float playerCheckTimer;
    private Vector2 myAddForce;
    private bool attracting;

    [SerializeField]
    private float maxXSpeed, maxYSpeed;

    private Transform playerTransform;

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector2(soulDefaultScale, soulDefaultScale);
        rb = GetComponent<Rigidbody2D>();
        srr = GetComponent<SpriteRenderer>();
        float randomamp = Random.Range(-.2f, .2f);
        transform.localScale = new Vector2(transform.localScale.x + randomamp, transform.localScale.y + randomamp);
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f));
        Physics2D.IgnoreLayerCollision(11, 9);
        float value = 1;
        float posOrNeg = Random.value < 0.5f ? -1f : 1f;
        posOrNeg = posOrNeg * value;
        rb.velocity = new Vector2(posOrNeg * (Random.Range(minInitialXSpeed, maxInitialXSpeed)), posOrNeg * Random.Range(minInitialYSpeed, maxInitialYSpeed));
        t = 0;
        defaultColor = srr.color;
        Physics.IgnoreLayerCollision(11, 14);

        if(GameObject.FindWithTag("Player") != null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        attracting = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Bounce
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


        if (t >= (lifetime / 3 * 2))
        {
            srr.color = defaultColor;
            srr.color -= new Color(0f, 0f, 0f, (t / lifetime)-.25f);
        }
        else if (!blinking)
            GetComponent<SpriteRenderer>().color = neutral;
        else
            GetComponent<SpriteRenderer>().color = orange;

        blinktimer--;

        if (pauser1.paused == false)
        {
            t++;

            //if (rb.velocity.y == 0)
            //{
            //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 5f);
            //}
            
            if (t == lifetime)
            {
                Destroy(gameObject);
            }

        }

        //Player Gravitation Script Area
        

        if (playerCheckTimer > 0)
        {
            playerCheckTimer -= Time.deltaTime;
        }
        else
        {
            Vector2 difference = new Vector2(
                    playerTransform.position.x - this.transform.position.x,
                    playerTransform.position.y - this.transform.position.y
                    );
            
            playerCheckTimer = playerCheckPeriodMax;

            float distanceSquared = difference.x * difference.x + difference.y * difference.y;
            float distance = Mathf.Sqrt(distanceSquared);

            if (distance < playerAttractionRange)
            {
                if(attracting == false)
                    attracting = true;

                myAddForce = (playerAttraction * difference.normalized / distanceSquared) * rb.mass;
            }else
            {
                if (attracting == true)
                    attracting = false;
            }

            
        }

        if(attracting)
        {
            rb.AddForce(myAddForce);
        }

        //Limit Soul Rigidbody Speed
        if (Mathf.Abs(rb.velocity.y) > maxYSpeed)
        {
            float storeSign = rb.velocity.y / Mathf.Abs(rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, maxYSpeed * storeSign);
        }

        if (Mathf.Abs(rb.velocity.x) > maxXSpeed)
        {
            float storeSign = rb.velocity.x / Mathf.Abs(rb.velocity.x);
            rb.velocity = new Vector2(maxXSpeed * storeSign, rb.velocity.y);
        }

        


    }



    void OnCollisionStay(Collision collide)
    {
        //Output the name of the GameObject you collide with
        Debug.Log("I hit the GameObject : " + collide.gameObject.name);
    }





}
