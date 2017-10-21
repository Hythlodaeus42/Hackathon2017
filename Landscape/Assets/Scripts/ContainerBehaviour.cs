using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool toggleVisibility()
    {
        Debug.Log("toggleVisibility(): " + this.name);
        this.transform.gameObject.SetActive(!this.transform.gameObject.activeSelf);

        return this.transform.gameObject.activeSelf;
    }

}
