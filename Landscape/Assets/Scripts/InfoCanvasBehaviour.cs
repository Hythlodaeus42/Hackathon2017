using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvasBehaviour : MonoBehaviour {

    private Canvas infoCanvas;
    private bool visible;

    // Use this for initialization
    void Start () {
        infoCanvas = GameObject.Find("InfoCanvas").GetComponentInChildren<Canvas>();
        visible = false;
        infoCanvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleVisibility(string info)
    {
        infoCanvas.GetComponentInChildren<Text>().text = info;        
        ToggleVisibility();
    }

    public void ToggleVisibility()
    {
        visible = !visible;
        infoCanvas.enabled = visible;

        
    }

}
