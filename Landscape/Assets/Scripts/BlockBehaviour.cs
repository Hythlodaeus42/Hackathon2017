using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBehaviour : MonoBehaviour
{
    private bool selected = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        Debug.Log("BlockBehaviour.OnSelect() called");

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
