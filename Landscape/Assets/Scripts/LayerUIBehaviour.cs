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
        /*
        foreach (Transform child in this.transform)
        {
            Button btn = child.GetComponent<Button>();
            //Debug.Log("LayerUIBehaviour.SetUp(): " + btn.name);

            btn.onClick.AddListener(ToggleLayerVisibility);
        }
        */
    }

    void OnSelect()
    {
        ToggleLayerVisibility();
    }
    // Update is called once per frame
    public void ToggleLayerVisibility () {
        //Debug.Log("LayerUIBehaviour.SetUp(): " + localLayerContainer.name);
        bool[] visibility = new bool[5];

        localLayerContainer.GetComponent<ContainerBehaviour>().toggleVisibility();

        //show/hide edges
        for (int i = 1; i <= 4; i++)
        {
            //Debug.Log(i.ToString());
            visibility[i] = localLayerContainer.parent.Find("Layer" + i.ToString()).gameObject.activeSelf;

        }

        int layerOrdinal = localLayerContainer.GetComponent<ContainerProperties>().Ordinal;
        Debug.Log("toggle layer " + layerOrdinal.ToString());

        Debug.Log(localLayerContainer.parent.Find("EdgeContainer").childCount.ToString());

        foreach (Transform trn in localLayerContainer.parent.Find("EdgeContainer"))
        {
            trn.gameObject.SetActive(visibility[trn.GetComponent<EdgeProperties>().fromLayerOrdinal] && visibility[trn.GetComponent<EdgeProperties>().toLayerOrdinal]);
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
