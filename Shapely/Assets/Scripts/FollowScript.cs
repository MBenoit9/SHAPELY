using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {

	GameObject[] players;
	int numberOfPlayers;
	float maxDistance = 0;
	
	void Start () {
		//Find all players
		players = GameObject.FindGameObjectsWithTag("Player");
		numberOfPlayers = players.Length;
		maxDistance = FindMaxDistance();
	}
	
	
	void Update () {
		//Set position of follow object
		float totalX = 0;
		float totalY = 0;
		foreach(GameObject player in players)
		{
			totalX = totalX + player.transform.position.x;
			totalY = totalY + player.transform.position.y;
		}
		
		transform.position = new Vector3(totalX/numberOfPlayers, totalY/numberOfPlayers, 0);
	}
	
	float FindMaxDistance()
	{
		float max = 0;
		for(int i = 0; i < players.Length; i++)
		{
			for(int j = 0; j < players.Length; j++)
			{
				float distance = Vector3.Distance(players[i].transform.position, players[j].transform.position);
				if(max < distance)
					max = distance;
			}
		}
		
		return max;
	}
	
	public float ChangeZoom()
	{
		float newMaxDistance = FindMaxDistance();
		if(maxDistance > newMaxDistance)
		{
			float difference;
			difference = maxDistance - newMaxDistance;
			maxDistance = newMaxDistance;
			
			return -1 * (difference/5);
		}
		else if(maxDistance < newMaxDistance)
		{
			float difference;
			difference = newMaxDistance - maxDistance;
			maxDistance = newMaxDistance;
			
			return difference/5;
		}
		else
			return 0;
	}
}
