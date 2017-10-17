using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using System.Xml;
//using System.Linq;
//using System.Xml.Linq;


public class LoadBusinessMatrix : MonoBehaviour {
    public Transform prefabAppBlock;
    public Transform prefabAxisBlock;

    private Transform matrixTransform;
    
    //private float xscale = 2f;
    //private float yscale = 5f;
    //private float zscale = 2f;
    //private float yoffset = -10f;
    //private float edgexscale = 0.05f;
    //private float edgeyscale = 0.05f;
    //private float layerscale = 3f;


    // Use this for initialization
    void Start () {
        // load XML document




        matrixTransform = GameObject.Find("BusinessArchitectureMatrix").transform;

        // draw model
        //DrawAxis();
        DrawApps();

    }

    /*

    void DrawAxis()
{
    TextAsset textBusinessFunctionGroup = Resources.Load("BusinessFunctionGroup") as TextAsset;
    TextAsset textBusinessFunction = Resources.Load("BusinessFunction") as TextAsset;
    TextAsset textAssetClass = Resources.Load("AssetClass") as TextAsset;

    string[] businessFunctionGroupRows = textBusinessFunctionGroup.Text.Split("\n"[0]);

    foreach (string row in nodeRbusinessFunctionGroupRowsows)
    {
        Instantiate(prefabNodeApplication, new Vector3(x, y, z), Quaternion.identity, graphTransform);
    }


        float x = 0;
    float y = 0;
    float z = 0;
    string nodeName = "dummy";
    string nodeType = "Default";
    string nodeLongName = "dummy";

    foreach (XElement node in xmlNodes)
    {
        x = float.Parse(node.Descendants().Where(a => a.Attribute("key").Value == "v_X").Select(a => a.Value).FirstOrDefault()) * xscale;
        y = float.Parse(node.Descendants().Where(a => a.Attribute("key").Value == "v_LayerOrdinal").Select(a => a.Value).FirstOrDefault()) * yscale + yoffset;
        z = float.Parse(node.Descendants().Where(a => a.Attribute("key").Value == "v_Z").Select(a => a.Value).FirstOrDefault()) * zscale;
        nodeName = node.Attribute("id").Value;
        nodeType = node.Descendants().Where(a => a.Attribute("key").Value == "v_Layer").Select(a => a.Value).FirstOrDefault();
        nodeLongName = node.Descendants().Where(a => a.Attribute("key").Value == "v_LongName").Select(a => a.Value).FirstOrDefault();

        switch (nodeType)
        {
            case "Application":
                Instantiate(prefabNodeApplication, new Vector3(x, y, z), Quaternion.identity, graphTransform);
                break;
            case "Service":
                Instantiate(prefabNodeService, new Vector3(x, y, z), Quaternion.identity, graphTransform);
                break;
            case "Channel":
                Instantiate(prefabNodeChannel, new Vector3(x, y, z), Quaternion.identity, graphTransform);
                break;
            default:
                Instantiate(prefabNodeDefault, new Vector3(x, y, z), Quaternion.identity, graphTransform);
                break;
        }

        Transform nodeInstance = graphTransform.GetChild(graphTransform.childCount - 1);
        nodeInstance.localPosition = new Vector3(x, y, z);
        nodeInstance.name = nodeName;

        NodeProperties nodeProperties = nodeInstance.gameObject.GetComponent<NodeProperties>();
        nodeProperties.NodeID = node.Attribute("id").Value;
        nodeProperties.LongName = nodeLongName;
        nodeProperties.LayerOrdinal = node.Descendants().Where(a => a.Attribute("key").Value == "v_LayerOrdinal").Select(a => a.Value).FirstOrDefault();
        nodeProperties.Layer = node.Descendants().Where(a => a.Attribute("key").Value == "v_Layer").Select(a => a.Value).FirstOrDefault();
        nodeProperties.Critically = node.Descendants().Where(a => a.Attribute("key").Value == "v_Critically").Select(a => a.Value).FirstOrDefault();
        nodeProperties.Desirability = node.Descendants().Where(a => a.Attribute("key").Value == "v_Desirability").Select(a => a.Value).FirstOrDefault();
        nodeProperties.DisplayWeight = node.Descendants().Where(a => a.Attribute("key").Value == "v_DisplayWeight").Select(a => a.Value).FirstOrDefault();

        float displayWeight = float.Parse(nodeProperties.DisplayWeight);

        nodeInstance.localScale = new Vector3(displayWeight, displayWeight, displayWeight);

        Text txt = nodeInstance.GetComponentInChildren<Text>();
        txt.text = nodeLongName;

        nodeInstance.GetComponentInChildren<Canvas>().enabled = false;

        nodecount++;
    }
}
    */

    void DrawApps()
    {
        TextAsset textMatrix = Resources.Load("BusinessArchitectureMatrix") as TextAsset;

        string[] matrixRows = textMatrix.text.Split("\n"[0]);

        foreach (string row in matrixRows)
        {
            string[] rowAttributes = row.Split(","[0]);
            //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
            //Debug.Log(nodecount.ToString());

            float x = float.Parse(rowAttributes[6]);
            float y = float.Parse(rowAttributes[5]);
            float z = 0;
            string appName = rowAttributes[3].Trim();

            Instantiate(prefabAppBlock, new Vector3(x, y, z), Quaternion.identity, matrixTransform);

            Transform appInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
            appInstance.transform.localPosition = new Vector3(x, y, z);
            appInstance.name = appName;

        }
        
    }

}
