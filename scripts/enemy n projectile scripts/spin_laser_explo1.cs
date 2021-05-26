using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin_laser_explo1 : MonoBehaviour {

    public float lifetimermax;
    private float lifetimer;

    AudioSource as1;
    public AudioClip explosion;
	// Use this for initialization
	void Start () {

        lifetimer = lifetimermax;
        as1 = GetComponent<AudioSource>();
        as1.PlayOneShot(explosion);
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
        }
	}
}
