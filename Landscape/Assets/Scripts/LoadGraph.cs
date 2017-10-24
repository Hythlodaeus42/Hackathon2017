using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Xml;
using System.Linq;
using System.Xml.Linq;


public class LoadGraph : MonoBehaviour {
    public Transform prefabLandscapeContainer;
    public Transform prefabNodeCone;
    public Transform prefabNodeCube;
    public Transform prefabNodeSphere;
    public Transform prefabNodeSphereHalf;
    public Transform prefabNodePyramid;
    public Transform prefabNodeCylinder;
    public Transform prefabNodeIcosphere;
    public Transform prefabNodeIcosphereSmall;
    public Transform prefabNodeRectangleH;
    public Transform prefabNodeRectangleV;

    public Transform prefabEdgeContainer;
    public Transform prefabEdge;
    public Transform prefabLayerContainer;
    public Transform prefabLayer;
    public Transform prefabLayerUI;
    public float hologramScale;
    public bool startVisible;

    private Transform parentContainer;
    //private Transform graphTransform;

    private XDocument xmlGraph;
    private IEnumerable<XElement> xmlNodes;
    private IEnumerable<XElement> xmlEdges;

    private float xscale = 2f;
    private float yscale = 10f;
    private float zscale = 2f;
    private float yoffset = -10f;
    private float edgexscale = 0.05f;
    private float edgeyscale = 0.05f;
    private float layerscale = 3f;
    private float EODdelay = 5f;
    private float yBentMean = 0.75f;
    private float yBentDrop = 2f;
    private float objectscale = 2f;
    

    // Use this for initialization
    void Start () {
        // load XML document
        TextAsset textGraph = Resources.Load("landscape") as TextAsset;
        xmlGraph = XDocument.Parse(textGraph.text);

        // get nodes
        xmlNodes =
            from el in xmlGraph.Elements("graphml").Elements("graph").Elements("node")
            select el;

        xmlEdges =
            from el in xmlGraph.Elements("graphml").Elements("graph").Elements("edge")
            select el;


        parentContainer = GameObject.Find("Landscape").transform;

        // build model for each year
        Transform yearContainer;
        TextAsset textLayer = Resources.Load("LandscapeYears") as TextAsset;
        int year;

        string[] rows = textLayer.text.Split("\n"[0]);

        foreach (string row in rows)
        {
            // draw model
            //Debug.Log(row);
            year = int.Parse(row);

            yearContainer = CreateYearContainer(year);
            DrawLayers(yearContainer);
            DrawNodes(yearContainer);
            DrawEdges(yearContainer);

            //rescale hologram
            yearContainer.localScale = new Vector3(hologramScale, hologramScale, hologramScale);
            yearContainer.gameObject.SetActive(false); //temporary during construction
        }

        SetStartActiveStatus();

    }

    Transform CreateYearContainer(int year)
    {
        float xoffset = (year - 2018) * layerscale * xscale;
        Instantiate(prefabLandscapeContainer, new Vector3(xoffset, 0, 0), Quaternion.identity, parentContainer);
        Transform yearContainer = parentContainer.GetChild(parentContainer.childCount - 1);
        yearContainer.localPosition = new Vector3(xoffset, 0, 0);
        yearContainer.name = "Landscape" + year.ToString();

        // quick option to put all children in year container without refactoring. 
        return yearContainer;
    }

    void SetStartActiveStatus()
    {
        foreach (Transform landscape in parentContainer.GetComponentInChildren<Transform>(true))
        {
            landscape.gameObject.SetActive(startVisible);
        }
    }

    void DrawNodes(Transform yearContainer)
    {

        int nodecount = 0;

        float x = 0;
        float y = 0;
        float z = 0;
        string nodeName = "dummy";
        string nodeLongName = "dummy";
        int layerOrdinal;
        string nodeType = "Default";
        string nodeColour;
        string nodeBaseColour;
        Color clr;

        foreach (XElement node in xmlNodes)
        {
            layerOrdinal = int.Parse(node.Descendants().Where(a => a.Attribute("key").Value == "v_LayerOrdinal").Select(a => a.Value).FirstOrDefault());
            x = float.Parse(node.Descendants().Where(a => a.Attribute("key").Value == "v_X").Select(a => a.Value).FirstOrDefault()) * xscale;
            y = layerOrdinal * yscale + yoffset;
            z = float.Parse(node.Descendants().Where(a => a.Attribute("key").Value == "v_Z").Select(a => a.Value).FirstOrDefault()) * zscale;
            nodeName = node.Attribute("id").Value;
            nodeType = node.Descendants().Where(a => a.Attribute("key").Value == "v_ObjectType").Select(a => a.Value).FirstOrDefault();
            nodeLongName = node.Descendants().Where(a => a.Attribute("key").Value == "v_LongName").Select(a => a.Value).FirstOrDefault();
            nodeColour = node.Descendants().Where(a => a.Attribute("key").Value == "v_ObjectColour").Select(a => a.Value).FirstOrDefault();
            nodeBaseColour = node.Descendants().Where(a => a.Attribute("key").Value == "v_ObjectBaseColour").Select(a => a.Value).FirstOrDefault();

            Transform layerContainer = yearContainer.Find("Layer" + layerOrdinal.ToString());

            switch (nodeType)
            {
                //application
                case "RCH":
                    Instantiate(prefabNodeRectangleH, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                case "RCV":
                    Instantiate(prefabNodeRectangleV, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                case "CUB":
                    Instantiate(prefabNodeCube, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                
                //channel
                case "SPH":
                    Instantiate(prefabNodeSphere, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                case "ISO":
                    Instantiate(prefabNodeIcosphere, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                case "ISS":
                    Instantiate(prefabNodeIcosphereSmall, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                
                //data
                case "CYL":
                    Instantiate(prefabNodeCylinder, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;

                //service
                case "DOM":
                    Instantiate(prefabNodeSphereHalf, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                case "PYR":
                    Instantiate(prefabNodePyramid, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;
                case "CON":
                    Instantiate(prefabNodeCone, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    break;


                default:
                    Instantiate(prefabNodeCube, new Vector3(x, y, z), Quaternion.identity, layerContainer);
                    Debug.Log(nodeType);
                    break;
            }

            Transform nodeInstance = layerContainer.GetChild(layerContainer.childCount - 1);
            nodeInstance.localPosition = new Vector3(x, 0, z);
            nodeInstance.name = nodeName;

            NodeProperties nodeProperties = nodeInstance.gameObject.GetComponent<NodeProperties>();
            nodeProperties.NodeID = node.Attribute("id").Value;
            nodeProperties.LongName = nodeLongName;
            nodeProperties.LayerOrdinal = layerOrdinal.ToString();
            nodeProperties.Layer = node.Descendants().Where(a => a.Attribute("key").Value == "v_Layer").Select(a => a.Value).FirstOrDefault();
            nodeProperties.Critically = node.Descendants().Where(a => a.Attribute("key").Value == "v_Critically").Select(a => a.Value).FirstOrDefault();
            nodeProperties.Desirability = node.Descendants().Where(a => a.Attribute("key").Value == "v_Desirability").Select(a => a.Value).FirstOrDefault();
            nodeProperties.DisplayWeight = node.Descendants().Where(a => a.Attribute("key").Value == "v_DisplayWeight").Select(a => a.Value).FirstOrDefault();
            nodeProperties.BusinessArea = node.Descendants().Where(a => a.Attribute("key").Value == "v_Business.Area").Select(a => a.Value).FirstOrDefault();
            nodeProperties.BusinessFunction = node.Descendants().Where(a => a.Attribute("key").Value == "v_Business.Function").Select(a => a.Value).FirstOrDefault();
            nodeProperties.IsMarkets = node.Descendants().Where(a => a.Attribute("key").Value == "v_non.Markets").Select(a => a.Value).FirstOrDefault() != "1";

            //size by display weight
            float displayWeight = float.Parse(nodeProperties.DisplayWeight);
            nodeInstance.localScale = new Vector3(nodeInstance.localScale.x * displayWeight * objectscale, nodeInstance.localScale.y * displayWeight * objectscale, nodeInstance.localScale.z * displayWeight * objectscale);

            //set colour
            ColorUtility.TryParseHtmlString("#" + nodeColour, out clr);
            nodeInstance.GetComponent<Renderer>().material.SetColor("_Color", clr);
            

            Light light = nodeInstance.GetComponent<Light>();
            light.range = displayWeight / 2f;
            light.enabled = false;

            Text txt = nodeInstance.GetComponentInChildren<Text>();
            txt.text = nodeLongName;

            nodeInstance.GetComponentInChildren<Canvas>().enabled = true;
            
            nodecount++;
        }

    }

    //Color HexToColor(string hex)
    //{
    //    byte R = (byte)((HexVal >> 16) & 0xFF);
    //    byte G = (byte)((HexVal >> 8) & 0xFF);
    //    byte B = (byte)((HexVal) & 0xFF);
    //    return new Color32(R, G, B, 255);
    //}

    //void AddChildTag(Transform trn, string tag)
    //{
    //    //create empty child object to hold tag
    //    GameObject child = new GameObject("Tag");

    //    //tag child
    //    child.tag = tag;

    //    //add tag to game object
    //    child.transform.parent = trn.transform;
    //}
    
    void DrawEdges(Transform yearContainer)
    {
        //Debug.Log("start draw edges");

        Transform edgeContainer = Instantiate(prefabEdgeContainer, new Vector3(0, 0, 0), Quaternion.identity, yearContainer);
        edgeContainer.name = "EdgeContainer";
        edgeContainer.localPosition = new Vector3(0, 0, 0);

        foreach (XElement edge in xmlEdges)
        {
            //Debug.Log(edge.Attribute("source").Value);

            GameObject startNode = GameObject.Find(edge.Attribute("source").Value);
            GameObject endNode = GameObject.Find(edge.Attribute("target").Value);
            //GameObject startNode = yearContainer.Find(edge.Attribute("source").Value).gameObject;
            //GameObject endNode = yearContainer.Find(edge.Attribute("target").Value).gameObject;

            Vector3 centerPosition = (startNode.transform.position + endNode.transform.position) / 2f;
            float dist = Vector3.Distance(startNode.transform.position, endNode.transform.position);

            //Transform edgeInstance = Instantiate(prefabEdge, startNode.transform.position, Quaternion.identity, yearContainer);
            Transform edgeInstance = Instantiate(prefabEdge, centerPosition, Quaternion.identity, edgeContainer);
            edgeInstance.LookAt(endNode.transform);
            edgeInstance.transform.localScale = new Vector3(edgexscale, edgeyscale, dist);

            EdgeProperties edgeProperties = edgeInstance.gameObject.GetComponent<EdgeProperties>();
            edgeProperties.fromNode = edge.Attribute("source").Value;
            edgeProperties.toNode = edge.Attribute("target").Value;
            edgeProperties.flowType = edge.Descendants().Where(a => a.Attribute("key").Value == "e_Type").Select(a => a.Value).FirstOrDefault(); ;
            edgeProperties.flowRate = edge.Descendants().Where(a => a.Attribute("key").Value == "e_Frequency").Select(a => a.Value).FirstOrDefault(); ; ;
            edgeProperties.dataClass = edge.Descendants().Where(a => a.Attribute("key").Value == "e_Data").Select(a => a.Value).FirstOrDefault(); ; ;
            edgeProperties.isBidirectional = (int.Parse(edge.Descendants().Where(a => a.Attribute("key").Value == "e_Direction").Select(a => a.Value).FirstOrDefault()) == 2);
            edgeProperties.fromLayerOrdinal = int.Parse(startNode.GetComponent<NodeProperties>().LayerOrdinal);
            edgeProperties.toLayerOrdinal = int.Parse(endNode.GetComponent<NodeProperties>().LayerOrdinal);

            edgeInstance.name = edgeProperties.fromNode + '>' + edgeProperties.toNode;
            
            // bent edges
            if (edgeProperties.fromLayerOrdinal.Equals(2) && edgeProperties.toLayerOrdinal.Equals(2))
            {
                float ydrop = yscale * yBentMean - Random.Range(yBentDrop, yBentDrop + 1f);

                // drop existing
                edgeInstance.transform.position = new Vector3(centerPosition.x, centerPosition.y - ydrop, centerPosition.z);
                SetupEdgeAnimation(edgeInstance, new Vector3(startNode.transform.position.x, startNode.transform.position.y - ydrop, startNode.transform.position.z), new Vector3(endNode.transform.position.x, endNode.transform.position.y - yscale, endNode.transform.position.z));

                // add drop
                Vector3 centerDropPosition = new Vector3(startNode.transform.position.x, startNode.transform.position.y - ydrop / 2 , startNode.transform.position.z);
                Transform edgeDropInstance = Instantiate(prefabEdge, centerDropPosition, Quaternion.identity, edgeContainer);
                edgeDropInstance.transform.localScale = new Vector3(edgexscale, edgeyscale, ydrop);
                edgeDropInstance.LookAt(new Vector3(startNode.transform.position.x, startNode.transform.position.y - ydrop, startNode.transform.position.z));
                edgeDropInstance.name = edgeProperties.fromNode + '>' + edgeProperties.toNode + "- DROP";

                EdgeProperties edgeDropProperties = edgeDropInstance.gameObject.GetComponent<EdgeProperties>();
                CopyEdgeProperties(edgeProperties, edgeDropProperties);

                SetupEdgeAnimation(edgeDropInstance, new Vector3(startNode.transform.position.x, startNode.transform.position.y, startNode.transform.position.z), new Vector3(startNode.transform.position.x, startNode.transform.position.y - ydrop, startNode.transform.position.z));

                // add go up
                Vector3 centerUpPosition = new Vector3(endNode.transform.position.x, endNode.transform.position.y - ydrop / 2, endNode.transform.position.z);
                Transform edgeUpInstance = Instantiate(prefabEdge, centerUpPosition, Quaternion.identity, edgeContainer);
                edgeUpInstance.transform.localScale = new Vector3(edgexscale, edgeyscale, ydrop);
                edgeUpInstance.LookAt(new Vector3(endNode.transform.position.x, endNode.transform.position.y, endNode.transform.position.z));
                edgeUpInstance.name = edgeProperties.fromNode + '>' + edgeProperties.toNode + "- UP";

                EdgeProperties edgeUpProperties = edgeUpInstance.gameObject.GetComponent<EdgeProperties>();
                CopyEdgeProperties(edgeProperties, edgeUpProperties);

                SetupEdgeAnimation(edgeUpInstance, new Vector3(endNode.transform.position.x, endNode.transform.position.y - ydrop, endNode.transform.position.z), new Vector3(endNode.transform.position.x, endNode.transform.position.y, endNode.transform.position.z));

            }
            else
            {
                SetupEdgeAnimation(edgeInstance, startNode.transform.position, endNode.transform.position);
            }




            //tag edges with node layers
            //AddChildTag(edgeInstance, "Layer" + startNode.GetComponent<NodeProperties>().LayerOrdinal);
            //AddChildTag(edgeInstance, "Layer" + endNode.GetComponent<NodeProperties>().LayerOrdinal);

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
    
    void CopyEdgeProperties(EdgeProperties edgeFromInstance, EdgeProperties edgeToInstance)
    {
        edgeToInstance.fromNode = edgeFromInstance.fromNode;
        edgeToInstance.toNode = edgeFromInstance.toNode;
        edgeToInstance.flowType = edgeFromInstance.flowType;
        edgeToInstance.flowRate = edgeFromInstance.flowRate;
        edgeToInstance.dataClass = edgeFromInstance.dataClass;
        edgeToInstance.isBidirectional = edgeFromInstance.isBidirectional;
        edgeToInstance.fromLayerOrdinal = edgeFromInstance.fromLayerOrdinal;
        edgeToInstance.toLayerOrdinal = edgeFromInstance.toLayerOrdinal;

    }

    void SetupEdgeAnimation(Transform edgeInstance, Vector3 startPos, Vector3 endPos)
    {

        // add Animation
        Transform objAnimationOut = edgeInstance.Find("AnimationOut");
        Transform objAnimationIn = edgeInstance.Find("AnimationIn");

        EdgeProperties edgeProperties = edgeInstance.gameObject.GetComponent<EdgeProperties>();
        ParticleSystem objPartOut = objAnimationOut.GetComponentInChildren<ParticleSystem>();
        ParticleSystem objPartIn = objAnimationIn.GetComponentInChildren<ParticleSystem>();
        var pSmainOut = objPartOut.main;
        var pSmainIn = objPartIn.main;
        var pEmissionOut = objPartOut.emission;
        var pEmissionIn = objPartIn.emission;
        float dist = edgeInstance.localScale.z;

        // set attributes
        objAnimationOut.position = startPos;
        pSmainOut.startLifetime = new ParticleSystem.MinMaxCurve(dist / objPartOut.main.startSpeed.constant);
        pSmainIn.startLifetime = new ParticleSystem.MinMaxCurve(dist / objPartOut.main.startSpeed.constant);
        /*
        Debug.Log("Edge: " + edgeInstance.transform.position);
        Debug.Log("Rotation: " + edgeInstance.transform.rotation);
        Debug.Log("Distance: " + dist);
        Debug.Log("Calculated: ");
        Debug.Log("Animation " + objAnimationOut.position);
        */
        // disable incoming if not both directional
        if (!edgeProperties.isBidirectional.Equals(true))
        {
            pEmissionIn.enabled = false;
        }
        else
        {
            // set up incoming link in opposite direction
            objAnimationIn.position = endPos;
            objAnimationIn.LookAt(objAnimationOut.position);
        }

        // delay for EOD
        if (edgeProperties.flowRate.Equals("EOD"))
        {
            pEmissionOut.rateOverTime = pEmissionOut.rateOverTime.constant / EODdelay;
            pEmissionIn.rateOverTime = pEmissionOut.rateOverTime;
        }

        // change color for integration types
        
        switch (edgeProperties.flowType)
        {
            case "API":
                pSmainOut.startColor = new Color(72, 54, 128);  // blue
                break;
            case "Batch":
                pSmainOut.startColor = new Color(221, 133, 10); // orange
                break;
            case "MSG":
                pSmainOut.startColor = new Color(131, 42, 42); // red
                break;
        }
        
    }


    void DrawLayers(Transform yearContainer)
    {
        TextAsset textLayer = Resources.Load("layers") as TextAsset;

        string[] rows = textLayer.text.Split("\n"[0]);

        foreach (string row in rows)
        {
            if (row != "")
            {
                string[] rowAttributes = row.Split(","[0]);

                float y = float.Parse(rowAttributes[0]) * yscale + yoffset;
                float layerx = float.Parse(rowAttributes[2]) * layerscale * xscale;
                float layerz = float.Parse(rowAttributes[3]) * layerscale * zscale;
                Color layerColor = new Color(float.Parse(rowAttributes[4]), float.Parse(rowAttributes[5]), float.Parse(rowAttributes[6]));

                Instantiate(prefabLayerContainer, new Vector3(0, y, 0), Quaternion.identity, yearContainer);
                Transform containerInstance = yearContainer.GetChild(yearContainer.childCount - 1);
                containerInstance.name = "Layer" + rowAttributes[0];

                //Quaternion target = Quaternion.Euler(90, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
                //Quaternion target = Quaternion.Euler(90, 0, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);

                //Instantiate(prefabLayer, new Vector3(0, y, 0), transform.rotation, yearContainer);
                Instantiate(prefabLayer, new Vector3(0, 0, 0), Quaternion.identity, containerInstance);

                Transform layerInstance = containerInstance.GetChild(containerInstance.childCount - 1);
                RectTransform layerRect = layerInstance.gameObject.GetComponent<RectTransform>();
                layerInstance.name = rowAttributes[1];
                layerInstance.localPosition = new Vector3(0, 0, 0);
                layerInstance.Rotate(90, 0, 0);
                layerRect.sizeDelta = new Vector2(layerx, layerz);

                //set layer text
                layerInstance.Find("Panel/North").GetComponent<Text>().text = layerInstance.name;
                layerInstance.Find("Panel/South").GetComponent<Text>().text = layerInstance.name;
                layerInstance.Find("Panel/East").GetComponent<Text>().text = layerInstance.name;
                layerInstance.Find("Panel/West").GetComponent<Text>().text = layerInstance.name;

                    
                //set beading           
                Transform northCylinder = layerInstance.Find("Panel/North/Cylinder");
                Transform westCylinder = layerInstance.Find("Panel/West/Cylinder");
                Transform southCylinder = layerInstance.Find("Panel/South/Cylinder");
                Transform eastCylinder = layerInstance.Find("Panel/East/Cylinder");

                // change beading size
                northCylinder.localScale = new Vector3(northCylinder.localScale.x, layerz * zscale, northCylinder.localScale.z);
                westCylinder.localScale = new Vector3(westCylinder.localScale.x, layerx * xscale, westCylinder.localScale.z);
                southCylinder.localScale = new Vector3(southCylinder.localScale.x, layerz * zscale, southCylinder.localScale.z);
                eastCylinder.localScale = new Vector3(eastCylinder.localScale.x, layerx * xscale, eastCylinder.localScale.z);

                // change beading color
                northCylinder.GetComponent<Renderer>().materials[0].color = layerColor;
                westCylinder.GetComponent<Renderer>().materials[0].color = layerColor;
                southCylinder.GetComponent<Renderer>().materials[0].color = layerColor;
                eastCylinder.GetComponent<Renderer>().materials[0].color = layerColor;

                //set layer properties
                containerInstance.GetComponent<ContainerProperties>().Ordinal = int.Parse(rowAttributes[0]);

                //create layer UI
                Instantiate(prefabLayerUI, new Vector3(0, y, 0), Quaternion.identity, yearContainer);

                Transform uiInstance = yearContainer.GetChild(yearContainer.childCount - 1);
                RectTransform uiRect = uiInstance.gameObject.GetComponent<RectTransform>();
                uiInstance.name = "LayerUI" + rowAttributes[0];
                //uiInstance.localPosition = new Vector3(0, y, 0);
                uiInstance.Rotate(90, 0, 0);
                uiRect.sizeDelta = new Vector2(layerx, layerz);

                uiInstance.GetComponent<LayerUIBehaviour>().SetUp(containerInstance);

            }
        }

    }

}

