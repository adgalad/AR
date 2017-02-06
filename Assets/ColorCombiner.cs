using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ColorCombiner : MonoBehaviour {

	public GameObject viewerObject;


	private ARMarker[] markers;
	private ARMarker red_marker;
	private ARMarker blue_marker;
	private ARMarker yellow_marker;
	private ARMarker viewer_marker;


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
			else
				Debug.LogError ("Unknow Marker" + m.tag);
		}
	}
	
	// Update is called once per frame
	void Update () {
		string str = "";


		Renderer rend = viewerObject.GetComponent<Renderer>();
		Color c = GetCurrentColor();
		rend.material.SetColor("_Color", c);

	
	}

	void ChangeColor (COLOR_MASK color){
		// Disable current Figure
		figures [(int)currentColor - 1] [currentFigureIndex].SetActive (false);
		// Enable the new Figure
		figures [(int)color - 1] [0].SetActive (true);
	}

	void ChangeFigure () {
		int cIx = (int)currentColor - 1;

		// Disable current Figure
		figures [cIx] [currentFigureIndex].SetActive (false);
		// Enable the new Figure
		figures [cIx] [++currentFigureIndex].SetActive (true);
	}

	Color GetCurrentColor(){
		COLOR_MASK colorMask = COLOR_MASK.BLACK;


		if (red_marker.Visible)
			colorMask |= COLOR_MASK.RED;
		if (blue_marker.Visible)
			colorMask |= COLOR_MASK.BLUE;
		if (yellow_marker.Visible)
			colorMask |= COLOR_MASK.YELLOW;

		switch (colorMask) {
		case COLOR_MASK.RED:
			return Color.red;

		case COLOR_MASK.BLUE:
			return Color.blue;

		case COLOR_MASK.VIOLET:
			return new Vector4 (1, 0, 1, 1);

		case COLOR_MASK.YELLOW:
			return Color.yellow;

		case COLOR_MASK.ORANGE:
			return new Vector4 (1, 0.647058823529412f, 0, 1);

		case COLOR_MASK.GREEN:
			return Color.green;

		default:
			return Color.black;
		}
	}
}
