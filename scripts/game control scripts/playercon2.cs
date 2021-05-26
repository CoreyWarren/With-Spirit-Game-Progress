using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercon2 : MonoBehaviour {

    public static bool strafing;
    AudioSource as1;
    public AudioClip strafeToggleSound;

	// Use this for initialization
	void Start () {
        strafing = false;
        as1 = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("q"))
        {
            if(strafing)
            {
                strafing = false;
            } else
            {
                strafing = true;
            }
            as1.PlayOneShot(strafeToggleSound);
        }
        
	}
}
