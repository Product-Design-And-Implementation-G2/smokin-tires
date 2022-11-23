using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZoneWoodland : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	// Triggers when the player enters the water
	void OnTriggerEnter(Collider other)
	{
		gameManager.PositionPlayer();
	}
}

