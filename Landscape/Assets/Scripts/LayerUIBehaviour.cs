using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerUIBehaviour : MonoBehaviour {
    private Transform localLayerContainer;
    //private Transform parentLayerContainer;

    // Use this for initialization
    void Start () {
        //parentLayerContainer = GameObject.Find("Landscape").transform;

    }

    public void SetUp(Transform layerContainer)
    {
        localLayerContainer = layerContainer;

        //get year-layer container


        // add handlers to buttons
        foreach (Transform child in this.transform)
        {
            Button btn = child.GetComponent<Button>();
            //Debug.Log("LayerUIBehaviour.SetUp(): " + btn.name);

            btn.onClick.AddListener(ToggleLayerVisibility);
        }
    }
	
	// Update is called once per frame
	public void ToggleLayerVisibility () {
        //Debug.Log("LayerUIBehaviour.SetUp(): " + localLayerContainer.name);
        localLayerContainer.GetComponent<ContainerBehaviour>().toggleVisibility();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Layer" + localLayerContainer.GetComponent<ContainerProperties>().Ordinal.ToString()))
        {
            obj.transform.parent.gameObject.SetActive(!obj.transform.parent.gameObject.activeSelf);
            
        }
    }

    void Destroy()
    {
        foreach (Transform child in this.transform)
        {
            Button btn = child.GetComponent<Button>();

            btn.onClick.RemoveAllListeners();
        }
    }
}
