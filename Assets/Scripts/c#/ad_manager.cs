using UnityEngine;
using System.Collections;

public class ad_manager : MonoBehaviour {
	public string Level1;
	public bool hideAd;
	 
	// Use this for initialization
	void Start () {
		hideAd = false;
		CpmbManager.showAd("J7NexscuGbUsoFxk", LoadLevel, hideAd);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoadLevel(){
		hideAd = true;
		CpmbManager.removeAd ();
		Application.LoadLevel(Level1);

	}
}
