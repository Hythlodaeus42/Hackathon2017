using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
	public AudioSource myAudio;
	public AudioClip myClip;
	public GameObject myOwner;
	//to do to enhance
	//Make sure that a sounc clip is aaded to each object ( prefab)
	//when a prefab is looked at or selected the press of F1 or space can activate the voice over
	//To automatically load teh sounds we can make teh name of the soung the same as that if the prefab, extensions 
	// will be different
	// Use this for initialization
	void Start () {
		if (myOwner == null) {
			Debug.LogError ("The Owner should not be null");
			return;
		}
		myOwner.AddComponent<AudioSource> ();
		myAudio = myOwner.GetComponent<AudioSource> ();
		if (myAudio == null) {
			Debug.LogError ("Audio sould not be null");
			return;
		} else {
			myAudio.playOnAwake = true;
		}
		myClip = Resources.Load ("TestSound") as AudioClip;
		if (myClip == null) {
			Debug.LogError ("AusioCLip should not be null");		
		
		} else {
			myAudio.clip = myClip;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !myAudio.isPlaying) {
			myAudio.Play ();
		}
		
	}
}
