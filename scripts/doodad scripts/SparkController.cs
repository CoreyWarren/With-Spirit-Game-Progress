using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour {
    
    public float min_scale;
    public float max_scale;
    public float min_rotate;
    public float max_rotate;
    public float destroyTime;
    private float minusAlpha;
    private float t;
    public float scaleAmount;
    // Use this for initialization
    void Start () {
        float min_scale_temp = Random.Range(min_scale, max_scale);
        float max_scale_temp = min_scale_temp;
        transform.localScale = new Vector3(transform.localScale.x+Random.Range(min_scale_temp, max_scale_temp),
            transform.localScale.y + Random.Range(min_scale_temp, max_scale_temp), transform.localScale.z);
        minusAlpha = 1/(destroyTime*100);
        RotateLeft();
    }

    void RotateLeft()
    {
        Quaternion theRotation = transform.localRotation;
        theRotation.z = Random.Range(min_rotate, max_rotate);
        transform.localRotation = theRotation;
    }

    // Update is called once per frame
    void Update() {
        if (pauser1.paused == false)
        {
            if (t >= destroyTime)
            {
                Destroy(gameObject);
            }
            t++;

            transform.localScale += new Vector3(scaleAmount * scaleAmount, scaleAmount * scaleAmount, 0f);
        }

    }
}
