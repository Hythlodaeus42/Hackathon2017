using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {
    private float thrust = 0.1f;
    private float spin = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // rotate camera
        if (Input.GetKey(KeyCode.DownArrow))
            //Debug.Log("DownArrow was pressed.");
            Camera.main.transform.Rotate(new Vector3(spin, 0, 0));

        if (Input.GetKey(KeyCode.UpArrow))
            //Debug.Log("DownArrow was pressed.");
            Camera.main.transform.Rotate(new Vector3(-spin, 0, 0));

        if (Input.GetKey(KeyCode.LeftArrow))
            //Debug.Log("DownArrow was pressed.");
            Camera.main.transform.Rotate(new Vector3(0, -spin, 0));

        if (Input.GetKey(KeyCode.RightArrow))
            //Debug.Log("DownArrow was pressed.");
            Camera.main.transform.Rotate(new Vector3(0, spin, 0));

        // move camera
        if (Input.GetKey(KeyCode.S))
            //Debug.Log("DownArrow was pressed.");
            Camera.main.transform.position -= Camera.main.transform.forward * thrust;

        if (Input.GetKey(KeyCode.W))
            //Debug.Log("DownArrow was pressed.");
            Camera.main.transform.position += Camera.main.transform.forward * thrust;

        //if (Input.GetKey(KeyCode.A))
        //    //Debug.Log("DownArrow was pressed.");
        //    transform.position += this.transform * thrust;

        //if (Input.GetKey(KeyCode.D))
        //    //Debug.Log("DownArrow was pressed.");
        //    transform.Rotate(new Vector3(0, 0.5f, 0));

        //if (Input.GetKeyUp(KeyCode.Space))
        //    Debug.Log("Space key was released.");
    }
}
