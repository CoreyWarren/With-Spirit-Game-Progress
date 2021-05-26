using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower1_pulse : MonoBehaviour {

    public float width = 0;
    public float height = 0;
    public float scaleFrames;
    private int frames = 0;
    private float scaleRate;
    Vector3 scale;
    public SpriteRenderer spriteR;
    private float alpha;
    public float alphaRate;

    // Use this for initialization
    void Start () {
        scaleRate = 3 / scaleFrames;
        scale = new Vector2(scaleRate, scaleRate);
        transform.localScale = new Vector3(width, height, 1f);
        alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauser1.paused == false)
        {
            transform.localScale += scale;
            frames++;
            alpha -= 1 / alphaRate;
            spriteR.color = new Color(1f, 1f, 1f, alpha);
            if (frames == scaleFrames)
            {
                Destroy(gameObject);
            }
        }
    }
}
