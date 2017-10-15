using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public bool rotateGraph = true;

	// Update is called once per frame
	void Update () {
        if (rotateGraph)
        {
            transform.Rotate(new Vector3(0, 2, 0) * Time.deltaTime);
        }
        
	}

    void RotateGraph()
    {
        rotateGraph = true;
    }

    void StopRotateGraph()
    {
        rotateGraph = false;
    }
}
