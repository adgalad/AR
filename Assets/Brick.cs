using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public Material[] materials;

	private Renderer rend;
	private int currentDamage = 0;

	// Use this for initialization
	void Start () {
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
				gameObject.SetActive (false);
			} else {
				rend.material = materials [currentDamage];
			}
				

		}
	}
}
