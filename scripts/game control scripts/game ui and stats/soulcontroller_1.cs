using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulcontroller_1 : MonoBehaviour {
    private float travellingSoulCount;
    public GameObject tsoul;
    GameObject makeTSoul;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        travellingSoulCount = GameObject.FindGameObjectsWithTag("Travelling Soul").Length;

        if (travellingSoulCount < PlayerController.soulcount && travellingSoulCount < 20)
        {
            makeTSoul = Instantiate(tsoul, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }
}
