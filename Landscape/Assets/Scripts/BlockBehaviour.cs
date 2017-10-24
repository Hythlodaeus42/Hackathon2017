using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBehaviour : MonoBehaviour
{
    private bool selected = false;
    private Transform matrix;
    private Vector3 offset;
    private Vector3 screenPoint;

    private Canvas infoCanvas;

    // Use this for initialization
    void Start()
    {
        matrix = this.transform.parent;
        infoCanvas = GameObject.Find("InfoCanvas").GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        Debug.Log("BlockBehaviour.OnSelect():" + this.name);

        // year for landscape model, indicating title block
        if (this.name == "2018" || this.name == "2022")
        {
            GameObject landscapeContainer = GameObject.Find("Landscape");
            foreach (Transform landscape in landscapeContainer.GetComponentInChildren<Transform>(true))
            {
                if (landscape.name == "Landscape" + this.name)
                {
                    landscape.GetComponent<ContainerBehaviour>().toggleVisibility();
                    break;
                }
            }
        } else
        {
            // set selected
            selected = !selected;

            if (selected)
            {
                infoCanvas.GetComponent<InfoCanvasBehaviour>().Show(BuildInfoString());
            } else
            {
                infoCanvas.GetComponent<InfoCanvasBehaviour>().Hide();
            }
            


            Color clr1 = gameObject.transform.Find("Canvas/Panel1").GetComponent<Image>().color;
            Color clr2 = gameObject.transform.Find("Canvas/Panel2").GetComponent<Image>().color;

            //Debug.Log(clr1.ToString());

            if (selected)
            {
                gameObject.transform.Find("Canvas/Panel1").GetComponent<Image>().color = new Color(clr1.r, clr1.g, clr1.b, 0.4f);
                gameObject.transform.Find("Canvas/Panel2").GetComponent<Image>().color = new Color(clr1.r, clr1.g, clr1.b, 0.4f);
            }
            else
            {
                gameObject.transform.Find("Canvas/Panel1").GetComponent<Image>().color = new Color(clr1.r, clr1.g, clr1.b, 0f);
                gameObject.transform.Find("Canvas/Panel2").GetComponent<Image>().color = new Color(clr1.r, clr1.g, clr1.b, 0f);
            }

        }
    }

    string BuildInfoString()
    {
        BlockProperties bp = this.GetComponent<BlockProperties>();
        string info = "<b>" + this.name + "</b>\n\n";
        info += "Application name: " + bp.ApplicationName + "\n";
        info += "Business Group: " + bp.BusinessGroup + "\n";
        info += "Business Function: " + bp.BusinessFunction + "\n";
        info += "Desirability: " + bp.Desirability;

        return info;
    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(matrix.position);
        offset = matrix.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        matrix.transform.position = cursorPosition;

        //if (selected && Time.time > timeMouseDown + 0.05f && dragStart)
        //{
        //    OnSelect();
        //    dragStart = false;
        //}
    }

    
}
