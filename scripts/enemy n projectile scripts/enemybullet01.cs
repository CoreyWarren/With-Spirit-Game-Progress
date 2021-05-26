using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet01 : MonoBehaviour {

	public Transform player;
	public float bulletspeed;
	public float rotatespeed;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		rotatespeed += Random.Range(-15f, 15f);
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 direction = (Vector2)player.position - rb.position;
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateAmount * rotatespeed;
        rb.velocity = transform.up * bulletspeed;

		if (bulletspeed < 9f)
		{
			bulletspeed += .25f;
		}
	}
}
