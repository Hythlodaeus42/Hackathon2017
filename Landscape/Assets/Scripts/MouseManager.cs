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
            if (LastClickedObject != null)
            {
                Debug.Log("last = " + LastClickedObject.name);
            } else
            {
                Debug.Log("last = none");
            }
            
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

                if (hitObject.GetComponent<NodeBehaviour>() != null || (hitObject.GetComponent<BlockBehaviour>() != null && hitObject.name != "2018"))
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
