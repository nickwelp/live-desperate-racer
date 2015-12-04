using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour
{
		public AudioSource SoundTrack;
		private bool FiredOnce;
		public float Volume;
		// Use this for initialization
		void Start ()
		{
			FiredOnce = false;		
			Volume = 0.30F;
		}
		void Awake(){
			DontDestroyOnLoad (this.gameObject);
			
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
		if (Volume > 1.0F) {
			Volume = .99F;
		} else if (Volume < 0.01F) {
		
			Volume = 0.0F;
		}
		SoundTrack.volume = Volume;
		if (!SoundTrack.isPlaying) {
				SoundTrack.Play();	
			}
		if (!FiredOnce) {
				Application.LoadLevel ("_main menu");
				FiredOnce = true;
			 
				}
		}
		public void SetVolume(float a){
			Volume = a;	
		}
		public float ReturnVolume(){
			return Volume;
		}
}

