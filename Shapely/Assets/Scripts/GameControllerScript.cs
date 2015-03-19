using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	GameObject[] players;
	GameObject[] scores;
	private int firstIt;
	private GameObject timer;

	//Only Temporary
	void Awake()
	{
		timer = GameObject.Find("Overlay/Timer");
		if(timer == null)
		{
			Debug.LogError("Timer was not found!");
		}
		SetTimerInitial(10);
	}

	void Start () 
	{
		//find out how many players you have
		players = GameObject.FindGameObjectsWithTag("Player");
		if(players.Length == 0)
		{
			Debug.LogError("No players were found!");
		}

		//randomly choose a player to start the game as it.
		firstIt = Random.Range( 0, players.Length);
		players[firstIt].GetComponent<PlayerController>().SetIt();

		//set timer
		timer = GameObject.Find("Overlay/Timer");
		if(timer == null)
		{
			Debug.LogError("Timer was not found!");
		}

		//find all the score labels
		scores = GameObject.FindGameObjectsWithTag("ScoreText");
		if(scores.Length == 0)
		{
			Debug.Log("No score objets were found!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//check if countdown has finished
		if(timer.GetComponent<TimerScript>().GetTime() == 0)
		{
			//if countdown has finished then start game over process
			Debug.Log ("The Game Has Ended!!!!");
			CancelInvoke("UpdateScores");
		}
		else
			UpdateScores();
	}

	//set start time for timer
	public void SetTimerInitial(int startTime)
	{
		timer.GetComponent<TimerScript>().SetTime(startTime);
	}
	
	public int GetNumberOfPlayers()
	{
		return players.Length;
	}

	//update the scores every frame
	public void UpdateScores()
	{
		//find the player who is currently it
		for(int i = 0; i < scores.Length; i++)
		{
			scores[i].GetComponent<ScoreScript>().SetScore(players[i].GetComponent<PlayerController>().GetPlayerScore());
		}
	}
}
