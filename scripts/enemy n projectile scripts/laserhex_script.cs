using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserhex_script : MonoBehaviour {

	public float lasertimer;
	public float lasertimermax;
	public GameObject laser;
	private GameObject laserclone;
	public SpriteRenderer srr;
	private Color defaultcol;
	public float toofarValue;
	public AudioSource aso;
    public AudioClip lasersound;

	public Transform player;
	// Use this for initialization

	void Start () {
		player = GameObject.Find("Player").transform;
		lasertimer = 0;
		srr = GetComponent<SpriteRenderer>();
		aso = GetComponent<AudioSource>();
		defaultcol = srr.color;
		
	}

    // Update is called once per frame
    void Update()
    {
        if (pauser1.paused == false)
        {

            if (Mathf.Abs(transform.position.y - player.position.y) < toofarValue &&
            Mathf.Abs(transform.position.x - player.position.x) < toofarValue)
            {
                if (lasertimer == 0)
                {
                    laserclone = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
                    aso.PlayOneShot(lasersound);

                    lasertimer = lasertimermax;
                }
                if ((lasertimer > 3 && lasertimer < 7) || (lasertimer > 10 && lasertimer < 15))
                {
                    srr.color = new Color(1f, .3f, .5f, 1f);
                }
                else
                {
                    srr.color = defaultcol;
                }

                lasertimer--;
            }
        }
    }
}
