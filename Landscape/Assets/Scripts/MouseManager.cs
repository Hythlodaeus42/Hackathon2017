﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {
	private GameObject hitObject; 
    private float nextClick = 0;
    private bool click = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (click)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log ("pressed left");
                RaycastHit hitInfo;
                Ray rayCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(rayCamera, out hitInfo))
                {
                    hitObject = hitInfo.collider.gameObject;

                    if (hitObject != null)
                    {
                        Debug.Log(hitObject.name);
                        hitObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
                    }

                }
                //else
                //{
                //    hitObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
                //}

                //nextClick = Time.time + 0.2f;
                //click = false;

            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Click2");
                    click = true;
                }
            }
        }

			
	}
}
