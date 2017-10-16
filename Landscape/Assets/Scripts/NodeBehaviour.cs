using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeBehaviour : MonoBehaviour {
    //public Text displayInfo;
    private Canvas mainCanvas;
    private Text systemText;
    private Text mainText;

    private Behaviour halo;

    private bool selected;


	// Use this for initialization
	void Start () {
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        systemText = GameObject.Find("SystemText").GetComponent<Text>();
        mainText = GameObject.Find("MainText").GetComponent<Text>();
        halo = (Behaviour)this.GetComponent("Halo");

        selected = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSelect()
    {
        // set selected
        selected = !selected;
        
        halo.enabled = selected;
        mainCanvas.enabled = selected;

        // Display info
        if (selected)
        {
            //string nodeType = this.name.Split("|"[0])[0];
            systemText.text = this.name.Split("|"[0])[1];

            string data = "<b>Data</b>";
            string OS = "<b>OS</b>";
            string businessfunction = "<b>Business Function</b>";
            string systemsoftware = "<b>System Software</b>";

            foreach (Transform child in this.transform)
            {
                if (child.name.IndexOf(">") > 0)
                {
                    string childNodeName = child.name.Split(">"[0])[1];
                    string childType = childNodeName.Split("|"[0])[0];
                    string childName = childNodeName.Split("|"[0])[1];

                    switch (childType)
                    {
                        case "data":
                            data += "\n" + childName;
                            break;
                        case "os":
                            OS += "\n" + childName;
                            break;
                        case "businessfunction":
                            businessfunction += "\n" + childName;
                            break;
                        case "systemsoftware":
                            systemsoftware += "\n" + childName;
                            break;

                    }

                }

            }

            mainText.text = businessfunction + "\n\n" + OS + "\n\n" + data + "\n\n" + systemsoftware;
            
        } 


    }
}
