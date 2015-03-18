using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject follow;
	private Vector3 offset;
	
	public float minZoomIn;
	public float maxZoomOut;
	
	FollowScript followScript;
	
	void Start () 
	{
		follow = GameObject.Find("FollowObject");
		offset = transform.position;
		followScript = follow.GetComponent<FollowScript>();
	}
	
	void LateUpdate () {
		//Move camera to the position of the follow object
		transform.position = follow.transform.position + offset;
		
		//Zoom in or out depending on the spacing of the players
		float newSize = GetComponent<Camera>().orthographicSize + followScript.ChangeZoom();
		if(newSize < minZoomIn)
		{
			newSize = minZoomIn;
		}
		
		else if(newSize > maxZoomOut)
		{
			newSize = maxZoomOut;
		}
		
		GetComponent<Camera>().orthographicSize = newSize;
	}
}
