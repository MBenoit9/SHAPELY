using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour {

	private static int timer = 0;
	Text uiText;

	void Awake () 
	{
		uiText = GetComponent<Text>(); // grab the text component
	}

	void Start()
	{
		InvokeRepeating("ReduceTime", 1.0f, 1.0f);
	}

	void Update () 
	{
		if(timer > 0)
			uiText.text = "Time: " + timer; //update text with current time
		else
			uiText.text = "GAME OVER";
	}

	public void SetTime(int newTime)
	{
		timer = newTime;
	}

	public int GetTime()
	{
		return timer;
	}

	void ReduceTime()
	{
		timer--;

		//If timer has reached 0, then stop counting down.
		if(timer == 0)
		{
			CancelInvoke("ReduceTime");

		}
	}
}
