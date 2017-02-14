using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Colorizing : MonoBehaviour {




	private ARMarker[] markers;
	private ARMarker red_marker;
	private ARMarker blue_marker;
	private ARMarker yellow_marker;

	private ARMarker viewer_marker;

	private ARMarker left_button_marker;
	private ARMarker right_button_marker;




//	public GameObject yesButtonRed;
	public GameObject noButtonRed;

//	public GameObject yesButtonBlue;
	public GameObject noButtonBlue;

//	public GameObject yesButtonYellow;
	public GameObject noButtonYellow;


//	public GameObject figure3;
//	public GameObject figure4;

	//	public GameObject yellow_figure1;
	//	public GameObject yellow_figure2;
	//
	//	public GameObject orange_figure1;
	//	public GameObject orange_figure2;
	//
	//	public GameObject green_figure1;
	//	public GameObject green_figure2;
	//
	//	public GameObject violet_figure1;
	//	public GameObject violet_figure2;

	private int nFigures;
	private bool changed = false;
	private bool colorChanged = true;

	public GameObject[] figures;
	private int currentFigureIndex  = 0;
	private COLOR_MASK currentColor = COLOR_MASK.BLACK;

	private enum COLOR_MASK : int {
		BLACK  = 0x0,
		RED    = 0x1,
		BLUE   = 0x2,
		VIOLET = 0x3,
		YELLOW = 0x4,
		ORANGE = 0x5,
		GREEN  = 0x6,
	};


	// Use this for initialization
	void Start () {
		markers = FindObjectsOfType (typeof(ARMarker)) as ARMarker[];
		foreach (ARMarker m in markers) {
			if (m.Tag == "Red_marker")
				red_marker = m;
			else if (m.Tag == "Blue_marker")
				blue_marker = m;
			else if (m.Tag == "Yellow_marker")
				yellow_marker = m;
			else if (m.Tag == "Viewer_marker")
				viewer_marker = m;
			else if (m.Tag == "Right_button_marker")
				right_button_marker = m;
			else if (m.Tag == "Left_button_marker")
				left_button_marker = m;
			else
				Debug.LogError ("Unknow Marker" + m.tag);
		}


//		figures[2]    = figure3;
//		figures[3]    = figure4;

		nFigures = figures.Length;
		foreach (GameObject obj in figures) {
			if (obj != null) {
				obj.SetActive (false);
				obj.tag = "NotVisible";
			}
		}
		figures [0].SetActive (true);
		figures [0].tag = "Visible";
		//		if (figures [0] [0] == null)
		//			Application.Quit ();



	}

	// Update is called once per frame
	void Update () {
		string str = "";
		//		ChangeColor (GetCurrentColor ());
		PressButton ();
		ChangeColor ();
	}

	void ChangeColor(){
		GameObject obj = figures [currentFigureIndex];
		obj.GetComponent<Renderer> ().material.color = maskToColor ();
	}

	Color maskToColor () {
		switch (currentColor) {
		case COLOR_MASK.BLUE:
			return Color.blue;
		case COLOR_MASK.RED:
			return Color.red;
		case COLOR_MASK.YELLOW:
			return Color.yellow;
		case COLOR_MASK.ORANGE:
			return new Color(1F, 0.47F,0);
		case COLOR_MASK.GREEN:
			return Color.green;
		case COLOR_MASK.VIOLET:
			return new Color (1,0,1,0);
		}
		return Color.white;
	}

	void PressButton () {
		if (viewer_marker.Visible) {
			if (!blue_marker.Visible && !colorChanged) {
				currentColor ^= COLOR_MASK.BLUE;
				colorChanged = true;
				if (noButtonBlue.tag == "Visible") {
					noButtonBlue.tag = "NotVisible";
				} else if (noButtonBlue.tag == "NotVisible") {
					noButtonBlue.tag = "Visible";
				}
//				obj.SetActive (!obj.active);

			} else if (!red_marker.Visible && !colorChanged) {
				currentColor ^= COLOR_MASK.RED;
				colorChanged = true;
				Debug.Log (noButtonRed.tag);
				if (noButtonRed.tag == "Visible") {
					noButtonRed.tag = "NotVisible";
				} else if (noButtonRed.tag == "NotVisible") {
					noButtonRed.tag = "Visible";
				}
//				obj.SetActive (!obj.active);
			} else if (!yellow_marker.Visible && !colorChanged) {
				currentColor ^= COLOR_MASK.YELLOW;
				colorChanged = true;
				GameObject obj = noButtonYellow;
				if (noButtonYellow.tag == "Visible") {
					noButtonYellow.tag = "NotVisible";
				} else if (noButtonYellow.tag == "NotVisible") {
					noButtonYellow.tag = "Visible";
				}
//				obj.SetActive (!obj.active);
			} else if (!left_button_marker.Visible && !changed) {
				figures [currentFigureIndex].SetActive (false);
				figures [currentFigureIndex].tag = "NotVisible";
				currentFigureIndex = (currentFigureIndex + 1) % nFigures;
				changed = true;
				figures [currentFigureIndex].SetActive (true);
				figures [currentFigureIndex].tag = "Visible";
				ResetColors ();
			} else if (!right_button_marker.Visible && !changed) {
				figures [currentFigureIndex].SetActive (false);
				figures [currentFigureIndex].tag = "NotVisible";
				if (currentFigureIndex == 0) {
					currentFigureIndex = nFigures - 1;
				} else {
					currentFigureIndex = (currentFigureIndex - 1) % nFigures;
				}
				figures [currentFigureIndex].SetActive (true);
				figures [currentFigureIndex].tag = "Visible";
				changed = true;
				ResetColors ();
			} else if (blue_marker.Visible && red_marker.Visible && yellow_marker.Visible && 
				right_button_marker.Visible && left_button_marker.Visible) 
			{
				colorChanged = false;
				changed = false;
			}
		}
	}

	void ResetColors() {
		currentColor = COLOR_MASK.BLACK;
		noButtonBlue.tag = "Visible";
		noButtonBlue.SetActive (true);
		noButtonRed.tag = "Visible";
		noButtonRed.SetActive (true);
		noButtonYellow.tag = "Visible";
		noButtonYellow.SetActive (true);
			
	}
}
