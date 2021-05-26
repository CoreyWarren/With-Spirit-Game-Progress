using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser1_script : MonoBehaviour {

	public float laserspeed;
	private float laserspeedinit;
	private Rigidbody2D rb;
    private AudioSource as1;
    private SpriteRenderer srr;
    private Collider2D col2d;

    [SerializeField]
    private AudioClip grounding;
    private float timePlaying;
    
    private bool dying;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0, -laserspeed);
		laserspeedinit = laserspeed;
        as1 = GetComponent<AudioSource>();
        srr = GetComponent<SpriteRenderer>();
        col2d = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update(){
		if (laserspeed < laserspeedinit*3)
		{
			laserspeed += .3f;
			rb.velocity = new Vector2(0, -laserspeed);
		}


        if(dying)
        {
            if(gameObject.layer != 0)
            {
                as1.PlayOneShot(grounding);
                rb.simulated = false;
                col2d.enabled = false;
                gameObject.layer = 0;
                srr.enabled = false;
            }

            timePlaying += Time.deltaTime;
            
            if (timePlaying >= grounding.length)
            {
                Destroy(gameObject);
                Debug.Log("destroying");
            }
        }
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 8)
		{
            dying = true;
		}
	}
}
