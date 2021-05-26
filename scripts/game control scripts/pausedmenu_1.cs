using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausedmenu_1 : MonoBehaviour {

    Image myimage;
    RectTransform rt;
	// Use this for initialization
	void Start () {
        myimage = GetComponent<Image>();
        myimage.color -= new Color(0f, 0f, 0f, .2f);
        rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1000f, 650f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
