@script ExecuteInEditMode() // 41 Post - Created by DimasTheDriver on Dec/12/2011 . Part of the 'Unity: How to create a speech balloon' post. Available at: http://www.41post.com/?p=4545 

//this game object's transform
private var goTransform:Transform;
//the game object's position on the screen, in pixels
private var goScreenPos:Vector3;
//the game objects position on the screen
private var goViewportPos:Vector3;

//the width of the speech bubble
public var bubbleWidth:int = 200;
//the height of the speech bubble
public var bubbleHeight:int = 100;

//an offset, to better position the bubble 
public var offsetX:float = 0;
public var offsetY:float = 150;
	
//an offset to center the bubble 
private var centerOffsetX:int;
private var centerOffsetY:int;
	
//a material to render the triangular part of the speech balloon
public var mat:Material;
//a guiSkin, to render the round part of the speech balloon
public var guiSkin:GUISkin;
	
//use this for early initialization
function Awake() 
{
	//get this game object's transform
	goTransform = this.GetComponent(Transform);
}
	
//use this for initialization
function Start()
{
	//if the material hasn't been found
	if (!mat) 
	{
		Debug.LogError("Please assign a material on the Inspector.");
		return;
	}
	
	//if the guiSkin hasn't been found
	if (!guiSkin) 
	{
		Debug.LogError("Please assign a GUI Skin on the Inspector.");
		return;
	}
	
	//Calculate the X and Y offsets to center the speech balloon exactly on the center of the game object
	centerOffsetX = bubbleWidth/2;
	centerOffsetY = bubbleHeight/2;
}


//Called once per frame, after the update
function LateUpdate() 
{
	//find out the position on the screen of this game object
	goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);	
	
	//Could have used the following line, instead of lines 70 and 71
	//goViewportPos = Camera.main.WorldToViewportPoint(goTransform.position);
	goViewportPos.x = goScreenPos.x/parseFloat(Screen.width);
	goViewportPos.y = goScreenPos.y/parseFloat(Screen.height);
}

//Draw GUIs
function OnGUI()
{
	//Begin the GUI group centering the speech bubble at the same position of this game object. After that, apply the offset
	GUI.BeginGroup(new Rect(goScreenPos.x-centerOffsetX-offsetX,Screen.height-goScreenPos.y-centerOffsetY-offsetY,bubbleWidth,bubbleHeight));
		
		//Render the round part of the bubble
		GUI.Label(new Rect(0,0,200,100),"",guiSkin.customStyles[0]);
		
		//Render the text
		GUI.Label(new Rect(10,25,190,50),"You can add any GUI element here, like the button below:",guiSkin.label);
	
		//If the button is pressed, go back to 41 Post
		if(GUI.Button(new Rect(50,60,100,30),"Back to post..."))
		{
			Application.OpenURL("http://www.41post.com/?p=4545");
		}
	
	GUI.EndGroup();
}

//Called after camera has finished rendering the scene
function OnRenderObject()
{
	//push current matrix into the matrix stack
	GL.PushMatrix();
	//set material pass
	mat.SetPass(0);
	//load orthogonal projection matrix
	GL.LoadOrtho();
	//a triangle primitive is going to be rendered
	GL.Begin(GL.TRIANGLES);

		//set the color
		GL.Color(Color.white);
		
		//Define the triangle vetices
		GL.Vertex3(goViewportPos.x, goViewportPos.y+(offsetY/3)/Screen.height, 0.1f);
		GL.Vertex3(goViewportPos.x - (bubbleWidth/3)/parseFloat(Screen.width), goViewportPos.y+offsetY/Screen.height, 0.1f);
		GL.Vertex3(goViewportPos.x - (bubbleWidth/8)/parseFloat(Screen.width), goViewportPos.y+offsetY/Screen.height, 0.1f);
	
	GL.End();
	//pop the orthogonal matrix from the stack
	GL.PopMatrix();
}