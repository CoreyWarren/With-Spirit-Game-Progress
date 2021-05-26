using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserhex_LR : MonoBehaviour {

	public float lasertimer;
	public float lasertimermax;
    [SerializeField]
    private float laserTimerVariation;
	public GameObject laser;
	private GameObject laserclone;
	public SpriteRenderer srr;
	private Color defaultcol;
	public float toofarValue;
	public AudioSource aso;
    public AudioClip lasersound;
	public float laserRotationDegrees;
	public Transform player;
	private Vector3 defaultScale;

	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
        lasertimer = Random.Range(0f, laserTimerVariation * 2);
		srr = GetComponent<SpriteRenderer>();
		aso = GetComponent<AudioSource>();
		defaultcol = srr.color;
		defaultScale = transform.localScale;
	}

    // Update is called once per frame
    void Update()
    {
        if (pauser1.paused == false)
        {
            if (Mathf.Abs(transform.position.y - player.position.y) < toofarValue &&
            Mathf.Abs(transform.position.x - player.position.x) < toofarValue)
            {
                if (lasertimer <= 0)
                {
                    laserclone = Instantiate(laser, transform.position, Quaternion.Euler(0, 0, laserRotationDegrees)) as GameObject;
                    aso.PlayOneShot(lasersound);
                    transform.localScale = defaultScale;
                    lasertimer = lasertimermax + Random.Range(-laserTimerVariation, laserTimerVariation);
                }
                if ((lasertimer > 3 && lasertimer < 7) || (lasertimer > 10 && lasertimer < 15))
                {
                    srr.color = new Color(1f, .3f, .5f);
                }
                else
                {
                    srr.color = defaultcol;
                }

                transform.localScale = new Vector3(defaultScale.x, (defaultScale.y/3) + (lasertimermax - lasertimer)/300f, 0f);
                float diff = lasertimermax - lasertimer;
                srr.color -= new Color(-(diff * (Random.Range(-0.003f, 0.003f))), -(diff * (Random.Range(-0.003f, 0.003f))), 0f, (diff - 3f) * 0.002f);
                lasertimer--;
            }
        }
    }

}
