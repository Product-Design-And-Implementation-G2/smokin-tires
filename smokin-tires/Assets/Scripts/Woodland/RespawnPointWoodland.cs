using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZoneWoodland : MonoBehaviour
{
	[SerializeField] WoodlandGameManager gameManager;

	// Triggers when the player enters the water
	void OnTriggerEnter(Collider other)
	{
		gameManager.PositionPlayer();
	}
}

