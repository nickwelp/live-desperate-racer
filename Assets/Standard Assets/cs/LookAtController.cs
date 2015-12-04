using UnityEngine;
using System.Collections;

public class LookAtController : MonoBehaviour
{		
		public int constantForceX;  
		
		private readonly VectorPid angularVelocityController = new VectorPid(33.7766f, 0, 0.2553191f);
		private readonly VectorPid headingController = new VectorPid(9.244681f, 0, 0.06382979f);
		
		public Transform target;
		
		public void FixedUpdate ()
		{
		var angularVelocityError = GetComponent<Rigidbody> ().angularVelocity  * -1;
		Debug.DrawRay(transform.position, GetComponent<Rigidbody>().angularVelocity, Color.black);
			
		var angularVelocityCorrection = angularVelocityController.AdjustError(angularVelocityError, Time.deltaTime);
			Debug.DrawRay(transform.position, angularVelocityCorrection, Color.green);
			
			GetComponent<Rigidbody>().AddTorque(angularVelocityCorrection);
			
			var desiredHeading = target.position - transform.position;
			Debug.DrawRay(transform.position, desiredHeading, Color.magenta);
			
			var currentHeading = transform.forward;
			Debug.DrawRay(transform.position, currentHeading * 15, Color.blue);
			
			var headingError = Vector3.Cross(currentHeading, desiredHeading);
		var headingCorrection = headingController.AdjustError(headingError  * constantForceX, Time.deltaTime);
			
			GetComponent<Rigidbody>().AddTorque(headingCorrection);
		}
	public void ChangeTarget(Transform NextWayPoint){
		target = NextWayPoint;

	}
}
