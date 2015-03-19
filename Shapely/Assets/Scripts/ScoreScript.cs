using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	private int score = 0;
	Text uiText;
	
	void Awake()
	{
		uiText = GetComponent<Text>(); // grab the text component
	}
	
	void Update () 
	{
		uiText.text = "Score: " + score;
	}

	public void SetScore(int newScore)
	{
		score = newScore;
	}

	public int GetScore()
	{
		return score;
	}
}
