using UnityEngine;
using System.Collections;

public class RaceManager : MonoBehaviour {
	public GameObject[] carObjects;
	public Car[] allCars;
	public Car[] carOrder;
	
	public void Start() {
		// set up the car objects
		carObjects = new GameObject[GameObject.FindGameObjectsWithTag("Race Contestants").Length]; 
		carObjects = GameObject.FindGameObjectsWithTag ("Race Contestants");
	}
	public void LaunchRaceManager(){

		allCars = new Car[carObjects.Length];
		carOrder = new Car[carObjects.Length];
		for (int i = 0; i < carObjects.Length; i++) {
			allCars[i] = carObjects[i].GetComponent<Car>();
		}
		
	
	}
	
	// this gets called every frame
	public void update() {
		foreach (Car car in allCars) {
			carOrder[car.GetCarPosition(allCars) - 1] = car;
		}
	}
}