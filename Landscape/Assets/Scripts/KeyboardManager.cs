using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyboardManager : MonoBehaviour {
    private float thrust = 0.1f;
    private float spin = 0.5f;
    private float tiltAngle = 10f;

    private GameObject LockedObject = null;
    private GameObject objClickedObject = null;
    static KeyCode HoldKey = KeyCode.Space;

    private bool hiding = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // rotate camera
        if (Input.GetKey(KeyCode.UpArrow))
            //Debug.Log("DownArrow was pressed.");
            Camera.main.transform.Rotate(new Vector3(spin, 0, 0));

        if (Input.GetKey(KeyCode.DownArrow))
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

        // tilt camera left
        if (Input.GetKey(KeyCode.A))
            Camera.main.transform.position -= Camera.main.transform.right * thrust;

        // tilt camera right
        if (Input.GetKey(KeyCode.D))
            Camera.main.transform.position += Camera.main.transform.right * thrust;

        // tilt camera Up
        if (Input.GetKey(KeyCode.R))
            Camera.main.transform.position += Camera.main.transform.up * thrust;

        // tilt camera down
        if (Input.GetKey(KeyCode.F))
            Camera.main.transform.position -= Camera.main.transform.up * thrust;

        //if (Input.GetKey(KeyCode.Tab))
        //{
        //    hiding = !hiding;

        //    if (hiding)
        //    {
        //        this.BroadcastMessage("HideNodes");
        //    } else
        //    {
        //        this.BroadcastMessage("ShowNodes");
        //    }   

        //}

        if (Input.GetKey(KeyCode.Comma))
        {
            this.BroadcastMessage("HideNodes");
        }

        if (Input.GetKey(KeyCode.Period))
        {
            this.BroadcastMessage("ShowNodes");
        }

        // lock the object
        if (Input.GetKeyDown(HoldKey))
        {

            RaycastHit hitInfo;
            Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayCamera, out hitInfo))
            {
                objClickedObject = hitInfo.collider.gameObject;
                
                if (objClickedObject != null)
                {
                    if (objClickedObject != LockedObject && objClickedObject != null)
                    {
                        LockedObject = objClickedObject;
                    }
                }
            }
            else
            {
                // unlock
                LockedObject = null;
            }
        }

        // unlock
       // if (Input.GetKeyUp(HoldKey))
        //    LockedObject = null;

        // if object is locked, look at it!!
        if (LockedObject != null)
            Camera.main.transform.LookAt(LockedObject.transform);



        //Debug.Log("Space key was released.");
        
    }
    
}
