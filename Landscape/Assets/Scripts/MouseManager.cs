﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {
	private GameObject hitObject;
    public GameObject LastClickedObject = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log ("pressed left");
            RaycastHit hitInfo;
            Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayCamera, out hitInfo))
            {
                hitObject = hitInfo.collider.gameObject;

                //Debug.Log(hitObject.name);

                if (hitObject != null)
                {
                    //Debug.Log(hitObject.name);
                    hitObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);


                }

                if (LastClickedObject != hitObject && LastClickedObject != null)
                {
                    LastClickedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
                }

                if (hitObject.GetComponent<NodeBehaviour>() != null)
                {
                    // only save nodes and blocks
                    LastClickedObject = hitObject;
                } else
                {
                    LastClickedObject = null;
                }
                

            }
            else
            {
                if (LastClickedObject != null)
                {
                    LastClickedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
                    LastClickedObject = null;
                }
            }

        }
    }

		
}
