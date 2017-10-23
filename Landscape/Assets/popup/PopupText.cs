using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour {
    public string myName = "popuptext";
    public GameObject owner = null; 
	public Billboard billb = new Billboard();
    public Vector3 currentpos;
    private string theMessage = "Info:  ";

	public void setOwner( GameObject go){
		owner = go;
	}
	public void setBillboardDisplay(TextMesh tm){
		billb.setTextMesh (tm);
	}
    // Use this for initialization
    void Start () {
		if (owner == null) {
			Debug.LogAssertion ("The owner has to be set");
		}
       

      currentpos = owner.transform.position;

     
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = owner.GetComponent<Renderer>();
		TextMesh infoText = billb.getTextDisplay();

        if (Input.GetKeyDown(KeyCode.R))
        {
            rend.material.color = Color.red;
            theMessage = "Red is the colour";
			if (infoText != null) {
				infoText.text = theMessage;
			}
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            rend.material.color = Color.blue;
			theMessage = "Bbue is the colour";if (infoText != null) {
				infoText.text = theMessage;
			}
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            rend.material.color = Color.white;
			theMessage = "White is the colour";if (infoText != null) {
				infoText.text = theMessage;
			}
        }
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Vector3 newpos = new Vector3(currentpos.x + 1f, currentpos.y + 1f, currentpos.z + 1f);
            owner.transform.position = newpos;
			theMessage = "press ++++";if (infoText != null) {
				infoText.text = theMessage;
			}
        }
        if (Input.GetKeyUp(KeyCode.Plus ) || Input.GetKeyUp(KeyCode.KeypadPlus))
        {

            owner.transform.position = currentpos;
            infoText.text = "....";
        }
    }
    public string getMessage()
    {
        return theMessage;
    }
}
