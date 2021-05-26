using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulScript1 : MonoBehaviour {

    bool deleting = false;

    private float blinktimer = 0;
    private float blinktimermax = 12;
    private float blinktimermin = 5;
    public bool blinking;
    public SpriteRenderer srr;
    Color neutral = new Color(1F, 0.8F, 0.5F);
    Color orange = new Color(1F, 0.5F, .8F);
    public Rigidbody2D rb;

    private float t;
    private float floatperiod = 80;
    private float amp=0.06f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        t = Random.Range(0, 2 * Mathf.PI);
        transform.position = new Vector3(transform.position.x, transform.position.y - amp * Mathf.Sin(t), transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {

        if(t > (Mathf.PI*2) || t < 0)
        {
            t = 0;
        }else
        {
            t += (Mathf.PI * 2) / floatperiod;
        }
        transform.position = new Vector3(transform.position.x + amp * Mathf.Cos(2 * t), transform.position.y + amp*Mathf.Sin(t), transform.position.z);

        if (blinktimer < 1)
        {
            if (blinking)
            {
                blinking = false;
            }
            else
            {
                blinking = true;
            }
            blinktimer = Random.Range(blinktimermin, blinktimermax);
        }
        
        blinktimer--;

        if (!blinking)
            GetComponent<SpriteRenderer>().color = neutral;
        else
            GetComponent<SpriteRenderer>().color = orange;

        

        //Deletion
        if (deleting)
        {
            PlayerController.soultouch = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            deleting = true;
        }
    }
}
