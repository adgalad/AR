using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public GameObject start;
	public int gravity;

	public float startSpeed;
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

		if (rb.velocity.magnitude > 10 && rb.velocity.magnitude < 40) {
			rb.AddForce (rb.velocity.normalized * 40);
		} else if (rb.velocity.magnitude >= 40) {
			rb.AddForce (new Vector3 (0, 0, -1) * 0);
		}

	}
	void OnTriggerEnter(Collider other){

	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Outline") {
			reset ();
		}
	}



	IEnumerator wait(int secs) {
		
		yield return new WaitForSecondsRealtime(secs);
		Vector3 direction = new Vector3 (Random.Range(-1,1), 0, 1);
		print (direction);
		rb.velocity =  (direction * startSpeed);

	}

	void reset () {
		rb.velocity        = Vector3.zero;
		transform.position = start.transform.position;
		transform.rotation = new Quaternion (0, 0, 0, 0);

		StartCoroutine(wait (2));


	}

}
