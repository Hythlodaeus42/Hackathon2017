using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : MonoBehaviour {
    public GameObject selectedGameObject;

	// Use this for initialization
	void Start () {
        selectedGameObject = null;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool toggleVisibility()
    {
        //Debug.Log("toggleVisibility(): " + this.name);
        this.transform.gameObject.SetActive(!this.transform.gameObject.activeSelf);

        return this.transform.gameObject.activeSelf;
    }

    public void HideNodes()
    {
        //Debug.Log("HideNodes()");
        if (selectedGameObject != null)
        {
            foreach (NodeBehaviour nb in this.GetComponentsInChildren<NodeBehaviour>(true))
            {
                if (!nb.IsSelected & !nb.IsNeighbour)
                {
                    nb.gameObject.SetActive(false);
                }
            }

            foreach (EdgeProperties ep in this.GetComponentsInChildren<EdgeProperties>(true))
            {
                if (!ep.IsConnected)
                {
                    ep.gameObject.SetActive(false);
                }
            }

        }
    }

    public void ShowNodes()
    {
        //Debug.Log("ShowNodes()");
        if (selectedGameObject != null)
        {
            foreach (NodeBehaviour nb in this.GetComponentsInChildren<NodeBehaviour>(true))
            {
                nb.gameObject.SetActive(true);
            }

            foreach (EdgeProperties ep in this.GetComponentsInChildren<EdgeProperties>(true))
            {
                ep.gameObject.SetActive(true);
            }
        }

    }
}
