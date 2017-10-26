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
    public Transform prefabTitleBlock;
    public Transform prefabContainer;
    public bool startVisible;

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
        SetStartActiveStatus();
    }

    void SetStartActiveStatus()
    {
        if (!startVisible)
        {
            foreach (Transform matrix in matrixParentTransform.GetComponentInChildren<Transform>(true))
            {
                matrix.GetComponent<ContainerBehaviour>().toggleVisibility();
            }
        }
    }

    void BuildMatrices()
    {
        //get years
        TextAsset textMatrix = Resources.Load("MatrixYears") as TextAsset;
        string[] matrixRows = textMatrix.text.Split("\n"[0]);

        //loop through years and build axis
        foreach (string row in matrixRows)
        {
            int year = int.Parse(row);

            Instantiate(prefabContainer, new Vector3(0, 0, year - 2018), Quaternion.identity, matrixParentTransform);
            Transform matrixTransform = matrixParentTransform.GetChild(matrixParentTransform.childCount - 1);
            matrixTransform.name = "Matrix" + year.ToString();
            matrixTransform.localPosition = new Vector3(0, 0, year - 2018);

            DrawAxis(matrixTransform, year);
        }

            
        DrawApps();

    }

    void DrawApps()
    {
        TextAsset textMatrix = Resources.Load("BusinessArchitectureMatrix") as TextAsset;
        Transform matrixTransform;

        string[] matrixRows = textMatrix.text.Split("\n"[0]);

        foreach (string row in matrixRows)
        {

            if (row != "")
            {
                string[] rowAttributes = row.Split("|"[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                int year = int.Parse(rowAttributes[2]);
                string appName = rowAttributes[4].Trim();
                string colour = rowAttributes[5].Trim();
                float x = float.Parse(rowAttributes[7]) * (xscale + xpad);
                float appcount = float.Parse(rowAttributes[8]);
                float apprank = float.Parse(rowAttributes[9]);
                float y = (float.Parse(rowAttributes[6]) + 0.5f - (apprank - 0.5f) / appcount) * (yscale + ypad);       // y = ([y number] - 1 + [app rank] / [app count]) * (scale + pad)
                float z = 0;
                bool leftMatch = (rowAttributes[10] == "TRUE");
                int contiguous = int.Parse(rowAttributes[11]);

                matrixTransform = matrixParentTransform.Find("Matrix" + year.ToString());

                if (!leftMatch || appcount > 1)
                {
                    Transform blockInstance = AddBlock(x, y, z, contiguous, Quaternion.identity, appName, prefabAppBlock, matrixTransform);
                    blockInstance.localScale = new Vector3(blockInstance.localScale.x * contiguous, blockInstance.localScale.y / appcount, blockInstance.localScale.z);

                    // fix text panel size
                    Transform objPanal1 = blockInstance.Find("Canvas/Panel1/Text1");
                    objPanal1.localScale = new Vector3(objPanal1.localScale.x / contiguous, objPanal1.localScale.y * appcount, objPanal1.localScale.z);
                    Transform objPanal2 = blockInstance.Find("Canvas/Panel2/Text2");
                    objPanal2.localScale = new Vector3(objPanal2.localScale.x / contiguous, objPanal2.localScale.y * appcount, objPanal2.localScale.z);

                    switch (colour)
                    {
                        case "Amber":
                            blockInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                            objPanal1.GetComponent<Text>().color = Color.black;
                            objPanal2.GetComponent<Text>().color = Color.black;
                            break;
                        case "White":
                            blockInstance.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
                            objPanal1.GetComponent<Text>().color = Color.black;
                            objPanal2.GetComponent<Text>().color = Color.black;
                            break;
                    }                    

                    // add property
                    BlockProperties blockProperties = blockInstance.gameObject.GetComponent<BlockProperties>();
                    blockProperties.ApplicationName = appName;
                    blockProperties.BusinessGroup = rowAttributes[3].Trim();
                    blockProperties.BusinessFunction = rowAttributes[0].Trim();
                    blockProperties.Desirability = colour;
                }

            }
        }
        
    }


    void DrawAxis(Transform matrixTransform, int year)
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

                AddBlock(x, y, z, 1, Quaternion.identity, blockName, prefabAxisBlock, matrixTransform);

            }
        }

        //draw title block
        AddBlock(-(xscale + xpad), maxBlockY + yscale + ypad, 0, 1, Quaternion.identity, year.ToString(), prefabTitleBlock, matrixTransform);


        //draw business function groups
        foreach (string row in businessFunctionGroupRows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split("|"[0]);
                //Debug.Log(nodeRow.ToString().TrimStart().Substring(0, 2));
                //Debug.Log(nodecount.ToString());

                float x = -yscale - xscale - xpad - (yscale + ypad);
                float y = (float.Parse(rowAttributes[1]) + float.Parse(rowAttributes[2])) / 2f * (yscale + ypad);
                float z = 0;
                float len = (float.Parse(rowAttributes[2]) - float.Parse(rowAttributes[1]) + 1) * (yscale + ypad);
                string blockName = rowAttributes[0].Trim();

                Transform blockInstance = AddBlock(x, y, z, 1, Quaternion.Euler(0, 0, 90), blockName, prefabAxisBlock, matrixTransform);
                float orgX = blockInstance.localScale.x;
                blockInstance.localScale = new Vector3(len, blockInstance.localScale.y, blockInstance.localScale.z);

                // fix text panel size
                Transform objPanal1 = blockInstance.Find("Canvas/Panel1/Text1");
                Transform objPanal2 = blockInstance.Find("Canvas/Panel2/Text2");
                if (len / (yscale + ypad) < 3f)
                {
                    objPanal1.GetComponent<Text>().text = objPanal1.GetComponent<Text>().text.Replace(' ', '\n');
                    objPanal2.GetComponent<Text>().text = objPanal2.GetComponent<Text>().text.Replace(' ', '\n');
                }
                else
                {
                    objPanal1.localScale = new Vector3(objPanal1.localScale.x / (len / orgX), objPanal1.localScale.y, objPanal1.localScale.z);
                    objPanal2.localScale = new Vector3(objPanal2.localScale.x / (len / orgX), objPanal2.localScale.y, objPanal2.localScale.z);
                }
                // bold
                objPanal1.GetComponent<Text>().fontStyle = FontStyle.Bold;
                objPanal2.GetComponent<Text>().fontStyle = FontStyle.Bold;

            }
        }


        //Debug.Log(maxBlockY.ToString());

        //draw column headers
        foreach (string row in assetClassRows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split("|"[0]);
                //Debug.Log(row);
                //Debug.Log(nodecount.ToString());

                float x = float.Parse(rowAttributes[1]) * (xscale + xpad);
                float y = maxBlockY + yscale + ypad;
                float z = 0;
                string blockName = rowAttributes[0].Trim();

                AddBlock(x, y, z, 1, Quaternion.identity, blockName, prefabAxisBlock, matrixTransform);
                
            }
        }
    }

    Transform AddBlock(float x, float y, float z, int contiguous, Quaternion rotation, string blockName, Transform prefab, Transform matrixTransform)
    {
        Instantiate(prefab, new Vector3(x, y, z), rotation, matrixTransform);

        Transform blockInstance = matrixTransform.GetChild(matrixTransform.childCount - 1);
        blockInstance.transform.localPosition = new Vector3(x + (contiguous - 1) * (xscale + xpad) / 2, y, z);
        blockInstance.name = blockName;

        blockInstance.Find("Canvas/Panel1").GetComponentInChildren<Text>().text = blockName;
        blockInstance.Find("Canvas/Panel2").GetComponentInChildren<Text>().text = blockName;

        // bold
        blockInstance.Find("Canvas/Panel1").GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
        blockInstance.Find("Canvas/Panel2").GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;

        return blockInstance;
    }
}
