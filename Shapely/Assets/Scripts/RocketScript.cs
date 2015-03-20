using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

	public float speed = 2.0f;
	
	void Awake()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * speed, 0);
	}
	
	void Update () {
		if(transform.position.x < -46)
		{
			Debug.Log("here");
			transform.position = new Vector3(47f, transform.position.y, 0);
		}
	}
}
