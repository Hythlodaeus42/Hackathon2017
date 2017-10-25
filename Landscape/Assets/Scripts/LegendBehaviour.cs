using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendBehaviour : MonoBehaviour {
    private Transform landscape;
    private Vector3 offset;
    private Vector3 screenPoint;

    // Use this for initialization
    void Start () {
        landscape = this.transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Debug.Log("mouse down on legend");
        screenPoint = Camera.main.WorldToScreenPoint(landscape.position);
        offset = landscape.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        landscape.transform.position = cursorPosition;

        //if (selected && Time.time > timeMouseDown + 0.05f && dragStart)
        //{
        //    OnSelect();
        //    dragStart = false;
        //}
    }
}
