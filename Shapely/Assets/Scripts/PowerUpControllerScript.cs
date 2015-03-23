using UnityEngine;
using System.Collections;

public class PowerUpControllerScript : MonoBehaviour {
	//Only used when powerups are on!!!
	GameObject[] spawnPoints;
	float currentTime = 5.0f;
	float nextSpawnTime = 5.0f;

	void Awake()
	{
		spawnPoints = GameObject.FindGameObjectsWithTag("PowerUpSpawn");
		if(spawnPoints.Length == 0)
		{
			Debug.LogError("Power up spawn points were not found!!!");
		}

		InvokeRepeating("SpawnPowerUp", 5.0f, 1.0f);
	}

	void SpawnPowerUp()
	{
		if(currentTime == nextSpawnTime)
		{
			int spawnChoice = Random.Range(0, spawnPoints.Length);
			GameObject newPowerUp = Instantiate(Resources.Load("PowerUp"), spawnPoints[spawnChoice].transform.position, spawnPoints[spawnChoice].transform.rotation) as GameObject;
			//int type = Random.Range (1,4);
			int type = 1;
			newPowerUp.GetComponent<PowerUpScript>().SetType(type);
			newPowerUp.GetComponent<PowerUpScript>().SetSprite("Power"+type);

			nextSpawnTime = currentTime += Random.Range(5.0f, 11.0f);
		}

		currentTime ++;
	}
}
