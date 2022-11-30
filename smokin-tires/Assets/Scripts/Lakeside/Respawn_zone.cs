using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_zone : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	// Triggers when the player enters the water
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Playercar")
		{ 
			gameManager.PositionPlayer();
			Debug.Log("Playercar");
		} else if (other.gameObject.tag == "Bluecar")
		{
			//Debug.Log("Bluecar");
			gameManager.PositionBlue();
		}
		else if (other.gameObject.tag == "Yellowcar")
		{
			//gameManager.PositionA();
			//Debug.Log("Yellowcar");
			gameManager.PositionYellow();
		}
		else if (other.gameObject.tag == "Redcar")
		{
			//gameManager.PositionA();
			//Debug.Log("Redcar");
			gameManager.PositionRed();
		}

	}
}

