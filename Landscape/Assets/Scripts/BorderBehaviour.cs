using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderBehaviour : MonoBehaviour {
    private bool selected;

    // Use this for initialization
    void Start () {
        selected = false;
    }
	
	// Update is called once per frame
	void OnSelect () {
        Debug.Log("border clicked");
        selected = !selected;

        Canvas canvas = this.GetComponentInParent<Canvas>();
        Image img = canvas.transform.Find("Panel").GetComponent<Image>();

        if (selected)
        {
            //clr = 
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        } else
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0.1f);
        }

    }
}
