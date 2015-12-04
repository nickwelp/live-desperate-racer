var RaceManagerAvatar : GameObject;
var RaceManagerScript : _JS_RaceManager;
var PlaneName : String;

// Use this for initialization
function Start () {
	RaceManagerAvatar = GameObject.Find("_Race Manager");
	RaceManagerScript = RaceManagerAvatar.GetComponent.<_JS_RaceManager>();

}

// Update is called once per frame
function Update () {
}
/*
function OnTriggerEnter(theCollision : Collider ){
	if(theCollision.gameObject.tag == "Race Contestant"){
		
		RaceManagerScript.AlertManagerOfTriggerEvent(PlaneName, theCollision.gameObject.name);
	
	}

}
*/

