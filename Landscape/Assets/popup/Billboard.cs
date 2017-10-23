using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
    private string myName = "billboard";
    public TextMesh thisText;
	public TextMesh getTextDisplay(){
		return thisText;
	}
	// Use this for initialization
	public void setTextMesh( TextMesh tm){
		thisText = tm;
	}
	void Start () {
		if (thisText == null) {
			Debug.LogWarning("billboard not set");
		}
		if (thisText != null) {
			thisText.text = "Info Here";
		} else {
			Debug.LogWarning ("The Info display need to be created");
		}
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
