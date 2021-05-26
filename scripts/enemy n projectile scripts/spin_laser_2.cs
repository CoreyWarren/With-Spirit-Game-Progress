using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin_laser_2 : MonoBehaviour {

    //spinlaser2 = spinner homing missle
    Rigidbody2D rb;
    Transform player;
    public float rotatespeed;
    public float bulletspeed;
    private int lifeTimer;
    public int lifeTimerMax;
    public float minBulletSpeed;

    private bool hit = false;
    public Transform hitcheck;
    public float hitcheckradius;
    public LayerMask whatishit;

    public GameObject explosion;
    GameObject creation1;
    private AudioSource as1;
    public AudioClip shoot;
    private SpriteRenderer srr;

    public float bulletSpeedDecay;

    private float missileFlashTimer;
    [SerializeField]
    private float missileFlashRestTime;
    [SerializeField]
    private float missileFlashActiveTime;
    private bool missileFlashing;
    
    private Sprite defaultMissile;
    [SerializeField]
    private Sprite flashingMissile;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        hitcheck = GetComponent<Rigidbody2D>().transform;
        as1 = GetComponent<AudioSource>();
        lifeTimer = lifeTimerMax;
        as1.PlayOneShot(shoot);
        srr = GetComponent<SpriteRenderer>();
        defaultMissile = srr.sprite;
    }
	
	// Update is called once per frame
	void Update () {
        if (pauser1.paused == false)
        {
            Vector2 direction = (Vector2)player.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotatespeed;

            rb.velocity = transform.forward * bulletspeed;

            if (rotatespeed > 50)
            {
                rotatespeed -= 9;
            }
            if (bulletspeed > minBulletSpeed)
            {
                bulletspeed = bulletspeed - ((bulletspeed / 1000) * bulletSpeedDecay);
            }
            rb.velocity = transform.up * bulletspeed;
            hit = Physics2D.OverlapCircle(hitcheck.position, hitcheckradius, whatishit);

            lifeTimer--;

            if (hit || lifeTimer <= 0)
            {
                creation1 = Instantiate(explosion, rb.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            /*
             *  public float missileFlashTimer;
                public float missileFlashRestTime;
                public float missileFlashActiveTime;
                private bool missileFlashing;
             */
            


            if(missileFlashing)
            {

                if(missileFlashTimer > 0)
                {
                    missileFlashTimer--;
                }
                else
                {
                    missileFlashing = false;
                    srr.sprite = defaultMissile;
                    missileFlashTimer = missileFlashRestTime;
                }


            }else
            if(!missileFlashing)
            {  
                if(missileFlashTimer > 0)
                {
                    missileFlashTimer--;
                }
                else
                {
                    missileFlashing = true;
                    srr.sprite = flashingMissile;
                    missileFlashTimer = missileFlashActiveTime;
                }
                
            }






        }//endof paused
    }
}
