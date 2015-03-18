﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private PlayerMove player;
	private bool jumpPressed;
	private string horizontalInput;
	private string jumpButton;
	private bool itOrNot;
	public bool canTag;

	private Transform tagLabel;

	void Awake()
	{
		itOrNot = false;
		canTag = true;
		tagLabel = transform.Find("TagLabel");
		//Set It label above player to invisible
		SetLabelInactive();

		SetPlayerControls();
	}

	void Start () {
	
	}

	void Update () 
	{
		jumpPressed = Input.GetButtonDown(jumpButton);
	}

	void FixedUpdate()
	{
		//if a player has been tagged then he/she has to wait for stun to ware off
		if(canTag)
		{
			float horizontalMovement = Input.GetAxis(horizontalInput);
			player.Move(horizontalMovement, jumpPressed);
		}
		else
		{
			player.Move(0, false);
		}
	}

	void SetPlayerControls ()
	{
		player = GetComponent<PlayerMove>();
		if(name == "Player1")
		{
			horizontalInput = "Horizontal1";
			jumpButton = "Jump1";
		}
		
		else if(name == "Player2")
		{
			horizontalInput = "Horizontal2";
			jumpButton = "Jump2";
		}
		
		else if(name == "Player3")
		{
			horizontalInput = "Horizontal3";
			jumpButton = "Jump3";
		}
		
		else if(name == "Player4")
		{
			horizontalInput = "Horizontal4";
			jumpButton = "Jump4";
		}
	}

	public void SetItOrNot(bool newIt)
	{
		itOrNot = newIt;

		if(newIt)
			SetLabelActive();
		else
			SetLabelInactive();
	}

	public bool GetItOrNot()
	{
		return itOrNot;
	}

	public void SetCanTag(bool newTag)
	{
		canTag = newTag;
	}

	public bool GetCanTag()
	{
		return canTag;
	}

	public void SetPlayerSpeed(float newSpeed)
	{
		player.SetSpeed(newSpeed);
	}

	public void SetLabelActive()
	{
		tagLabel.gameObject.SetActive(true);
	}

	public void SetLabelInactive()
	{
		tagLabel.gameObject.SetActive(false);
	}

	public void SetIt()
	{
		itOrNot = true;
		canTag = false;
		SetLabelActive();
		SetPlayerSpeed(8.0f);

		StartCoroutine(waitALittle(1.5f));
	}

	void OnCollisionStay2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Player")
		{
			//Check if tagger is IT
			//If so, then swap tag status with tagee
			if (itOrNot && !coll.gameObject.GetComponent<PlayerController>().GetItOrNot() && canTag)
			{
				Debug.Log ("Tag");
				//handle tagging object
				itOrNot = false;
				SetLabelInactive();
				player.SetSpeed(5.0f);

				//handle the tagged player
				coll.gameObject.GetComponent<PlayerController>().SetIt();

			}
		}
	}

	//wait for stun to ware off
	IEnumerator waitALittle(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		canTag = true;
	}
}