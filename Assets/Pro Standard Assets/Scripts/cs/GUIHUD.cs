using UnityEngine;
using System.Collections;

public class GUIHUD : MonoBehaviour {
	public int PlayerPosition;
	public int TotalNumberOfRacers;
	public int UserOnLap;
	public int TotalNumberOfLaps;
	public Car_rm PlayerCharacter;
	public GameManager GManager;
	public int Wins;
	public int Tracks;
	private bool paused;
	public int HowManyLaps;
	public float hSliderValue;
	public string stringtoEdit;
	private int dummyLapsValue;
	public Font f;
	public MusicScript MScript;
	public float localVolume;
	public float lapHandler;
	GUIStyle largeFont;
	GUIStyle moderateFont;


	void OnGUI(){
		GUI.backgroundColor = Color.clear;
		GUI.color = Color.red;
		GUI.skin.label.alignment = TextAnchor.UpperRight;
		GUI.skin.font = f;
		int c = UserOnLap + 1;
		int d = TotalNumberOfLaps + 1;
		string s = PlayerPosition.ToString () + "/" + TotalNumberOfRacers.ToString () +  " - Position\n";
		s += c.ToString() + "/" + d.ToString () + " - Lap\n";
		s +=  Wins + " - Wins\n";
		s +=  Tracks + "- Track";
		GUI.Box (new Rect (Screen.width - 150, 10, 140, 30),s,moderateFont);

		if (paused == true) {
			GUI.backgroundColor = Color.clear;
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width/2 -100, Screen.height/2 - 120, 200, 40), "Press 'I' to unpause",largeFont);
			GUI.Box (new Rect (Screen.width/2 -140, Screen.height/2 - 60,280, 30), "Change the number of Laps:", largeFont);
			GUI.Box (new Rect (Screen.width/2 -140, Screen.height/2 + 0,280, 30), "Change the Music Volume:", largeFont);
			GUI.backgroundColor = Color.red; 
			lapHandler = GUI.HorizontalSlider(new Rect(Screen.width/2 - 50, Screen.height/2 -20, 100, 30), lapHandler, 0.0F, 10.0F);
			HowManyLaps = (int)lapHandler;

			localVolume = GUI.HorizontalSlider(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 100, 30), localVolume, 0.0F, 0.99F);

			}
		}
	void Start(){
		largeFont = new GUIStyle ();
		largeFont.fontSize = 32;
		largeFont.normal.textColor= Color.red;
		largeFont.font = f;
		largeFont.alignment = TextAnchor.UpperCenter;
		moderateFont = new GUIStyle ();
		moderateFont.fontSize = 20;
		moderateFont.normal.textColor= Color.red;
		moderateFont.font = f;
		moderateFont.alignment = TextAnchor.UpperRight;


		GUI.color = Color.red;
		GUI.skin.label.alignment = TextAnchor.UpperRight;
		GUI.skin.font = f;

		paused = false;
		PlayerPosition = 0;
		TotalNumberOfRacers = 0;
		UserOnLap = 0;


	}
	public void SetPause(bool a){
		paused = a;
	
	}
	public void changePosition(int NewValue){

		PlayerPosition = NewValue;
	}
	public void setInitialValues(int TotalNumOfLaps){
		TotalNumberOfLaps = TotalNumOfLaps+1;
		TotalNumberOfRacers = GameObject.FindGameObjectsWithTag("Race Contestant").Length;

		PlayerCharacter = GameObject.Find ("_Player").GetComponent<Car_rm>(); 
		GManager = GameObject.Find("_Game Manager").GetComponent<GameManager>();
		MScript = GameObject.Find("Music Player").GetComponent<MusicScript>();
		localVolume = MScript.ReturnVolume ();
		Wins = GManager.GetWins ();
		Tracks = GManager.GetLevel ();
		TotalNumberOfLaps = GManager.ReturnLaps ();
		HowManyLaps = TotalNumberOfLaps;
		lapHandler = (float)TotalNumberOfLaps;
		dummyLapsValue = HowManyLaps;
	}
	void findNeededObjects(){
		GManager = GameObject.Find("_Game Manager").GetComponent<GameManager>();
		MScript = GameObject.Find("Music Player").GetComponent<MusicScript>();
	}

	void FixedUpdate(){
		PlayerCharacter.GetCarLap ();
		int a = HowManyLaps;
		if (a < 0) {
			a=0;		
		}
		GManager.SetLaps(a);
		MScript.SetVolume(localVolume);
	}
	public void changeLap (int NewValue){
		UserOnLap = NewValue;
	}


}