using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour{
		public bool paused;
		public StateType state;
		public int GameLevel;	
		public int Laps;	
		public int LevelsPlayed;
		public int wins;
		public int HowManyLaps;
		public float hSliderValue;
		public GUIHUD _GUIHUD;	

		public static GameManager instance; 
		public static GameManager Instance {
			get{
				if(instance==null) {
					instance = new GameManager();
				}
			return instance;	
			}
		}
		
		// Add your game mananger members here
		public void Pause(bool pause) {
			if(pause){
				paused = true;
				PauseGUI(paused);
				Time.timeScale = 0f;
				
			}else{
				paused = false;
				PauseGUI(paused);
				Time.timeScale = 1.0f;
			}			
		}
		void PauseGUI(bool a){
			
			_GUIHUD = GameObject.Find ("_GUI").GetComponent<GUIHUD> ();
			if (_GUIHUD) {
				_GUIHUD.SetPause(a);
			}
		}
		public bool GetPauseState(){
			return paused;


		}
		void Start(){
			state = StateType.MENU; 	
			LevelsPlayed = 0;
			wins = 0;
			paused = false;
			HowManyLaps = 0;
		}
	    
		void Awake(){
			DontDestroyOnLoad (this.gameObject);

		}
		public void AddWin(){
			wins++;
	 

		}
		public void AddLevelsPlayed(){
			LevelsPlayed++;
			 
		}
		public int GetWins(){
			return wins;

		}
		public int GetLevel(){
			return LevelsPlayed;
		}
		public void SetLaps(int laps){
			HowManyLaps = laps;
		}
		public int ReturnLaps(){
			return HowManyLaps;
		}
	    
	}


public enum StateType
{
	DEFAULT,      //Fall-back state, should never happen
	TESTING_MAP,
	RACING,      //RACE IS UNDERWAY
	PRACTICE,    //Free drive mode
	TRAINING,      //WORKING WITH NEURAL NET
	COUNTDOWN,		//3 2 1 before race starts
	PAUSED, 
	CHECKMAP,       //Analyzing map for drive ability, check if need to reload
	LOADMAP,     //Loading Map from save state
	SAVEMAP,     //Save Map to save space
	MENU,
	OPTIONS,
	LOBBY
};
//