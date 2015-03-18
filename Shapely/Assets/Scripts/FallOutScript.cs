using UnityEngine;
using System.Collections;

public class FallOutScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Respawn");
		GameObject closest = gos[0];
		float distance = Mathf.Infinity;
		Vector3 position = coll.gameObject.transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		
		coll.gameObject.transform.position = closest.transform.position;
	}
}
