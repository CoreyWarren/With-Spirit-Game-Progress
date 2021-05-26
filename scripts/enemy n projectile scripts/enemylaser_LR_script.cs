using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemylaser_LR_script : MonoBehaviour {

	public float laserspeed;
	private float laserspeedinit;
    private int lifeTimer;
    public int lifeTimerMax;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(laserspeed, 0);
		laserspeedinit = laserspeed;
        lifeTimer = lifeTimerMax;
	}
	
	// Update is called once per frame
	void Update(){
        if (pauser1.paused == false)
        {

            if (laserspeed < laserspeedinit * 3)
            {
                laserspeed += .3f;
                rb.velocity = new Vector2(laserspeed, 0);
            }

            lifeTimer--;
            if (lifeTimer <= 0)
            {
                Object.Destroy(this.gameObject);
            }

        }
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 8)
		{
			Object.Destroy(this.gameObject);
		}
	}
}
