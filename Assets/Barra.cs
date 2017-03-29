using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barra : MonoBehaviour {

	public int score = 0;
	public Text text;

	// Use this for initialization
	void Start () {
		text.text = "Score: " + score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Coin") { 
			other.gameObject.SetActive (false);
			score += 300;
			text.text = "Score: " + score;
		}
	}

}
