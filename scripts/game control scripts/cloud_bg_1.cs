using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud_bg_1 : MonoBehaviour {

    GameObject cam;
    Vector2 posCam;

    public float xSpeed;
    public float scrolled;
    public float xLimit;
    public float yOffset;
    public float yAmp, yPeriod;

    private float t;

    public float yCamDiff;
    public float xCamDiff;
	// Use this for initialization
	void Awake () {
    
        cam = GameObject.FindWithTag("MainCamera");
        posCam = new Vector2(0f, 0f);

        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + yOffset, transform.position.z);
        
        scrolled = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (cam == null)
        {
            cam = GameObject.FindWithTag("MainCamera");
        }
        if(!pauser1.paused)
        {

            t += yPeriod;

            if (t >= 360)
            {
                t = 0;
            }

            //(This is seemingly done twice on purpose)
            transform.position = new Vector3(transform.position.x, yOffset + Mathf.Sin(Mathf.PI * t * yAmp), transform.position.z);


            posCam = new Vector2(cam.transform.position.x, cam.transform.position.y);
            Vector2 posMy = new Vector2(transform.position.x, transform.position.y);

            

            scrolled -= xSpeed;

            if (scrolled > Mathf.Abs(xLimit))
            {
                scrolled = 0f;
            }

            //Scroll
            posMy = new Vector2(posCam.x - scrolled, posMy.y);

            //Offset and Float (sin function)
            transform.position = new Vector3(posMy.x, posCam.y + yOffset + Mathf.Sin(Mathf.PI * t * yAmp), transform.position.z);

            xCamDiff = cam.transform.position.x - transform.position.x;
            yCamDiff = cam.transform.position.y - transform.position.y - yOffset + Mathf.Sin(Mathf.PI * t * yAmp);
        }
        
        
        




	}//end update



}
