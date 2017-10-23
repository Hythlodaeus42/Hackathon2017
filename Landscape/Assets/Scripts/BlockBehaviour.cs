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

    // Use this for initialization
    void Start()
    {
        matrix = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        Debug.Log("BlockBehaviour.OnSelect():" + this.name);

        // year for landscape model, indicating title block
        string year = "2018";

        if (this.name == year)
        {
            GameObject landscapeContainer = GameObject.Find("Landscape");
            foreach (Transform landscape in landscapeContainer.GetComponentInChildren<Transform>(true))
            {
                if (landscape.name == "Landscape" + year)
                {
                    landscape.GetComponent<ContainerBehaviour>().toggleVisibility();
                    break;
                }
            }
        } else
        {
            // set selected
            selected = !selected;

            Color clr1 = gameObject.transform.Find("Canvas/Panel1").GetComponent<Image>().color;
            Color clr2 = gameObject.transform.Find("Canvas/Panel2").GetComponent<Image>().color;

            Debug.Log(clr1.ToString());

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

    void OnDeSelect()
    {
        Color clr1 = gameObject.transform.Find("Canvas/Panel1").GetComponent<Image>().color;
        Color clr2 = gameObject.transform.Find("Canvas/Panel2").GetComponent<Image>().color;

        gameObject.transform.Find("Canvas/Panel1").GetComponent<Image>().color = new Color(clr1.r, clr1.g, clr1.b, 0f);
        gameObject.transform.Find("Canvas/Panel2").GetComponent<Image>().color = new Color(clr1.r, clr1.g, clr1.b, 0f);
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
    }

}
