using System.Collections;
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

    // Use this for initialization
    void Start () {
        // load XML document
        TextAsset textGraph = Resources.Load("landscape") as TextAsset;
        xmlGraph = new XmlDocument();
        xmlGraph.LoadXml(textGraph.text);

        // get nodes
        xmlNodes = xmlGraph.SelectNodes("/graphml/graph/node");
        xmlEdges = xmlGraph.SelectNodes("/graphml/graph/edge");

        int nodecount = 0;

        float x = 0;
        float y = 0;
        float z = 0;
        float yoffset = -10f;
        float yscale = 10f;
        string nodeName = "dummy";
        string nodeType = "Default"; 

        graphTransform = GameObject.Find("Landscape").transform;

        foreach (XmlNode node in xmlNodes)
        {           
            x = float.Parse(node.SelectSingleNode("data[@key='v_X']").InnerText);
            y = float.Parse(node.SelectSingleNode("data[@key='v_LayerOrdinal']").InnerText) * yscale + yoffset;
            z = float.Parse(node.SelectSingleNode("data[@key='v_Z']").InnerText);
            nodeName = node.Attributes["id"].Value;
            nodeType = node.SelectSingleNode("data[@key='v_Layer']").InnerText;

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

            Text txt = nodeInstance.GetComponentInChildren<Text>();
            txt.text = nodeName;

            nodeInstance.GetComponentInChildren<Canvas>().enabled = false;

            nodecount++;
        }

        DrawEdges();
        
        //Instantiate(node, new Vector3(0.134f, 2.564f, 0), Quaternion.identity);
    }
	
    void DrawEdges()
    {

        foreach (XmlNode edge in xmlEdges)
        {
            Debug.Log(edge.InnerXml);
            
            GameObject startNode = GameObject.Find(edge.Attributes["source"].Value);
            GameObject endNode = GameObject.Find(edge.Attributes["target"].Value);

            //float startX = startNode.transform.position.x;
            //float startY = startNode.transform.position.y;
            //float startZ = startNode.transform.position.z;

            //float endX = endNode.transform.position.x;
            //float endY = endNode.transform.position.y;
            //float endZ = endNode.transform.position.z;

            Vector3 centerPosition = (startNode.transform.position + endNode.transform.position) / 2f;
            float dist = Vector3.Distance(startNode.transform.position, endNode.transform.position);

            //Debug.Log(vectorDir.ToString());
            //Quaternion rotation = Quaternion.Euler(vectorDir);



            //Transform edgeInstance = Instantiate(prefabEdge, startNode.transform.position, Quaternion.identity, graphTransform);
            Transform edgeInstance = Instantiate(prefabEdge, centerPosition, Quaternion.identity, graphTransform);
            edgeInstance.LookAt(endNode.transform);
            edgeInstance.transform.localScale = new Vector3(0.01f, 0.01f, dist);

            //var edgeData = edgeInstance.transform.GetComponent<EdgeData>();
            

            //float lineAngle = Vector3.Angle(startNode.transform.position, endNode.transform.position);

            //float length = Mathf.Pow(endX - startX, 2);

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

    void DrawLayer(float y)
    {
        Quaternion target = Quaternion.Euler(90, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);

        Instantiate(prefabLayer, new Vector3(0, y, 0), transform.rotation, graphTransform);
    }

	// Update is called once per frame
	//void Update () {
		
	//}

    //private 

    //private DataTable GetDataTableFromCsv(string path)
    //{
    //    DataTable dataTable = new DataTable();
    //    string[] csv = File.ReadAllLines(path);

    //    foreach (string csvrow in csv)
    //    {
    //        var fields = csvrow.Split(','); // csv delimiter
    //        var row = dataTable.NewRow();

    //        row.ItemArray = fields;
    //        dataTable.Rows.Add(row);
    //    }

    //    return dataTable;
    //}

}
