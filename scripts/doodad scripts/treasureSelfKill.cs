using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasureSelfKill : MonoBehaviour
{

    private bool playerHere;
    private bool dying;
    [SerializeField]
    private float playerCheckRadius, myDeathTime;
    private float myDeathTimer;
    [SerializeField]
    private LayerMask whatIsPlayer;

    private AudioSource as1;
    private SpriteRenderer srr;
    private BoxCollider2D myHitbox;
    [SerializeField]
    private AudioClip foundSound;

    // Start is called before the first frame update
    void Start()
    {
        playerHere = false;
        dying = false;
        as1 = GetComponent<AudioSource>();
        srr = GetComponent<SpriteRenderer>();
        myHitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHere = Physics2D.OverlapCircle(transform.position, playerCheckRadius, whatIsPlayer);

        if(playerHere)
        {
            if(!dying)
            {
                myDeathTimer = myDeathTime;
                dying = true;
                as1.PlayOneShot(foundSound);
                srr.enabled = false;
                Destroy(myHitbox);
            }
            
        }

        if(dying)
        {
            myDeathTimer -= Time.deltaTime;
            if(myDeathTimer <= 0)
            {
                if (transform.parent != null)
                {
                    GameObject foo;
                    foo = transform.parent.gameObject;
                    transform.parent = null;
                    Destroy(foo);
                }
                Destroy(gameObject);
            }
        }
    }
}
