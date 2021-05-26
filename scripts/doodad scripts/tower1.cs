using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower1 : MonoBehaviour {

    public float pulseRate;
    private int pulseTimer;
    public GameObject pulse;
    GameObject pulseclone;
    public float restTimerMax = 120;
    private float restTimer;
    private bool on = true;
    private float pulses;

    // Use this for initialization
    void Start () {
        pulseTimer = 0;
        pulses = 0;
	}

    // Update is called once per frame
    void Update() {
        if (pauser1.paused == false)
        {
            //Active (Pulsing)//
            if (on)
            {
                if (pulseTimer == pulseRate)
                {
                    pulseclone = Instantiate(pulse, new Vector3(transform.position.x,
                                transform.position.y + 0.55f, transform.position.z),
                                Quaternion.identity) as GameObject;
                    pulseTimer = 0;
                    pulses++;
                }

                if (pulses == 3)
                {
                    on = false;
                    pulses = 0;
                    pulseTimer = 0;
                }
                pulseTimer++;
            }
            //Resting (Not Pulsing)//
            else
            {
                restTimer++;

                if (restTimer == restTimerMax)
                {
                    on = true;
                    restTimer = 0;
                }
            }

        }
    }//update


}
