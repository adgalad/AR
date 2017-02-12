using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ColorCombiner : MonoBehaviour {




	private ARMarker[] markers;
	private ARMarker red_marker;
	private ARMarker blue_marker;
	private ARMarker yellow_marker;

	private ARMarker viewer_marker;

	private ARMarker left_button_marker;
	private ARMarker right_button_marker;




	public GameObject red_figure1;
	public GameObject red_figure2;
	public GameObject red_figure3;

	public GameObject blue_figure1;
	public GameObject blue_figure2;
	public GameObject blue_figure3;

	public GameObject yellow_figure1;
	public GameObject yellow_figure2;
	public GameObject yellow_figure3;

	public GameObject orange_figure1;
	public GameObject orange_figure2;
	public GameObject orange_figure3;

	public GameObject green_figure1;
	public GameObject green_figure2;
	public GameObject green_figure3;

	public GameObject violet_figure1;
	public GameObject violet_figure2;
	public GameObject violet_figure3;

	private int nFigures = 3;
	private bool changed = false;

	public GameObject[/*Number of colors*/][/*nNumber of figures per color*/] figures;
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

		figures = new GameObject [6] [];
		figures[0] = new GameObject [nFigures];
		figures[1] = new GameObject [nFigures];
		figures[2] = new GameObject [nFigures];
		figures[3] = new GameObject [nFigures];
		figures[4] = new GameObject [nFigures];
		figures[5] = new GameObject [nFigures];

		figures[(int)COLOR_MASK.RED-1][0] = red_figure1;
		figures[(int)COLOR_MASK.RED-1][1] = red_figure2;
		figures[(int)COLOR_MASK.RED-1][2] = red_figure3;

		figures[(int)COLOR_MASK.BLUE-1][0] = blue_figure1;
		figures[(int)COLOR_MASK.BLUE-1][1] = blue_figure2;
		figures[(int)COLOR_MASK.BLUE-1][2] = blue_figure3;

		figures[(int)COLOR_MASK.VIOLET-1][0] = yellow_figure1;
		figures[(int)COLOR_MASK.VIOLET-1][1] = yellow_figure2;
		figures[(int)COLOR_MASK.VIOLET-1][2] = yellow_figure3;

		figures[(int)COLOR_MASK.YELLOW-1][0] = orange_figure1;
		figures[(int)COLOR_MASK.YELLOW-1][1] = orange_figure2;
		figures[(int)COLOR_MASK.YELLOW-1][2] = orange_figure3;

		figures[(int)COLOR_MASK.ORANGE-1][0] = green_figure1;
		figures[(int)COLOR_MASK.ORANGE-1][1] = green_figure2;
		figures[(int)COLOR_MASK.ORANGE-1][2] = green_figure3;

		figures[(int)COLOR_MASK.GREEN-1][0] = violet_figure1;
		figures[(int)COLOR_MASK.GREEN-1][1] = violet_figure2;
		figures[(int)COLOR_MASK.GREEN-1][2] = violet_figure3;

		foreach (GameObject[] objs in figures){
			foreach (GameObject obj in objs){
				if (obj != null) {
					obj.SetActive (false);
					obj.tag = "NotVisible";
				}
			}
		}
//		figures [0] [0].SetActive (true);
//		figures [0] [0].tag = "Visible";
//		if (figures [0] [0] == null)
//			Application.Quit ();


	}

	// Update is called once per frame
	void Update () {
		string str = "";
		if (!ChangeColor (GetCurrentColor ())) {
			PressButton ();
		}

	}


	void PressButton () {
		if (viewer_marker.Visible && currentColor != COLOR_MASK.BLACK) {
			if (!left_button_marker.Visible && !changed) {
				PreviousFigure ();
				changed = true;
			} else if (!right_button_marker.Visible && !changed) {
				NextFigure ();
				changed = true;
			} else if (right_button_marker.Visible && left_button_marker.Visible && changed) {
				changed = false;
			}
		}

	}
	bool ChangeColor (COLOR_MASK color){
		// Disable current Figure
		if (currentColor != COLOR_MASK.BLACK) {
			figures [(int)currentColor - 1] [currentFigureIndex].SetActive (false);
			figures [(int)currentColor - 1] [currentFigureIndex].tag = "NotVisible";
		}
		// Enable the new Figure
		currentFigureIndex = 0;
		currentColor = color;
		if (color != COLOR_MASK.BLACK) {
			Debug.Log (color);
			figures [(int)color - 1] [0].SetActive (true);
			figures [(int)color - 1] [0].tag = "Visible";
			return true;
		}
		return false;
	}

	void NextFigure () {
		int cIx = (int)currentColor - 1;

		// Disable current Figure
		if (currentColor != COLOR_MASK.BLACK) {
			figures [cIx] [currentFigureIndex].SetActive (false);
			figures [cIx] [currentFigureIndex].tag = "NotVisible";
			// Enable the new Figure
			currentFigureIndex = (currentFigureIndex + 1) % nFigures;
			figures [cIx] [currentFigureIndex].SetActive (true);
			figures [cIx] [currentFigureIndex].tag = "Visible";
		}
	}

	void PreviousFigure () {
		int cIx = (int)currentColor - 1;

		if (currentColor != COLOR_MASK.BLACK) {
			// Disable current Figure
			figures [cIx] [currentFigureIndex].SetActive (false);
			figures [cIx] [currentFigureIndex].tag = "NotVisible";
			// Enable the new Figure
			if (currentFigureIndex == 0) {
				currentFigureIndex = nFigures - 1;
			} else {
				currentFigureIndex = (currentFigureIndex - 1) % nFigures;
			}
			figures [cIx] [currentFigureIndex].SetActive (true);
			figures [cIx] [currentFigureIndex].tag = "Visible";
		}
	}

	COLOR_MASK GetCurrentColor(){
		COLOR_MASK colorMask = COLOR_MASK.BLACK;


		if (red_marker.Visible)
			colorMask |= COLOR_MASK.RED;
		if (blue_marker.Visible)
			colorMask |= COLOR_MASK.BLUE;
		if (yellow_marker.Visible)
			colorMask |= COLOR_MASK.YELLOW;

		return colorMask;
		
	}
}
