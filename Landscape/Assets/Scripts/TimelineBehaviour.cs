using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineBehaviour : MonoBehaviour {

    void OnSelect()
    {
        string year = this.name.Split(" "[0])[1];

        GameObject matrixParent = GameObject.Find("BusinessArchitectureMatrix");
        foreach (Transform matrix in matrixParent.GetComponentInChildren<Transform>(true))
        {
            if (matrix.name == "Matrix" + year)
            {
                matrix.GetComponent<ContainerBehaviour>().toggleVisibility();
                break;
            }
        }
    }
}
