using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
	public int currentWaypoint;
	public int currentLap;
	public Transform lastWaypoint;
	
	private static int WAYPOINT_VALUE = 100;
	private static int LAP_VALUE = 10000;
	
	// Use this for initialization
	public void Initialize() {
		currentWaypoint = 0;
		currentLap = 0;
	}
	
	public void OnTriggerEnter(Collider other) {
		string otherTag = other.gameObject.name;
		string PlaneCount = otherTag.Substring(6);
		currentWaypoint = System.Convert.ToInt32(PlaneCount);
		if (currentWaypoint == 1) // completed a lap, so increase currentLap;
			currentLap++;
		lastWaypoint = other.transform;
	}
	
	public float GetDistance() {
		return (transform.position - lastWaypoint.position).magnitude + currentWaypoint * WAYPOINT_VALUE + currentLap * LAP_VALUE;
	}
	
	public int GetCarPosition(Car[] allCars) {
		float distance = GetDistance();
		int position = 1;
		foreach (Car car in allCars) {
			if (car.GetDistance() > distance)
				position++;
		}
		return position;
	}
}
