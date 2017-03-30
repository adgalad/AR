using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class TrackableList : MonoBehaviour {

	IEnumerable<TrackableBehaviour> activeTrackables;
	public GameObject audioMundoSuperado;
	bool playSound = true;
	bool ganoMundo = false;
	public GameObject ball;
	string mundoAnterior = "";

	public GameObject mundoActual;

	public GameObject gema1;
	public GameObject gema2;
	public GameObject gema3;

	IEnumerator PlayAudioMundoSuperado() {
		playSound = false;
		audioMundoSuperado.SetActive (true);
		AudioSource sonido = audioMundoSuperado.GetComponent<AudioSource> ();
		yield return new WaitForSeconds (sonido.clip.length);
		audioMundoSuperado.SetActive (false);
	}
	// Use this for initialization
	void Start () {
		mundoAnterior = "mundo1";

		audioMundoSuperado = GameObject.Find ("correct");
		audioMundoSuperado.SetActive (false);

		gema1 = GameObject.Find ("Gem1");
		gema1.SetActive (false);
		gema2 = GameObject.Find ("Gem2");
		gema2.SetActive (false);
		gema3 = GameObject.Find ("Gem3");
		gema3.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// Get the Vuforia StateManager
		StateManager sm = TrackerManager.Instance.GetStateManager ();		
	
		// Query the StateManager to retrieve the list of
		// currently 'active' trackables 
		//(i.e. the ones currently being tracked by Vuforia)
		activeTrackables = sm.GetActiveTrackableBehaviours ().OrderBy(go=>go.transform.position.x).ToList();
	
		if (activeTrackables.Count () > 0) {
			string nombreMundo = "M";
			int numeroMundo = 1;

			int index = 0;
			for (int i = 0; i < activeTrackables.Count (); i++) {
				string name = activeTrackables.ElementAt (i).TrackableName;
				Debug.Log(activeTrackables.ElementAt(i).TrackableName);
				if (name.Length > 5) {
					string subStr = name.Substring (0, 5);
					if (subStr == "mundo") {
						nombreMundo += name.Substring (1, 5);
						numeroMundo = Int32.Parse (name.Substring (5, 1));
						if (name == mundoAnterior) {
							Debug.Log ("yes");	
						} else {
							ganoMundo = false;
							if (gema1.activeSelf) {
								gema1.SetActive (false);
							}
							if (gema2.activeSelf) {
								gema2.SetActive (false);
							}
							if (gema3.activeSelf) {
								gema3.SetActive (false);
							}
						}
					} else {
						//Debug.Log (subStr);
					}
				}
			}
			mundoActual = GameObject.Find (nombreMundo);
			bool anyActiveBrick = false;
			GameObject ladrillos = GameObject.Find ("Ladrillos"+numeroMundo);
			foreach (Transform child in ladrillos.transform) {
				//Debug.Log(child.gameObject.activeSelf);
				if (child.gameObject.activeSelf) {
					anyActiveBrick = true;
				}
			}

			if (!anyActiveBrick && !ganoMundo) {
				ganoMundo = true;
				if (nombreMundo == "Mundo1") {
					gema1.SetActive (true);
					if (playSound) {
						StartCoroutine (PlayAudioMundoSuperado ());
						playSound = true;
					}
				}
				else if (nombreMundo == "Mundo2") {
					ball.GetComponent<Ball> ().lives = 20;
					ball.GetComponent<Ball> ().bricks = 5;
					gema1.SetActive (true);
					gema2.SetActive(true);

						StartCoroutine (PlayAudioMundoSuperado ());
						playSound = true;


				}

				else if (nombreMundo == "Mundo3") {
					ball.GetComponent<Ball> ().lives = 20;
					ball.GetComponent<Ball> ().bricks = 9;
					gema1.SetActive (true);
					gema2.SetActive(true);
					gema3.SetActive(true);

						StartCoroutine (PlayAudioMundoSuperado ());
						playSound = true;


				}

				Debug.Log ("ganaste");

				if (playSound) {
					StartCoroutine (PlayAudioMundoSuperado ());
				}

			}
		}


	}
}
