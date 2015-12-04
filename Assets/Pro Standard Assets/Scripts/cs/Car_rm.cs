using UnityEngine;
using System.Collections;

public class Car_rm : MonoBehaviour {
	public int currentWaypoint;
	public int currentLap;
	public Transform lastWaypoint;
	private static int TEST_WAYPOINT = 30; //There is always a 30th way point to verify user doesn't dbl back on race
	private bool AllowLap = false;
	private static int WAYPOINT_VALUE = 100;
	private static int LAP_VALUE = 10000;
	
	// Use this for initialization
	public void Initialize() {
		currentWaypoint = 1;
		currentLap = 0;
		lastWaypoint = GameObject.Find("Plane_1").transform;
	}
	
	public void OnTriggerEnter(Collider other) {
		string otherTag = other.gameObject.name;
		string PlaneCount = otherTag.Substring(6);
		currentWaypoint = System.Convert.ToInt32(PlaneCount);
		lastWaypoint = other.transform;
		if (currentWaypoint >= TEST_WAYPOINT && currentWaypoint - 5 <= TEST_WAYPOINT ) { //ensure racer made it to 30th lap, within an error range
			AllowLap = true;
		}
		if (currentWaypoint == 0 && AllowLap) { // completed a lap, so increase currentLap;
			currentLap++;
			AllowLap = false;
		}
	}
	
	public float GetDistance() {
		return  (transform.position - lastWaypoint.position).magnitude + currentWaypoint * WAYPOINT_VALUE + currentLap * LAP_VALUE;
	}
	
	public int GetCarPosition(Car_rm[] allCars) {
		float distance = GetDistance();
		int position = 1;
		foreach (Car_rm car in allCars) {
			if (car.GetDistance() > distance)
				position++;
		}
		return position;
	}
	public int GetCarLap(){
		return currentLap;

	}
}
