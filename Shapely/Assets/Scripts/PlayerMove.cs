using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float speed = 5.0f;
    public float trueSpeed = 5.0f;
	public float jumpSpeed = 10f;
	float groundCheckRadius = 0.2f;
	float ceilingCheckRadius = 0.02f;
	
	Transform groundCheck, ceilingCheck;
	Transform playerGraphics;
	
	bool doubleJumpAvailable = true;
	bool lookingRight = true;
	bool onGround = false;
	[SerializeField] bool moveInAir = true;

	[SerializeField] LayerMask groundThings;

	Animator animator;

	void Awake()
	{
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");

		//Get the child object that holds the sprite. The sprite is kept in a child
		//game object in order to make working with it more flexible.
		playerGraphics = transform.FindChild("Graphics");
		
		if(playerGraphics == null)
		{
			Debug.LogError("Graphic for player not found.");
		}
		
		//Get the animator component. If Animator is not attached then throw an error.
		animator = GetComponent<Animator>();
		
		if(animator == null)
		{
			Debug.LogError("Animator for player not found.");
		}
		
		//Set direction sprite will face at the start
		if(transform.position.x >= 0)
		{
			Flip ();
		}
	}
	
	void Start () 
	{
		//Set tag manager
		//tagManager = GameObject.Find("_TagObject").GetComponent<TagManager>();
		
		//Get attached tag status script
		//itOrNot = GetComponent<TagStatus>();
	}

	void Update () 
	{
		animator.SetBool("ground", onGround);
		animator.SetFloat("horizontalVelocity", GetComponent<Rigidbody2D>().velocity.x);
		animator.SetFloat("verticalVelocity", GetComponent<Rigidbody2D>().velocity.y);
	}

	void FixedUpdate()
	{
		onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundThings);

		//If player has landed, reset double jump
		if(onGround)
		{
			doubleJumpAvailable = true;
		}
	}

	public void Move(float horizontalMove, bool jumpPressed)
	{
		//Run to the left or to the right
		if(onGround || moveInAir)
		{
			//Set velocity of moving player
			GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalMove * speed, GetComponent<Rigidbody2D>().velocity.y);
			
			//Change direction player is facing depending on if they are moving left or right
			if (horizontalMove < 0 && lookingRight)
			{
				Flip ();
			}
			
			if (horizontalMove > 0 && !lookingRight)
			{
				Flip ();
			}
		}
		
		//Jump
		if((onGround || doubleJumpAvailable)&& jumpPressed)
		{
			
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
			//rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			//check if player can double jump
			if(!onGround && doubleJumpAvailable)
			{
				doubleJumpAvailable = false;
			}
		}
	}

	void Flip()
	{
		//Change facing right to facing left or vice versa
		lookingRight = !lookingRight;
		
		Vector3 reverseX = playerGraphics.localScale;
		reverseX.x *= -1;
		
		playerGraphics.localScale = reverseX;
	}

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
	
	public float GetSpeed()
	{
		return speed;
	}

    public void SetTrueSpeed(float newTrueSpeed)
    {
        trueSpeed = newTrueSpeed;
    }

    public float GetTrueSpeed()
    {
        return trueSpeed;
    }
	
	public void SetJumpSpeed(float newJS)
	{
		jumpSpeed = newJS;
	}
	
	public float GetJumpSpeed()
	{
		return jumpSpeed;
	}
}
