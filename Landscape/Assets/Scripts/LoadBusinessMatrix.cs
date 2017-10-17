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
        DrawAxis();
        DrawApps();

    }


    void DrawApps()
    {
        TextAsset textMatrix = Resources.Load("BusinessArchitectureMatrix") as TextAsset;

        string[] matrixRows = textMatrix.text.Split("\n"[0]);

        foreach (string row in matrixRows)
        {

            if (row != "")
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


    void DrawAxis()
    {
        TextAsset textBusinessFunctionGroup = Resources.Load("BusinessFunctionGroup") as TextAsset;
        TextAsset textBusinessFunction = Resources.Load("BusinessFunction") as TextAsset;
        TextAsset textAssetClass = Resources.Load("AssetClass") as TextAsset;

        string[] businessFunctionRows = textBusinessFunction.text.Split("\n"[0]);
        string[] assetClassRows = textAssetClass.text.Split("\n"[0]);

        int maxBlockY = 0;

        //draw row headers
        foreach (string row in businessFunctionRows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split(","[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                float x = 0;
                float y = float.Parse(rowAttributes[0]);
                float z = 0;
                string blockName = rowAttributes[1].Trim();

                //track height of structure
                if (y > maxBlockY)
                {
                    maxBlockY = (int)y;
                }

                Instantiate(prefabAxisBlock, new Vector3(x, y, z), Quaternion.identity, matrixTransform);

                Transform blockInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
                blockInstance.transform.localPosition = new Vector3(x, y, z);
                blockInstance.name = blockName;
            }
        }

        //draw column headers
        foreach (string row in assetClassRows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split(","[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                float x = float.Parse(rowAttributes[0]);
                float y = maxBlockY + 1;
                float z = 0;
                string blockName = rowAttributes[1].Trim();

                Instantiate(prefabAxisBlock, new Vector3(x, y, z), Quaternion.identity, matrixTransform);

                Transform blockInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
                blockInstance.transform.localPosition = new Vector3(x, y, z);
                blockInstance.name = blockName;
            }
        }
    }
}
