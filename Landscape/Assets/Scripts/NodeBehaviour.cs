using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBehaviour : MonoBehaviour
{
    //public Text displayInfo;
    //private Canvas mainCanvas;
    //private Canvas localCanvas;
    //private Text systemText;
    //private Text mainText;
    private Canvas infoCanvas;

    private bool selected;

    void Start()
    {
        //Debug.Log("NodeBehaviour.Start(): " + this.name);
        infoCanvas = GameObject.Find("InfoCanvas").GetComponentInChildren<Canvas>();
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        //Debug.Log("NodeBehaviour.OnSelect() called");

        // set selected
        selected = !selected;

        Light light = this.GetComponent<Light>();
        light.enabled = selected;
        Debug.Log(BuildInfoString());
        infoCanvas.GetComponent<InfoCanvasBehaviour>().ToggleVisibility(BuildInfoString());       
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
    
}