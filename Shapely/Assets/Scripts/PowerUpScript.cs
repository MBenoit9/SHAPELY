using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

	private int type;

	public void SetPowerType(int newType)
	{
		type = newType;
	}

	public int GetPowerType()
	{
		return type;
	}

	public void SetSprite(string spriteAddress)
	{
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteAddress);
	}
}
