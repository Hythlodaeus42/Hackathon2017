﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;


public class LoadGraph : MonoBehaviour {
    public Transform prefabNodeDefault;
    public Transform prefabNodeApplication;
    public Transform prefabNodeChannel;
    public Transform prefabNodeService;
    public Transform prefabEdge;
    public Transform prefabLayer;

    private Transform graphTransform;
    //private float lastLayer = 999999f;

    private XmlDocument xmlGraph;
    private XmlNodeList xmlNodes;
    private XmlNodeList xmlEdges;

    private float yoffset = -10f;
    private float yscale = 10f;
    private float edgexscale = 0.05f;
    private float edgeyscale = 0.05f;
    private float layerscale = 4f;


    // Use this for initialization
    void Start () {
        // load XML document
        TextAsset textGraph = Resources.Load("landscape") as TextAsset;
        xmlGraph = new XmlDocument();
        xmlGraph.LoadXml(textGraph.text);

        // get nodes
        xmlNodes = xmlGraph.SelectNodes("/graphml/graph/node");
        xmlEdges = xmlGraph.SelectNodes("/graphml/graph/edge");

        graphTransform = GameObject.Find("Landscape").transform;

        // draw model
        DrawNodes();
        DrawEdges();
        DrawLayers();

        //Instantiate(node, new Vector3(0.134f, 2.564f, 0), Quaternion.identity);
    }

    void DrawNodes()
    {

        int nodecount = 0;

        float x = 0;
        float y = 0;
        float z = 0;
        string nodeName = "dummy";
        string nodeType = "Default";
        string nodeLongName = "dummy";

        foreach (XmlNode node in xmlNodes)
        {
            x = float.Parse(node.SelectSingleNode("data[@key='v_X']").InnerText);
            y = float.Parse(node.SelectSingleNode("data[@key='v_LayerOrdinal']").InnerText) * yscale + yoffset;
            z = float.Parse(node.SelectSingleNode("data[@key='v_Z']").InnerText);
            nodeName = node.Attributes["id"].Value;
            nodeType = node.SelectSingleNode("data[@key='v_Layer']").InnerText;
            nodeLongName = node.SelectSingleNode("data[@key='v_LongName']").InnerText;

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
            nodeInstance.name = nodeName;

            NodeProperties nodeProperties = nodeInstance.gameObject.GetComponent<NodeProperties>();
            nodeProperties.NodeID = node.Attributes["id"].Value;
            nodeProperties.LongName = nodeLongName;
            nodeProperties.LayerOrdinal = node.SelectSingleNode("data[@key='v_LayerOrdinal']").InnerText;
            nodeProperties.Layer = node.SelectSingleNode("data[@key='v_Layer']").InnerText;
            nodeProperties.Critically = node.SelectSingleNode("data[@key='v_Critically']").InnerText;
            nodeProperties.Desirability = node.SelectSingleNode("data[@key='v_Desirability']").InnerText;
            nodeProperties.DisplayWeight = node.SelectSingleNode("data[@key='v_DisplayWeight']").InnerText;

            float displayWeight = float.Parse(nodeProperties.DisplayWeight);

            nodeInstance.localScale = new Vector3(displayWeight, displayWeight, displayWeight);

            Text txt = nodeInstance.GetComponentInChildren<Text>();
            txt.text = nodeLongName;

            nodeInstance.GetComponentInChildren<Canvas>().enabled = true;

            nodecount++;
        }
    }
    void DrawEdges()
    {

        foreach (XmlNode edge in xmlEdges)
        {
            //Debug.Log(edge.InnerXml);
            
            GameObject startNode = GameObject.Find(edge.Attributes["source"].Value);
            GameObject endNode = GameObject.Find(edge.Attributes["target"].Value);

            Vector3 centerPosition = (startNode.transform.position + endNode.transform.position) / 2f;
            float dist = Vector3.Distance(startNode.transform.position, endNode.transform.position);


            //Transform edgeInstance = Instantiate(prefabEdge, startNode.transform.position, Quaternion.identity, graphTransform);
            Transform edgeInstance = Instantiate(prefabEdge, centerPosition, Quaternion.identity, graphTransform);
            edgeInstance.LookAt(endNode.transform);
            edgeInstance.transform.localScale = new Vector3(edgexscale, edgeyscale, dist);

            //var edgeData = edgeInstance.transform.GetComponent<EdgeData>();


            //LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            //lineRenderer.widthMultiplier = 0.1f;
            ////lineRenderer.positionCount = lengthOfLineRenderer;
            //lineRenderer.SetPosition(0, new Vector3(startNode.transform.position.x, startNode.transform.position.y, startNode.transform.position.z));
            //lineRenderer.SetPosition(1, new Vector3(endNode.transform.position.x, endNode.transform.position.y, endNode.transform.position.z));

            ////A simple 2 color gradient with a fixed alpha of 1.0f.
            //float alpha = 1.0f;
            //Gradient gradient = new Gradient();
            //gradient.SetKeys(
            //    new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            //    );
            //lineRenderer.colorGradient = gradient;

        }
    }

    void DrawLayers()
    {
        TextAsset textLayer = Resources.Load("layers") as TextAsset;

        string[] rows = textLayer.text.Split("\n"[0]);

        foreach (string row in rows)
        {
            string[] rowAttributes = row.Split(","[0]);

            float y = float.Parse(rowAttributes[0]) * yscale + yoffset;
            float layerx = float.Parse(rowAttributes[2]) * layerscale;
            float layerz = float.Parse(rowAttributes[3]) * layerscale;


            //Quaternion target = Quaternion.Euler(90, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
            //Quaternion target = Quaternion.Euler(90, 0, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);

            //Instantiate(prefabLayer, new Vector3(0, y, 0), transform.rotation, graphTransform);
            Instantiate(prefabLayer, new Vector3(0, y, 0), Quaternion.identity, graphTransform);
            

            Transform layerInstance = graphTransform.GetChild(graphTransform.childCount - 1);
            RectTransform layerRect = layerInstance.gameObject.GetComponent<RectTransform>();
            layerInstance.name = rowAttributes[1];
            layerInstance.Rotate(90, 0, 0);
            layerRect.sizeDelta = new Vector2(layerx, layerz);

        }

    }

}
