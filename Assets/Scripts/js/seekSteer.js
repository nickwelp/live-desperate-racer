 
 
var waypoints : Transform[];
var waypointRadius : float = 1.5;
var damping : float = 0.1;
var loop : boolean = false;
var faceHeading : boolean = true;
var canDrive : boolean = false;
var canSteer : boolean = true;
var DrivePower : int = 500000;
var TurnPower : float = 1.0f;
var throttle : int = 1;
var brakeForce : int = 1;
var FreezeCar : boolean;				
var MapMakerJS : createRaceWay;

var LookAtC : LookAtController;
var GameManager : GameManager; 

var previous: Vector3;
var velocity: float;
     
    
private var targetHeading : Vector3;
private var currentHeading : Vector3;
private var targetwaypoint : int = 0;
private var xform : Transform;
private var useRigidbody : boolean;
private var rigidmember : Rigidbody;
 
var localWayPoints : Vector3[];
private var localGameObjects : GameObject[];

public var CollisionClip : AudioSource;
public var EngineSound : AudioSource;

// Use this for initialization
function Start() {

FreezeCar = true;
var Tester : boolean =  (MapMakerJS.WayPoints.Length>0);
while(!Tester){	
	while(!Tester) yield;
	Tester = MapMakerJS.WayPoints.Length>0;
	yield WaitForSeconds(1.0);
}//won't stop tell MapMAker is done
localWayPoints = new Vector3[MapMakerJS.WayPoints.Length];
waypoints = new Transform[MapMakerJS.WayPoints.Length];
localGameObjects = new GameObject[MapMakerJS.WayPoints.Length];

xform = transform;
currentHeading = xform.forward;
var name1 : String;
for( var o :int = 0;o<localWayPoints.Length;o++){
	name1="WayPointSphere"+o.ToString();
	localGameObjects[o] = GameObject.Find(name1);
	waypoints[o] = localGameObjects[o].transform;
}
if(waypoints.Length<=0)
{
	Debug.Log("No waypoints on "+name);
	enabled = false;
}
targetwaypoint = 0;
useRigidbody = true;
rigidmember = GetComponent.<Rigidbody>();
LookAtC.ChangeTarget(waypoints[0]); 

}
 
 
// calculates a new heading
function FixedUpdate() {
	if(!FreezeCar){
	targetHeading = waypoints[targetwaypoint].position - xform.position;
	//Debug.Log("targetwaypint: "+targetwaypoint.ToString()); 
	currentHeading = Vector3.Lerp(currentHeading,targetHeading,damping*Time.deltaTime);
	Check_If_Car_Is_Stuck();
	Check_If_Car_Is_Flipped();
	//	rigidmember.velocity = currentHeading * speed;
	
	//	rigidmember.AddForce(transform.forward * Time.deltaTime * (throttle*5000000+ brakeForce));
		var mar : float = 1;
			if(!canDrive){
				mar = 0.1;
			}
		rigidmember.AddForce((transform.forward * Time.deltaTime * (throttle*DrivePower+ brakeForce))*mar);
	 
	if(Vector3.Distance(xform.position,waypoints[targetwaypoint].position)<=waypointRadius)
	{
	
		targetwaypoint++;
		 
		if(targetwaypoint>=waypoints.Length)
		{
			targetwaypoint = 0;
			if(!loop)
				enabled = false;
		}
		LookAtC.ChangeTarget(waypoints[targetwaypoint]);
	}
 	}
 	velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
    previous = transform.position;
	EngineSound.volume = velocity / 57.0;
	if (!EngineSound.isPlaying)
		{
			EngineSound.Play();
		}

     
}

var resetTimeFlipped : float = 3.0;
private var resetTimerFlipped : float = 0.0;

function Check_If_Car_Is_Flipped()
{
	if(transform.localEulerAngles.z > 80 && transform.localEulerAngles.z < 280)
		resetTimerFlipped += Time.deltaTime;
	else
		resetTimerFlipped = 0;
	
	if(resetTimerFlipped > resetTimeFlipped)
		FlipCar();
}
function FlipCar() 
{
	transform.rotation = Quaternion.LookRotation(transform.forward);
	transform.position += Vector3.up * 0.5;
	GetComponent.<Rigidbody>().velocity = Vector3.zero;
	GetComponent.<Rigidbody>().angularVelocity = Vector3.zero;
	resetTimerFlipped = 0;
	currentEnginePower = 0;
}


var resetTime : float = 10.0;
private var resetTimer : float = 0.0;
private var lastX:float=0.0;
private var lastY:float=0.0;
private var secondLastX:float=0.0;
private var secondLastY:float=0.0;

function Check_If_Car_Is_Stuck(){
	secondLastX = lastX;
	secondLastY = lastY;
	lastX = Mathf.Round(transform.position.x);
	lastY = Mathf.Round(transform.position.y);

	if(secondLastX==lastX&&secondLastY==lastY)
		resetTimer += Time.deltaTime;
	else
		resetTimer = 0;
	
	if(resetTimer > resetTime){
		MoveCar();
		Debug.Log("Reset Car");
		}
	
	
		//KillCar();
}

function OnCollisionEnter(collisionInfo : Collision){
 	if(collisionInfo.transform.name=="Road"){
 		canDrive = true;}
 	if(collisionInfo.transform.name=="done_explosion_enemy"||collisionInfo.transform.name=="bullet"){
		rigidmember.AddForce(Vector3(0,30,0) * 9000000 * Time.deltaTime);
 		Debug.Log("IMPACT BULLET");
 	}	
 	if (collisionInfo.relativeVelocity.magnitude > 3&&collisionInfo.transform.tag=="Race Contestant")
        CollisionClip.Play();
 
}

function OnCollisionExit(collisionInfo : Collision){
 	if(collisionInfo.transform.name=="Road"){
 			canDrive = false;
 		}
 
}

function StartRace(v : boolean){
	FreezeCar = v;

} 
  
    
function MoveCar(){
	var y : int = targetwaypoint-1;
	if(y<0){y=waypoints.Length-1;}
	transform.position = Vector3(waypoints[y].position.x,waypoints[y].position.y+Random.Range(2,22),waypoints[y].position.z);
	
}
// draws red line from waypoint to waypoint
function OnDrawGizmos(){
 
Gizmos.color = Color.red;
for(var i : int = 0; i< waypoints.Length;i++)
{
var pos : Vector3 = waypoints[i].position;
if(i>0)
{
var prev : Vector3 = waypoints[i-1].position;
Gizmos.DrawLine(prev,pos);
}
}
}