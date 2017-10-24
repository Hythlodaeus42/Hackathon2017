using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvasBehaviour : MonoBehaviour {

    private Canvas infoCanvas;

    // Use this for initialization
    void Start () {
        infoCanvas = GameObject.Find("InfoCanvas").GetComponentInChildren<Canvas>();
        infoCanvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show(string info)
    {
        infoCanvas.GetComponentInChildren<Text>().text = info;
        infoCanvas.enabled = true;
    }

    public void Hide()
    {
        infoCanvas.enabled = false;

        
    }

}
