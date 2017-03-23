using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Material defaultMaterial, material;
	public GameObject start;

	WebCamTexture webCamTexture;
	WebCamDevice[] devices;
	Renderer rend;
	Rigidbody rb;

	Vector3 initialPosition;

	void Start() 
	{
		rend = GetComponent <Renderer> ();
		rb   = GetComponent <Rigidbody> ();
		initialPosition = new Vector3 (0f,0.216f, 0.281f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 move = Vector3.zero;
		move.x = Input.GetAxis ("Horizontal");
		move.z = Input.GetAxis ("Vertical");
	
		rb.AddForce (move*100);

	}
	void OnTriggerEnter(Collider other){

	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Outline") {
			reset ();
		}
	}

	void reset () {
		rb.velocity        = Vector3.zero;
		transform.position = start.transform.position;
	}

}
