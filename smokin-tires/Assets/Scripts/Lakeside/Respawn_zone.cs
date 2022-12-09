using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_zone : MonoBehaviour
{
	[SerializeField] LakeGameManager gameManager;

	// Triggers when the player enters the water
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Playercar")
		{ 
			gameManager.RespawnPlayer();
		} else if (other.gameObject.tag == "Bluecar")
		{
			gameManager.PositionBlue();
		}
		else if (other.gameObject.tag == "Yellowcar")
		{
			gameManager.PositionYellow();
		}
		else if (other.gameObject.tag == "Redcar")
		{
			gameManager.PositionRed();
		}

	}
}

