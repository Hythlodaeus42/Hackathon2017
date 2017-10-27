using UnityEngine;
using UnityEngine.UI;

public class TapToPlaceParent : MonoBehaviour
{
    bool placing = false;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            //Debug.Log("placing");
            SpatialMapping.Instance.DrawVisualMeshes = true;
            //this.transform.Find("Canvas/Panel/TitleText").GetComponent<Text>().text = "tapped";
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            //Debug.Log("not placing");
            SpatialMapping.Instance.DrawVisualMeshes = false;
            //this.transform.Find("Canvas/Panel/TitleText").GetComponent<Text>().text = "untapped";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing)
        {
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            //this.transform.Find("Canvas/Panel/TitleText").GetComponent<Text>().text = "placing";

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                100.0f, SpatialMapping.PhysicsRaycastMask))
            {
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                this.transform.parent.position = hitInfo.point;
                //this.transform.Find("Canvas/Panel/TitleText").GetComponent<Text>().text = "hit";

                // Rotate this object's parent object to face the user.
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;
            }
        }
    }
}
