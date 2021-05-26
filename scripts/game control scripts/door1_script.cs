using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door1_script : MonoBehaviour {

    GameObject player;

    public Transform playerCheck;
    public float playerCheckRadius;
    public LayerMask whatIsPlayer;
    public bool playerHere;

    public string targetSceneName;

    // Use this for initialization
    void Start () {
		player = GameObject.FindWithTag("Player");
        playerHere = false;
    }
	
	// Update is called once per frame
	void Update () {

        playerHere = Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

        if (Input.GetKeyDown(KeyCode.DownArrow) && playerHere)
            {


            if (Application.CanStreamedLevelBeLoaded(targetSceneName))
            {
                //If the scene is loadable/exists, then load it.
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.Log("That scene does not exist. Wrong Name? No name?");
            }


        }
	}
    
}
