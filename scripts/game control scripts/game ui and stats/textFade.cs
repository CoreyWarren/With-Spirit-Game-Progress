using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textFade : MonoBehaviour
{

    public float minOpacity;
    public float fadePeriod;
    private float fadeTimer;

    private bool fading;

    private TextMeshProUGUI tgui;
    private float opacity;

    private float fadePercentage;

    // Start is called before the first frame update
    void Start()
    {
        fading = false;
        tgui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (DialogueManager.dialogueOn)
        {
            fadePercentage = fadeTimer / fadePeriod;


            if(fading)
            {
                opacity = (1 - minOpacity) * fadePercentage + minOpacity;
            }
            else
            {
                opacity = (1 - minOpacity) * Mathf.Abs(fadePercentage - 1) + minOpacity;
            }
            
            fadeTimer -= 0.01f;

            if(fadeTimer <= 0)
            {
                fadeTimer = fadePeriod;
                fading = !fading;
            }


            tgui.color = new Color(tgui.color.r, tgui.color.g, tgui.color.b, opacity);
        }
    }
}
