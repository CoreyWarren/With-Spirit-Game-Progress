using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulcollect_transform_script : MonoBehaviour {

    bool deleting = false;
    public Transform t1;

    public AudioSource audio1;
    public AudioClip groundHit;

    // Use this for initialization
    void Start () {
        t1 = transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = t1.gameObject.transform.position;
        if (deleting && !PlayerController.soultouch)
        {
            t1 = transform.parent; 
            PlayerController.soultouch = true;
            canvas_soulscript.uiSoulTouch = true;
            Destroy(t1.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            deleting = true;
        }

        if (col.gameObject.layer.Equals(8) || col.gameObject.layer.Equals(18) )
        {
            audio1.PlayOneShot(groundHit);
        }
    }
    
}
