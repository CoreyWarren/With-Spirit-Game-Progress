using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas_soulscript : MonoBehaviour {

    Player_Stats_Storage playerStats;
    public Text soulText;
    Vector2 size1 = new Vector2(1f, 1f);
    Vector2 size2 = new Vector2(1.1f, 1.5f);

    private float blinkTimer;
    private float blinkTimerMax = 6;

    private float painTimer;
    private float painTimerMax = 16;

    RectTransform rtf; 

    Color[] colors = new Color[]
    {
        new Color (1f, 1f, 1f, .9f), // This is the color briefly shown when switching
        new Color (1f, 0f, 0f),
        new Color (0f, 1f, 0f),
        new Color (0f, 0f, 1f),
        new Color (0f, .5f, .5f)
    };

    Color defaultColor;
    Vector2 defaultPos;
    Vector2 defaultSize;

    public static bool uiSoulTouch;
    public static bool uiPainTouch;

    // Use this for initialization
    void Start () {
        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
        rtf = GetComponent<RectTransform>();
        defaultColor = soulText.color;
        defaultPos = rtf.anchoredPosition;
        defaultSize = transform.localScale;
        blinkTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Text Itself
        if (playerStats.playerSouls == 0)
            soulText.text = "Souls: --";
        else if (playerStats.playerSouls < 10)
            soulText.text = "Souls: 0" + playerStats.playerSouls;
        else
            soulText.text = "Souls: " + playerStats.playerSouls;


        //Text Effects and Colors
        if (uiSoulTouch)
        {
            blinkTimer = blinkTimerMax;
            transform.localScale = size2;
            soulText.color = colors[0];
            uiSoulTouch = false;
        }

        if (blinkTimer == 0)
        {
            transform.localScale = size1;
            soulText.color = defaultColor;
        }
        else if
        (blinkTimer % 3 == 0 || (blinkTimer + 1) % 3 == 0)
        {
            transform.localScale = size1;
            soulText.color = defaultColor;
        } else
        {
            transform.localScale = size2;
            soulText.color = colors[0];
        }

        
        if (uiPainTouch)
        {
            painTimer = painTimerMax;
            uiPainTouch = false;
        }

        if (painTimer == 0 && blinkTimer == 0)
        {
            transform.localScale = size1;
            soulText.color = defaultColor;
            rtf.anchoredPosition = defaultPos;
        }
        else if
        ((painTimer % 2 == 0 || painTimer % 3 == 0) && blinkTimer == 0)
        {
            transform.localScale = size1;
            soulText.color = defaultColor;
        }
        else if
        (blinkTimer == 0 && painTimer != 0)
        {
            float randomScale = Random.Range(0f, .55f);
            transform.localScale = defaultSize + new Vector2(randomScale, randomScale);
            rtf.anchoredPosition = defaultPos + new Vector2(Random.Range(-3f, 3f), Random.Range(-1f, 1f));
            soulText.color = colors[1];
        }


        
        if (blinkTimer > 0)
        {
            blinkTimer--;
        }

        if (painTimer > 0)
        {
            painTimer--;
        }

        if (playerStats.playerHealth < 0)
        {
            float randomScale = Random.Range(.2f, .6f);
            transform.localScale = defaultSize + new Vector2(randomScale, randomScale);
            rtf.anchoredPosition = defaultPos + new Vector2(Random.Range(-10f, 10f), Random.Range(-3f, 3f));
            soulText.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        if (playerStats.playerHealth == 0 && painTimer == 0 && blinkTimer == 0)
        {
            float randomScale = Random.Range(.4f, .44f);
            transform.localScale = defaultSize + new Vector2(randomScale, randomScale);
            rtf.anchoredPosition = defaultPos + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            soulText.color = new Color(1f, .2f, .1f, 1f);
        }
        else
        if (playerStats.playerHealth == 1 && painTimer == 0 && blinkTimer == 0)
        {
            float randomScale = Random.Range(.3f, .32f);
            transform.localScale = defaultSize + new Vector2(randomScale, randomScale);
            rtf.anchoredPosition = defaultPos + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            soulText.color = new Color(1f, .3f, .4f, 1f);
        }
        else
        if (playerStats.playerHealth == 2 && painTimer == 0 && blinkTimer == 0)
        {
            float randomScale = Random.Range(.2f, .22f);
            transform.localScale = defaultSize + new Vector2(randomScale, randomScale);
            rtf.anchoredPosition = defaultPos + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            soulText.color = new Color(.8f, .4f, .4f, 1f);
        }
        else if (playerStats.playerSouls < 5 && painTimer == 0 && blinkTimer == 0)
        {
            soulText.color = new Color (0f,.8f,.8f,1f);
        }


    }
}
