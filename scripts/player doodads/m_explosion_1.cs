using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_explosion_1 : MonoBehaviour {
    
    public float deleteTimerMin = -24;
    private float killTimer;
    public float killTimerMax = 12;
    private float soundtimer;
    public AudioSource aso;
    public AudioClip explo1;
    public Collider2D coll;

	// Use this for initialization
	void Start () {
        killTimer = killTimerMax;
        aso = GetComponent<AudioSource>();
        aso.pitch += (Random.Range(-0.3f, 0.3f) + Random.Range(-0.3f, 0.3f))/2;
        aso.PlayOneShot(explo1);
        coll = gameObject.GetComponent<Collider2D>();
        soundtimer = Random.Range(0, 8);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
    }
	
	// Update is called once per frame
	void Update () {
        if (pauser1.paused == false)
        {
            killTimer--;

            if (killTimer == 0)
            {
                Destroy(coll);
            }

            if (killTimer <= deleteTimerMin)
            {
                Destroy(gameObject);
            }

        }
	}
}
