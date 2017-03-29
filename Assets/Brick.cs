using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public Material[] materials;
	public GameObject coin;

	private Renderer rend;
	private int currentDamage = 0;

	// Use this for initialization
	void Start () {
		if (coin != null) {
			coin.transform.position = transform.position;
			coin.SetActive (false);
		}
		rend = GetComponent <Renderer> ();
		rend.material = materials [0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Ball")
		{	
			Debug.Log (materials.Length);
			++currentDamage;
			if (currentDamage >= materials.Length) {
				col.gameObject.GetComponent<Ball> ().bricks--;
				gameObject.SetActive (false);
				if (coin != null) {
					coin.SetActive (true);
				}
				print (col.gameObject.GetComponent<Ball> ().bricks);
			} else {
				rend.material = materials [currentDamage];
			}
				

		}
	}
}
