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

    private bool selected;
    private float nextClick = 0;

    void Start()
    {
        //Debug.Log("NodeBehaviour.Start(): " + this.name);
        //       mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        //       systemText = GameObject.Find("SystemText").GetComponent<Text>();
        //       mainText = GameObject.Find("MainText").GetComponent<Text>();
        //localCanvas = this.GetComponent<Canvas>();
        //halo = (Behaviour)this.GetComponent("Halo");

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


        /*
        // Display info
        if (selected)
        {

        }





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
        */
    }

    void OnDeSelect()
    {
        selected = false;

        Light light = this.GetComponent<Light>();
        light.enabled = selected;
    }
}