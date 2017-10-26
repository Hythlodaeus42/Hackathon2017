using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisBlockBehaviour : MonoBehaviour {
    private Transform matrix;
    private Vector3 offset;
    private Vector3 screenPoint;

    // Use this for initialization
    void Start () {
        matrix = this.transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
		
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
