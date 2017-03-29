using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	public int lives;
	public int bricks = 9;
	public Text text;
	public GameObject gameOver;

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
		text.text = "Lives: " + lives;
		gameOver.SetActive (false);
		rend = GetComponent <Renderer> ();
		rb   = GetComponent <Rigidbody> ();
		initialPosition = new Vector3 (0f,0.216f, 0.281f);
		wait (1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 move = Vector3.zero;
		move.x = Input.GetAxis ("Horizontal");
		move.y = Input.GetAxis ("Vertical");
	
		rb.AddForce (move*100);

		if (rb.velocity.magnitude > 10 && rb.velocity.magnitude < 40) {
			rb.AddForce (rb.velocity.normalized * 40);
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
		rb.useGravity = false;
		yield return new WaitForSecondsRealtime(secs);
		Vector3 direction = new Vector3 (Random.Range(-1,1), 1, 0);
		print (direction);
		rb.velocity =  (direction * startSpeed);
		rb.useGravity = true;
	}

	void reset () {
		rb.velocity        = Vector3.zero;
		transform.position = start.transform.position;
		transform.rotation = new Quaternion (0, 0, 0, 0);
		if (lives-- == 1) {
			gameOver.SetActive (true);
			gameObject.SetActive (false);
		} else {
			StartCoroutine(wait (2));
		}
		text.text = "Lives: " + lives;



	}

}
