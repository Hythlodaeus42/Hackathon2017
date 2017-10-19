﻿using System.Collections;
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

    private Transform matrixParentTransform;

    private float xscale;
    private float yscale;
    private float zscale;

    private float xpad = 0.02f;
    private float ypad = 0.02f;
    private float zpad = 0.02f;
    //private float yoffset = -10f;
    //private float edgexscale = 0.05f;
    //private float edgeyscale = 0.05f;
    //private float layerscale = 3f;


    // Use this for initialization
    void Start() {
        // load XML document

        xscale = prefabAppBlock.localScale.x;
        yscale = prefabAppBlock.localScale.y;
        zscale = prefabAppBlock.localScale.z;


        matrixParentTransform = GameObject.Find("BusinessArchitectureMatrix").transform;

        // draw matrices
        BuildMatrices();

    }


    void BuildMatrices()
    {
        GameObject container = new GameObject();

        //get years
        TextAsset textMatrix = Resources.Load("MatrixYears") as TextAsset;
        string[] matrixRows = textMatrix.text.Split("\n"[0]);

        //loop through years and build axis
        foreach (string row in matrixRows)
        {
            int year = int.Parse(row);

            Instantiate(container, new Vector3(0, 0, year - 2018), Quaternion.identity, matrixParentTransform);
            Transform matrixTransform = matrixParentTransform.GetChild(matrixParentTransform.childCount - 1);
            matrixTransform.name = "Matrix" + year.ToString();

            DrawAxis(matrixTransform);
        }

            
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
                string[] rowAttributes = row.Split("|"[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                float x = float.Parse(rowAttributes[6]) * (xscale + xpad);
                float appcount = float.Parse(rowAttributes[7]);
                float apprank = float.Parse(rowAttributes[8]); 
                float y = (float.Parse(rowAttributes[5]) + 0.5f - (apprank - 0.5f) / appcount) * (yscale + ypad);       // y = ([y number] - 1 + [app rank] / [app count]) * (scale + pad)
                float z = 0;
                int year = int.Parse(rowAttributes[2]);
                string appName = rowAttributes[4].Trim();

                Transform matrixTransform = matrixParentTransform.Find("Matrix" + year.ToString());

                Instantiate(prefabAppBlock, new Vector3(x, y, z), Quaternion.identity, matrixTransform);
                
                Transform blockInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
                blockInstance.localScale = new Vector3(blockInstance.localScale.x, blockInstance.localScale.y / appcount, blockInstance.localScale.z);
                blockInstance.transform.localPosition = new Vector3(x, y, z);
                blockInstance.name = appName;

                blockInstance.Find("Canvas/Panel1").GetComponentInChildren<Text>().text = appName;
                blockInstance.Find("Canvas/Panel2").GetComponentInChildren<Text>().text = appName;

            }
        }
        
    }


    void DrawAxis(Transform matrixTransform)
    {
        TextAsset textBusinessFunctionGroup = Resources.Load("BusinessFunctionGroup") as TextAsset;
        TextAsset textBusinessFunction = Resources.Load("BusinessFunction") as TextAsset;
        TextAsset textAssetClass = Resources.Load("AssetClass") as TextAsset;

        string[] businessFunctionGroupRows = textBusinessFunctionGroup.text.Split("\n"[0]);
        string[] businessFunctionRows = textBusinessFunction.text.Split("\n"[0]);
        string[] assetClassRows = textAssetClass.text.Split("\n"[0]);

        float maxBlockY = 0;

        //draw row headers
        foreach (string row in businessFunctionRows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split("|"[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                float x = -(xscale + xpad);
                float y = float.Parse(rowAttributes[1]) * (yscale + ypad);
                float z = 0;
                string blockName = rowAttributes[0].Trim();

                //track height of structure
                if (y > maxBlockY)
                {
                    maxBlockY = y;
                }

                Instantiate(prefabAxisBlock, new Vector3(x, y, z), Quaternion.identity, matrixTransform);

                Transform blockInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
                blockInstance.transform.localPosition = new Vector3(x, y, z);
                blockInstance.name = blockName;

                blockInstance.Find("Canvas/Panel1").GetComponentInChildren<Text>().text = blockName;
                blockInstance.Find("Canvas/Panel2").GetComponentInChildren<Text>().text = blockName;
            }
        }

        //draw business function groups
        foreach (string row in businessFunctionGroupRows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split("|"[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                float x = -yscale - xscale - xpad;
                float y = (float.Parse(rowAttributes[1]) + float.Parse(rowAttributes[2])) / 2f * (yscale + ypad);
                float z = 0;
                float len = (float.Parse(rowAttributes[2]) - float.Parse(rowAttributes[1]) + 1) * (yscale + ypad);
                string blockName = rowAttributes[0].Trim();

                Instantiate(prefabAxisBlock, new Vector3(x, y, z), Quaternion.Euler(0, 0, 90), matrixTransform);

                Transform blockInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
                blockInstance.localScale = new Vector3(len, blockInstance.localScale.y, blockInstance.localScale.z);
                blockInstance.transform.localPosition = new Vector3(x, y, z);
                blockInstance.name = blockName;

                blockInstance.Find("Canvas/Panel1").GetComponentInChildren<Text>().text = blockName;
                blockInstance.Find("Canvas/Panel2").GetComponentInChildren<Text>().text = blockName;
            }
        }

        //Debug.Log(maxBlockY.ToString());

        //draw column headers
        foreach (string row in assetClassRows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split("|"[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                float x = float.Parse(rowAttributes[1]) * (xscale + xpad);
                float y = maxBlockY + yscale + ypad;
                float z = 0;
                string blockName = rowAttributes[0].Trim();

                Instantiate(prefabAxisBlock, new Vector3(x, y, z), Quaternion.identity, matrixTransform);

                Transform blockInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
                blockInstance.transform.localPosition = new Vector3(x, y, z);
                blockInstance.name = blockName;

                blockInstance.Find("Canvas/Panel1").GetComponentInChildren<Text>().text = blockName;
                blockInstance.Find("Canvas/Panel2").GetComponentInChildren<Text>().text = blockName;
            }
        }
    }
}
