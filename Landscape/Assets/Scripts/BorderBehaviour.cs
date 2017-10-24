using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderBehaviour : MonoBehaviour {
    private bool selected;

    // Use this for initialization
    void Start () {
        selected = false;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("border clicked");
	}
}
