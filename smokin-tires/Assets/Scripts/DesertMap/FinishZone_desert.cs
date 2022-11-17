using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone_desert : MonoBehaviour
{
	public DesertGameManager gameManager;

	// When an object enters the finish zone, let the
	// game manager know that the current game has ended
	void OnTriggerEnter(Collider other)
	{
		//audioPlayer.Play();
		//FindObjectOfType<GameManager>().FinishedGame();
		gameManager.FinishedGame();
		this.enabled = false;
	}
}
