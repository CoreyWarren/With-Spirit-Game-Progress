using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{

    [HideInInspector]
    public bool opaque, displaying;
    private Image myImage, avatarImage;

    // Start is called before the first frame update
    void Start()
    {
        opaque = true;
        myImage = GetComponent<Image>();
    }

    Transform textTrans;
    Transform avatarTrans;

    // Update is called once per frame
    void Update()
    {

        if(textTrans == null)
        {
            textTrans = GameObject.FindWithTag("Dialogue Text").transform;
        }
        if (avatarTrans == null)
        {
            avatarTrans = GameObject.FindWithTag("Dialogue Image").transform;
            avatarImage = avatarTrans.gameObject.GetComponent<Image>();
        }

        //Pressing P while dialogue is happening
        if (Input.GetKeyDown("p"))
        {
            if(opaque)
            {
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0.1f);
                avatarImage.color = new Color(avatarImage.color.r, avatarImage.color.g, avatarImage.color.b, 0.1f);
                opaque = false;
            }else
            {
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 1f);
                avatarImage.color = new Color(avatarImage.color.r, avatarImage.color.g, avatarImage.color.b, 1f);
                opaque = true;
            }
        }


        //Primary Dialogue Functions
        if(DialogueManager.dialogueOn)
        {
            textTrans.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            this.GetComponent<Image>().enabled = true;
            avatarImage.enabled = true;

            if(!displaying)
            {
                opaque = true;
                displaying = true;
            }
            
        }
        else
        {
            textTrans.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            this.GetComponent<Image>().enabled = false;
            avatarImage.enabled = false;
            displaying = false;
        }
    }
}
