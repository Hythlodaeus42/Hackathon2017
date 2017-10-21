using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineBehaviour : MonoBehaviour {

    void OnSelect()
    {
        //Debug.Log("TimelineBehaviour.OnSelect(): " + this.name);

        string year = this.name.Split(" "[0])[1];
        //Debug.Log(year);

        GameObject matrix = GameObject.Find("Matrix" + year);

        if (matrix != null)
        {
            Debug.Log(matrix.name);
        }
        else
        {
            Debug.Log("nothing found");
        }
    }
}
