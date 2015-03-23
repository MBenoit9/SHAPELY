using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

	private int type;

	public void SetType(int newType)
	{
		type = newType;
	}

	public int GetType()
	{
		return type;
	}

	public void SetSprite(string spriteAddress)
	{
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteAddress);
	}
}
