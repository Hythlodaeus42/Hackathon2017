using System.Collections;
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

                if (hitObject != null)
                {
                    Debug.Log(hitObject.name);
                    hitObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);


                }

                if (LastClickedObject != hitObject && LastClickedObject != null)
                {
                    LastClickedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
                }

                LastClickedObject = hitObject;

            }
            //else
            //{
            //    hitObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
            //}

            //nextClick = Time.time + 0.2f;
            //click = false;

        }
    }

		
}
