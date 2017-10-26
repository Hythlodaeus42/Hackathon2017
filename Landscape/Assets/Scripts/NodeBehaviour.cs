using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBehaviour : MonoBehaviour
{
    public bool IsSelected;
    public bool IsNeighbour;

    //public Text displayInfo;
    //private Canvas mainCanvas;
    //private Canvas localCanvas;
    //private Text systemText;
    //private Text mainText;
    private Canvas infoCanvas;
    private Transform yearContainer;


    void Start()
    {
        //Debug.Log("NodeBehaviour.Start(): " + this.name);
        infoCanvas = GameObject.Find("InfoCanvas").GetComponentInChildren<Canvas>();
        IsSelected = false;
        IsNeighbour = false;

        yearContainer = this.transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        Debug.Log("NodeBehaviour.OnSelect() " + this.name);

        // set IsSelected
        IsSelected = !IsSelected;

        FlagNeighbours();

        Light light = this.GetComponent<Light>();
        light.enabled = IsSelected;

        if (IsSelected)
        {
            //Debug.Log(BuildInfoString());
            infoCanvas.GetComponent<InfoCanvasBehaviour>().Show(BuildInfoString());

        } else
        {
            //Debug.Log(BuildInfoString());
            infoCanvas.GetComponent<InfoCanvasBehaviour>().Hide();
        }
    }

    string BuildInfoString()
    {
        NodeProperties np = this.GetComponent<NodeProperties>();
        string info = "<b>" + np.LongName + "</b>\n\n";
        info += "Layer: " + np.Layer + "\n";
        info += "Critically: " + np.Critically + "\n";
        info += "Desirability: " + np.Desirability + "\n";
        info += "Business Area: " + np.BusinessArea + "\n";
        info += "Business Function: " + np.BusinessFunction + "\n";
        info += "IsMarkets: " + np.IsMarkets.ToString();

        return info;
    }

    void FlagNeighbours()
    {
        // find all edges with source or target to this node
        foreach (EdgeProperties ep  in yearContainer.GetComponentsInChildren<EdgeProperties>())
        {
            // get nodes at other end of edges
            if (ep.toNode == this.name)
            {
                // set neighbour flag
                yearContainer.Find("Layer" + ep.fromLayerOrdinal + "/" + ep.fromNode).GetComponent<NodeBehaviour>().IsNeighbour = IsSelected;
                ep.IsConnected = IsSelected;
            }

            if (ep.fromNode == this.name)
            {
                // set neighbour flag
                yearContainer.Find("Layer" + ep.toLayerOrdinal + "/" + ep.toNode).GetComponent<NodeBehaviour>().IsNeighbour = IsSelected;
                ep.IsConnected = IsSelected;
            }
        }

    }


    
}