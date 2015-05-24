using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private PlayerMove player;
	private bool jumpPressed;
	private string horizontalInput;
	private string jumpButton;
	private bool itOrNot;
	public bool canTag;
	private int playerScore = 0;

	private Transform tagLabel;
	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
		if(animator == null)
		{
			Debug.LogError("Error: The animator was not found for a player object.");
		}
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
        //If speedup not due to powerup
        if(newSpeed < 10)
        {
            player.SetTrueSpeed(newSpeed);
        }
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

		//start score counter
		InvokeRepeating("CountScore", 1.0f, 1.0f);

		animator.SetBool("tagStunned", true);
		StartCoroutine(WaitForStun(1.5f));
	}

	void OnCollisionStay2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Player")
		{
			//Check if tagger is IT
			//If so, then swap tag status with tagee
			if (itOrNot && !coll.gameObject.GetComponent<PlayerController>().GetItOrNot() && canTag)
			{
				//handle tagging object
				itOrNot = false;
				SetLabelInactive();
				SetPlayerSpeed(5.0f);

				//stop incrementing the score
				CancelInvoke("CountScore");

				//handle the tagged player
				coll.gameObject.GetComponent<PlayerController>().SetIt();

			}
		}
        else if (coll.gameObject.tag == "PowerUp")
        {
            int powerType = coll.gameObject.GetComponent<PowerUpScript>().GetPowerType();
            switch(powerType)
            {
                //Speed PowerUp
                case 1:
                    StartCoroutine(WaitForSpeedUp());
                    Destroy(coll.gameObject);
                    break;

                default:
                    Debug.LogError("PowerUp type " + powerType + " is invalid.");
                    break;
            }
        }
	}

	void CountScore()
	{
		playerScore++;
	}

	public int GetPlayerScore()
	{
		return playerScore;
	}
	
	//wait for stun to ware off
	IEnumerator WaitForStun(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		canTag = true;
		animator.SetBool("tagStunned", false);
	}

    void DoubleSpeed()
    {
        SetPlayerSpeed(2.0f * player.GetTrueSpeed());
    }

    //Wait for powerup 1 effect to ware off
    IEnumerator WaitForSpeedUp()
    {
        //double speed
        InvokeRepeating("DoubleSpeed", 0.0f, 0.001f);
        yield return new WaitForSeconds(4.0f);
        CancelInvoke("DoubleSpeed");
        SetPlayerSpeed(player.GetTrueSpeed());
    }
}
