using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2 : MonoBehaviour {

	public float initialSpeed = 600f;

	private Rigidbody rb;
	private bool ballInPLay;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

		// if ball has not been launched...
		if (Input.GetButtonDown("Fire1") && ballInPLay  == false) {
			ballInPLay = true;
			rb.AddForce (new Vector3 (initialSpeed, 0f, initialSpeed));
		}
	}
}
