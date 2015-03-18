using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	GameObject[] players;
	private int firstIt;

	// Use this for initialization
	void Start () 
	{
		//find out how many players you have
		players = GameObject.FindGameObjectsWithTag("Player");

		//randomly choose a player to start the game as it.
		firstIt = Random.Range( 0, players.Length);
		players[firstIt].GetComponent<PlayerController>().SetItOrNot(true);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
