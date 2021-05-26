using System.Collections;
using System.Collections.Generic;
// PLAYER MISSILE //
// PLAYER MISSILE //
// PLAYER MISSILE //
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class missile1_script : MonoBehaviour {

    public Transform target;

    private Rigidbody2D rb;
    SpriteRenderer srr;

    public float speed;
    public float rotateSpeed;

    GameObject closest;

    public bool newTarget;
    public bool longdeath;
    public float longdeathtime = 20;

    public float lifeTimerMax;
    public float lifeTimer;

    public Transform hitcheck;
    public float hitcheckradius;
    public LayerMask whatishit;
    private bool hit = false;

    private float hitTimer;
    private float hitTimerMax = 20;
    private bool hitDeath = false;

    public GameObject mexplo1;
    GameObject explosion;

    private AudioSource audio1;
    public AudioClip seeking;

    GameObject FindEnemy()
    {
        GameObject go;
        go = GameObject.FindGameObjectWithTag("Enemy");
        return go;
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
            return closest;
    }

    void Start()
    {
        audio1 = gameObject.GetComponent<AudioSource>();
        lifeTimer = lifeTimerMax + Random.Range(-20, 20); ;
        hitTimer = hitTimerMax;
        newTarget = false;
        audio1.pitch += Random.Range(-0.3f, 0.3f);
        audio1.PlayOneShot(seeking);
        srr = GetComponent<SpriteRenderer>();
        longdeath = false;
    }

    //UPDATE
    void Update()
    {
        if (pauser1.paused == false)
        {
            if (!FindEnemy())
            {
                explosion = Instantiate(mexplo1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                Destroy(gameObject);
                PlayerController.soulcount++;
            }

            hit = Physics2D.OverlapCircle(hitcheck.position, hitcheckradius, whatishit);
            rb = GetComponent<Rigidbody2D>();

            if(FindClosestEnemy() != null)
            {
                target = FindClosestEnemy().transform;
            }
            

            if (lifeTimer <= 0 && longdeath == false)
            {
                speed = 0;
                srr.color = new Color(0f, 0f, 0f, 0f);
                explosion = Instantiate(mexplo1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                longdeath = true;
                lifeTimer = longdeathtime;
            }

            if (longdeath == true && lifeTimer <= 0)
            {
                Destroy(gameObject);
            }

            lifeTimer--;

            if (speed < 7)
                speed = speed + speed * 0.05f;

            if (rotateSpeed < 600)
                rotateSpeed += 2;

            if(target != null)
            {
                Vector2 direction = (Vector2)target.position - rb.position;
                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.up).z;
                //rotate it
                rb.angularVelocity = -rotateAmount * rotateSpeed;
                rb.velocity = transform.up * speed;
            }
            
            

            


            if (hit && hitDeath == false)
            {
                speed = 0;
                hitDeath = true;
                srr.color = new Color(0f, 0f, 0f, 0f);
                explosion = Instantiate(mexplo1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            }

            if (hitDeath == true)
            {
                hitTimer--;
            }

            if (hitTimer <= 0 && hitDeath == true)
            {
                gameObject.layer = 0;
                Destroy(gameObject);
            }
        }
    }


    

}
