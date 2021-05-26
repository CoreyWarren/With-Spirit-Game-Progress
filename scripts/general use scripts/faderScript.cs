using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faderScript : MonoBehaviour
{

    public float fadeTime;
    
    private float fadeClock;

    public bool goLimp;

    public float opacityPercent;
    public bool usingOpacityPercent = false;

    public SpriteRenderer srr;

    [SerializeField]
    private float myOpacity;
    // Start is called before the first frame update

    void Awake()
    {
        srr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
        fadeClock = fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauser1.paused)
        {
            fadeClock -= Time.deltaTime * 10;
            float lifePercentage = fadeClock / fadeTime;

            if (!usingOpacityPercent)
            {
                //Question: Why use that boolean "usingOpacityPercent?"
                //Answer: because it's ambiguous whether setting it to 100
                //initially will overwrite the value assigned to the fader's creator;
                

                if (!goLimp)
                {
                    srr.color = new Color(srr.color.r, srr.color.g, srr.color.b, lifePercentage);
                }
                else
                {
                    srr.color = new Color(srr.color.r, srr.color.g, srr.color.b, lifePercentage / 2);
                }

            }else
            {
                srr.color = new Color(srr.color.r, srr.color.g, srr.color.b, (lifePercentage * (opacityPercent / 100)));
                srr.color = new Color(srr.color.r, srr.color.g, srr.color.b, srr.color.a * srr.color.a);
            }



            //Destroy
            if (fadeClock <= 0)
            {
                Destroy(gameObject);
            }

        }
        
    }
}
