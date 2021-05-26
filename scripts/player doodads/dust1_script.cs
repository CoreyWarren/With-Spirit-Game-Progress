using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dust1_script : MonoBehaviour {

	public float lifeTime;
	private float lifeTimer;
	SpriteRenderer srr;
	// Use this for initialization
	void Start () {
		lifeTimer = lifeTime;	
		srr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		srr.color -= new Color(0f, 0f, 0f, Random.Range(0.3f, 0.7f));
		lifeTimer--;
		if (lifeTimer <= 0)
		{
			Destroy(gameObject);
		}
	}
}
