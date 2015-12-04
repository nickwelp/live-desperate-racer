using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public string Level1;
	public string Level2;
	public Texture background;
	public Texture logo;
	public Texture arcade;
	public Texture about;
	public Texture options;
	public Font f;

	private float middleWidthSetter = Screen.width / 2;
	private float middleHeightSetter = Screen.height / 2;
	private float middleWidthAdjuster = Screen.width *0.1f;
	private float middleHeightAdjuster = Screen.height *0.1f;
	private float marqueeWidth = Screen.width *0.3f;
	private float marqueeHeight = Screen.height *0.3f;
	public AudioSource DesperateRacerVoice;
	GUIStyle largeFont;

	void OnGUI () {
		GUI.backgroundColor = Color.clear;

		GUI.skin.label.alignment =TextAnchor.UpperCenter;;
		// Make a background box
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height),  background);
		GUI.DrawTexture (new Rect (middleWidthSetter-marqueeWidth, middleHeightSetter-marqueeHeight-10, marqueeWidth*2, marqueeWidth*0.31262f+10),  logo);
		GUI.skin.font = f;
		GUI.color = Color.red;
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect (Screen.width/2 -100, Screen.height/2 - 50, 200, 40), "Click Here to Begin",largeFont)) {
			Application.LoadLevel(Level1);
		}

		/*
		// Make the second button.
		if(GUI.Button(new Rect(middleWidthSetter-(2*middleWidthAdjuster),middleHeightSetter-middleHeightAdjuster+(((middleWidthAdjuster+middleWidthAdjuster)*2)*0.2056f)+5,(middleWidthAdjuster+middleWidthAdjuster)*2,((middleWidthAdjuster+middleWidthAdjuster)*2)*0.2056f), about)) {
			Application.LoadLevel(Level2);
		}
		
		if(GUI.Button(new Rect(middleWidthSetter-(2*middleWidthAdjuster),middleHeightSetter-middleHeightAdjuster+(((middleWidthAdjuster+middleWidthAdjuster)*2)*0.2056f)*2+5,(middleWidthAdjuster+middleWidthAdjuster)*2,((middleWidthAdjuster+middleWidthAdjuster)*2)*0.2056f), options)) {
			Application.LoadLevel(Level2);
		}
		*/
	}
	void Start(){
		largeFont = new GUIStyle ();
		largeFont.fontSize = 32;
		largeFont.normal.textColor = Color.red;
		largeFont.font = f;
		largeFont.alignment =TextAnchor.UpperCenter;
		DesperateRacerVoice.Play ();

	}

}
