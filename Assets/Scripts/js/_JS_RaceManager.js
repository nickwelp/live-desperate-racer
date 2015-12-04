

var MapMakerJS : createRaceWay;
var RaceManager : RaceManager;
var GManager : GameManager;

var Level1 : String;
var waypoints : Transform[];
var Contestant : GameObject[];
var targetwaypoint : int = 0;
var SlopeAtPoint : Vector3[];
var InverseTangetAtPoint : Vector3[];
var rigidbdy : Component; 
var SeekSteerController : Component;
var PlaneCountsForRace : boolean[];
private var PaceCarFirstLap : boolean = false;
var showCountdown : boolean;
var showEnd : boolean;
var  f : Font;
          
var localWayPoints : Vector3[];
private var localGameObjects : GameObject[];
var waypointPlanes : GameObject[];
var countdown : String;
var Car : Car;
var SeekSteer : GameObject[];
var Win : boolean;  
private var EndRaceTrigger : boolean;
private var RaceOrDie : AudioSource;
private var YouLost : AudioSource;
private var YouWon : AudioSource;

var myStyle : GUIStyle = new GUIStyle();
    myStyle.fontSize = 30;
    myStyle.normal.textColor = Color.red;
// Use this for initialization
function Start() {
	Win = false;
	showCountdown = false;
	showEnd = false;
	Contestant = new GameObject[GameObject.FindGameObjectsWithTag("Race Contestant").Length];
	Contestant = GameObject.FindGameObjectsWithTag("Race Contestant");
	Car = GameObject.Find("_Player").GetComponent.<Car>();
	GManager = GameObject.Find("_Game Manager").GetComponent.<GameManager>();
	SeekSteer  = new GameObject[GameObject.FindGameObjectsWithTag("Race Contestant").Length-1];
	var i : int = 0; 
	for( var u :int = 0;u<GameObject.FindGameObjectsWithTag("Race Contestant").Length;u++){
		if(Contestant[u].GetComponent("seekSteer") != null){
			SeekSteer[i] = Contestant[u]; 
			i++;
		}
		Contestant[u].AddComponent.<Car_rm>();
	}
	var aSources = GetComponents(AudioSource); 
	RaceOrDie = aSources[0]; 
	YouWon = aSources[1];
	YouLost = aSources[2];
	EndRaceTrigger = false;
 
}


function LaunchRaceManager(){
	
	localWayPoints = new Vector3[MapMakerJS.WayPoints.Length];
	waypoints = new Transform[MapMakerJS.WayPoints.Length];
	localGameObjects = new GameObject[MapMakerJS.WayPoints.Length];
	waypointPlanes = new GameObject[MapMakerJS.WayPoints.Length];
	SlopeAtPoint = new Vector3[MapMakerJS.WayPoints.Length];
	InverseTangetAtPoint = new Vector3[MapMakerJS.WayPoints.Length];
	PlaneCountsForRace = new boolean[MapMakerJS.WayPoints.Length];

	
	
	
	var deltaX :float; 
	var deltaZ:float;
	var deltaY:float;
	var AngleAtPoint:float[];
	AngleAtPoint = new float[localWayPoints.Length];
	//Find slope at all localWayPoints
	
	
	
	var name1 : String;
	for( var o :int = 0;o<localWayPoints.Length;o++){
			name1="WayPointSphere"+o.ToString();
			localGameObjects[o] = GameObject.Find(name1);
			waypoints[o] = localGameObjects[o].transform;
			localWayPoints[o] = localGameObjects[o].transform.position;
			PlaneCountsForRace[o] = false;
	}
	for(var f : int = 0;f<localWayPoints.Length;f++){
   			deltaZ = localWayPoints[((f-1)+localWayPoints.Length)%localWayPoints.Length].z - localWayPoints[(f+1)%localWayPoints.Length].z;
   			deltaX = localWayPoints[((f-1)+localWayPoints.Length)%localWayPoints.Length].x - localWayPoints[(f+1)%localWayPoints.Length].x;
   			deltaY = localWayPoints[((f-1)+localWayPoints.Length)%localWayPoints.Length].y - localWayPoints[(f+1)%localWayPoints.Length].y;
   			//Debug.Log("DX"+deltaX+", DY:"+deltaY+", DZ:"+deltaZ);
   			SlopeAtPoint[f] = new Vector3(deltaX,deltaY,deltaZ);
   			InverseTangetAtPoint[f] = new Vector3(-deltaZ,deltaY,deltaX); //with no regard to Y FYI
   				AngleAtPoint[f] = deltaX/deltaZ;
     }
 	for( o = 0;o<localWayPoints.Length;o++){   	
     		waypointPlanes[o] = GameObject.CreatePrimitive(PrimitiveType.Plane);
     		waypointPlanes[o].transform.localScale = new Vector3(10,10,10);
			waypointPlanes[o].transform.position = localWayPoints[o];	  
			waypointPlanes[o].transform.eulerAngles = new  Vector3(0,Mathf.Atan(AngleAtPoint[o])*Mathf.Rad2Deg-90,90);//target;
   			rigidbdy = waypointPlanes[o].GetComponent(MeshCollider);
   	  		waypointPlanes[o].tag = "Race Controller Plane";
     		waypointPlanes[o].name = "Plane_"+o;
     		waypointPlanes[o].GetComponent.<MeshCollider>().convex = true;
     		waypointPlanes[o].GetComponent.<MeshCollider>().isTrigger = true;
     		waypointPlanes[o].GetComponent.<Renderer>().enabled = false;   
 	}
 	
	if(waypoints.Length<=10||waypoints.Length=='undefined')
	{
		Debug.Log("No waypoints on "+name1);
		enabled = false;
		Application.LoadLevel("basic race scene");
	}
	RaceManager.LaunchRaceManager();
	//FREEZE THE CARS
	CountDownRace();
	
	
}
 function CountDownRace(){
     showCountdown = true;    
 
     countdown = "3";    
     yield WaitForSeconds (1.5);  
 
     countdown = "2";    
     yield WaitForSeconds (1.5);
 
     countdown = "1";    
     yield WaitForSeconds (1.5);
 
     
 	 Car.StartRace(false);
 	 	for( var u :int = 0;u<GameObject.FindGameObjectsWithTag("Race Contestant").Length-1;u++){
 	 	 	 
			SeekSteerController = SeekSteer[u].GetComponent.<seekSteer>();
			SeekSteerController.StartRace(false);
		}
	 countdown = "GO";  
	 RaceOrDie.Play();  
     yield WaitForSeconds (1.5);
     GManager.AddLevelsPlayed();	
     showCountdown = false;
     countdown = "";  	
	}
	
function OnGUI(){
	 GUI.skin.font = f;
		
	
	
     if (showCountdown)
     {    
         GUI.backgroundColor = Color.clear;
         GUI.Box (Rect (Screen.width / 2 - 100, 50, 200, 175), "GET READY", myStyle);
 
         // display countdown    
         
         GUI.Box (Rect (Screen.width / 2 - 15, 95, 30, 140), countdown, myStyle);
         GUI.Box (Rect (Screen.width / 2 - 90, 200, 180, 240), "Press 'P' to Pause", myStyle);
         
     }  
     if(showEnd){
         GUI.backgroundColor = Color.clear;
         GUI.Box (Rect (Screen.width / 2 - 150, 75, 300, 140), countdown, myStyle);
     
     }  

}
	

function AlertManagerOfTriggerEvent(Name : String, Car : String){
	var words = Name.Split("_"[0]);
 	var theCount : int = int.Parse(words[1]);
 	/*
	if(Car =="Pace Car"){
		PlaneCountsForRace[theCount] = true;
		Debug.Log("<color=red>TriggerEvent:</color> " + Name + ", Car: " + Car);
	}
	8*/
}

function FixedUpdate(){
	var a = RaceManager.CheckRaceStatus();
	if(a!=""){
		if(a=="You Win!"){Win = true;}
		EndRace(a, Win);
	
	}
}


function EndRace(Announcement : String, Win : boolean){
     showEnd = true;    
 	if(!EndRaceTrigger){
 		 EndRaceTrigger = true;	
 		// YouWon.preloadAudioData();
 		 //YouLost.preloadAudioData();
 	  	  countdown = Announcement;    
    	 if(!Win){
			//YouLost.Play(); 	
 	  	}else{
 	  		//YouWon.Play();
 	  		GManager.AddWin();
 	  	
 	 		}
 	 	yield WaitForSeconds (4.0);
 	 	if(!Win){
 	  		Car.Destruct();
 	 	}
 	 	yield WaitForSeconds (1.0);  
     
     	Application.LoadLevel(Level1);
      
	}
}
 