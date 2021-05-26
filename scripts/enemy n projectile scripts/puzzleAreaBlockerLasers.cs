using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleAreaBlockerLasers : MonoBehaviour
{
    //This object's purpose
    //is to essentially ward players off from attempting to enter an area.

    //It will do so by unleashing a constant, un-passable flurry of lasers.
    //It will shoot these lasers along the entire width of its horizontal width.
    //It will shoot lasers some SET DISTANCE apart <-float
    //starting from either the left or right SIDE <-boolean
    //starting from some X offset
    //starting from some Y offset
    //at a fixed RATE <-float
    //at a fixed DISTANCE between each shot <-float
    //ending at some (-)X offset
    //ending at some (-)Y offset

    //until it reaches the end <-comparison/if statement

    [SerializeField]
    private float laserRate, laserSeparation, shootingOffset, mySpriteWidth;
    [SerializeField]
    private float shootingPosX;
    [SerializeField]
    private int frameCounter;
    [SerializeField]
    private bool startLasersLeft;
    private int startLasersSign;
    [SerializeField]
    private GameObject myLaser;
    private AudioSource myAudioSource;
    [SerializeField]
    private AudioClip laserSound;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();

        frameCounter = 0;

        if(startLasersLeft)
        {
            startLasersSign = -1;
        }else
        {
            startLasersSign = 1;
        }

        shootingPosX = (startLasersSign * mySpriteWidth) + shootingOffset;
    }

    // Update is called once per frame
    void Update()
    {

        if (pauser1.paused == false)
        {

            frameCounter++;

            if (frameCounter % laserRate == 0)
            {
                Instantiate(myLaser, new Vector3(transform.position.x + shootingPosX, transform.position.y, transform.position.z), Quaternion.identity);
                shootingPosX -= startLasersSign * laserSeparation;
                myAudioSource.PlayOneShot(laserSound);
            }

            if (Mathf.Abs(shootingPosX) > (Mathf.Abs(mySpriteWidth) + Mathf.Abs(shootingOffset)))
            {
                shootingPosX = ((startLasersSign) * mySpriteWidth) + shootingOffset;
                frameCounter = 0;
            }



        }
    }
}
