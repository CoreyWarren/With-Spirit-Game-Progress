using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin_laser_1 : MonoBehaviour {

    Rigidbody2D rb;
    public float yspeed;
    public float lifetimermax;
    private float lifetimer;
    public GameObject laseroffspring;
    GameObject creation;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        lifetimer = lifetimermax;
	}

    // Update is called once per frame
    void Update()
    {
        if (pauser1.paused == false)
        {
            rb.velocity = new Vector2(0f, yspeed);

            lifetimer--;
            if (lifetimer == 0)
            {
                creation = Instantiate(laseroffspring, rb.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }
}
