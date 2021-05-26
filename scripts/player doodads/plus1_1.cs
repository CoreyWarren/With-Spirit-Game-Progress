using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plus1_1 : MonoBehaviour {

    public float lifetime;
    private float lifetimer;
    public float startSpeed;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        lifetimer = lifetime;
        rb = GetComponent<Rigidbody2D>();
        startSpeed += Random.Range(-1f, 1f);
        transform.localScale = new Vector2(transform.localScale.x + Random.Range(-0.1f, 0.1f), transform.localScale.y + Random.Range(-0.1f, 0.1f));
        float var = Random.Range(-1f, 1f);
        if (var > 0)
        {
            rb.velocity = new Vector2(startSpeed * 1/8f, startSpeed);
        }
        if (var < 0)
        {
            rb.velocity = new Vector2(startSpeed * -1/8f, startSpeed);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (pauser1.paused == false)
        {
            lifetimer--;
            if (lifetimer <= 0)
            {
                Destroy(gameObject);
            }

            transform.localScale -= new Vector3(0.01f, 0.01f, 0f);
            rb.velocity -= new Vector2(0f, rb.velocity.y / 10f);
        }
	}
}
