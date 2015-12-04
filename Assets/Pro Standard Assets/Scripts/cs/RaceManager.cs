using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour {
	public GameObject[] carObjects;
	public Car_rm[] allCars;
	public Car_rm[] carOrder;
	public GUIHUD GUIHUD_Player;
	public Car_rm Player_Car;
	private int TotalNumberOfLaps;
	public bool FailRace;
	public string Announcement;
	public GameManager GManager;

	public void Start() {
		FailRace = false;
		Announcement = "";

		// set up the car objects
		carObjects = new GameObject[GameObject.FindGameObjectsWithTag("Race Contestant").Length]; 
		carObjects = GameObject.FindGameObjectsWithTag ("Race Contestant");
		GManager = GameObject.Find ("_Game Manager").GetComponent<GameManager> ();

	}
	public void LaunchRaceManager(){
		TotalNumberOfLaps = GManager.ReturnLaps (); 
		allCars = new Car_rm[carObjects.Length];
		carOrder = new Car_rm[carObjects.Length];
		for (int i = 0; i < carObjects.Length; i++) {
			allCars[i] = carObjects[i].GetComponent<Car_rm>();
			allCars[i].Initialize();
			//carOrder[i] = carObjects[i].GetComponent<Car_rm>();
		}

		Player_Car = GameObject.Find ("_Player").GetComponent<Car_rm>();
		GUIHUD_Player = GameObject.Find ("_GUI").GetComponent<GUIHUD>();
		GUIHUD_Player.setInitialValues(TotalNumberOfLaps);
	}
	
	// this gets called every frame
	public void FixedUpdate() {
		if (Player_Car.GetCarLap () > TotalNumberOfLaps && !FailRace) {
			Announcement = "You Win!";
			//Restart Level
		
		} else {
			foreach (Car_rm car in allCars) {
				carOrder [car.GetCarPosition (allCars) - 1] = car;
				if(car.GetCarLap()>TotalNumberOfLaps){
					Announcement = "Oh No! You Lost!";
					FailRace = true;
				}
			}
			GUIHUD_Player.changePosition (Player_Car.GetCarPosition (allCars));
			GUIHUD_Player.changeLap (Player_Car.GetCarLap ());
		}		 
	}
 
	public string CheckRaceStatus(){
		return Announcement;
	}
}