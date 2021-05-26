using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas_weaponUI : MonoBehaviour {

    public Text uiText;
    public static int selectedW;
    private int selectedWmax = 4;

    Color defaultColor;
    Color color2 = new Color(1f, 0f, 0f);
    Color color3 = new Color(0f, 1f, 0f);
    Color color4 = new Color(0f, 0f, 1f);
    Color color5 = new Color(0f, .5f, .5f);
    public AudioSource as1;
    public AudioClip selectsound1;
    Vector2 size1 = new Vector2(1f, 1f);
    Vector2 size2 = new Vector2(1.3f, 1.8f);
    Color color1 = new Color(1f, 1f, 1f, 1f);

    Color[] colors = new Color[]
    {
        new Color (1f, 1f, 1f, .9f), // This is the color briefly shown when switching
        new Color (1f, 0f, 0f),
        new Color (0f, 1f, 0f),
        new Color (0f, 0f, 1f),
        new Color (0f, .5f, .5f)
    };

    string[] descs = new string[] { "NULL", "Sword", "Swarmers", "Bomb", "Shield" };
    //string[] hotkeys = new string[] { "NULL", "J", "K", "I", "L" };
    int selecting = 0;


    // Use this for initialization
    void Start () {
        GameObject playerStats = GameObject.FindWithTag("Event System");
        selectedW = playerStats.GetComponent<Player_Stats_Storage>().playerChosenWeapon; // Default weapon is Weapon 1
        defaultColor = uiText.color;
        as1 = GetComponent<AudioSource>();

	}

    // Update is called once per frame
    void Update() {
        transform.localScale = size1;
        uiText.text = "Weapon: " + selectedW + " " + "(" + descs[selectedW] + ")";
        uiText.color = colors[selectedW];


        if(selectedW == 1)
        {
            uiText.color = defaultColor;
        }
        
        if (selecting != 0)
        {


            if (selecting == 1)
                {
                    selectedW = 1;
                    uiText.color = colors[0];
                    as1.PlayOneShot(selectsound1);
                    transform.localScale = size2;
                    selecting = 0;
                } else
            if (selecting == 2)
                {
                    selectedW = 2;
                    uiText.color = colors[0];
                    as1.PlayOneShot(selectsound1);
                    transform.localScale = size2;
                    selecting = 0;
                }
                else
            if (selecting == 3)
                {
                    selectedW = 3;
                    uiText.color = colors[0];
                    as1.PlayOneShot(selectsound1);
                    transform.localScale = size2;
                    selecting = 0;
                }
                else
            if (selecting == 4)
                {
                    selectedW = 4;
                    uiText.color = colors[0];
                    as1.PlayOneShot(selectsound1);
                    transform.localScale = size2;
                    selecting = 0;
                }


            }
        
        
        if(Input.GetKeyDown("a"))
        {
            //Previous Weapon
            if(selectedW == 1)
            {
                selecting = 4;
            }
            else
            {
                selecting = selectedW - 1;
            }
        }

        if(Input.GetKeyDown("s"))
        {
            //Next Weapon
            if (selectedW == selectedWmax)
            {
                selecting = 1;
            }
            else
            {
                selecting = selectedW + 1;
            }

        }

        if(Input.GetKeyDown("1"))
        {
                selecting = 1;
        }
        if (Input.GetKeyDown("2"))
        {
            selecting = 2;
        }
        if (Input.GetKeyDown("3"))
        {
            selecting = 3;
        }
        if (Input.GetKeyDown("4"))
        {
            selecting = 4;
        }

    }
}
