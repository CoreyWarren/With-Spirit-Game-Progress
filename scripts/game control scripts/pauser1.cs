using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauser1 : MonoBehaviour {

    public static bool paused;
    public static bool dialogue;
    public static bool endDialogue;
    private float timescaledefault;
    public GameObject pausemenu1;
    Player_Stats_Storage playerStats;
    GameObject pausemenu1_clone;
    GameObject canvas;
    RectTransform rt;

    private bool pauseSoundEnabled;

    [SerializeField]
    private AudioClip pauseSound;

    AudioSource as1;

    // Use this for initialization
    void Start () {
        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
        paused = false;
        timescaledefault = Time.timeScale;
        canvas = GameObject.FindWithTag("Main Canvas");
        if (canvas != null)
        {
            rt = canvas.GetComponent<RectTransform>();
        }
        as1 = GetComponent<AudioSource>();
        pauseSoundEnabled = true;
        endDialogue = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(!dialogue)
        {




            if (Input.GetKeyDown("o"))
            {

                as1.PlayOneShot(pauseSound);
                pauseSoundEnabled = !pauseSoundEnabled;
                if (!pauseSoundEnabled)
                {
                    as1.pitch = 0.9f;
                }
                else
                {
                    as1.pitch = 1.1f;
                }
            }

            if (Input.GetKeyDown("p") && !paused)
            {
                paused = true;
                CanvasNullCheck();
                Time.timeScale = 0;
                if (pauseSoundEnabled)
                {
                    as1.pitch = 1f;
                    as1.PlayOneShot(pauseSound);
                }


                pausemenu1_clone = Instantiate(pausemenu1, rt.anchoredPosition + new Vector2(0f, 50f),
                    Quaternion.identity, canvas.transform) as GameObject;
            }
            else
            if (Input.GetKeyDown("p") && paused)
            {
                CanvasNullCheck();
                Time.timeScale = timescaledefault;
                paused = false;
                Destroy(pausemenu1_clone);
            }

            if (!paused)
            {
                if (pausemenu1_clone != null)
                {
                    Destroy(pausemenu1_clone);
                }
            }

            if (Input.GetKeyDown("r") && paused)
            {
                CanvasNullCheck();
                Time.timeScale = timescaledefault;
                paused = false;
                Destroy(pausemenu1_clone);
                playerStats.playerDied = true;
                dialogue = false;

            }







        }else
        //if(dialogue == true) <-- that's actually what this below is \/ \/
        {
            if(paused != true)
            {
                paused = true;
                Time.timeScale = 0;
                //BUT now,
                //do not make pause menu.
            }


            if(endDialogue)
            {
                paused = false;
                Time.timeScale = timescaledefault;
                dialogue = false;
                endDialogue = false;
            }





        }
        
        

    }

    void CanvasNullCheck()
    {
        if(canvas == null)
        {
            canvas = GameObject.FindWithTag("Main Canvas");
            rt = canvas.GetComponent<RectTransform>();
            Debug.Log("Found new canvas for pause menu");
        }
    }
}
