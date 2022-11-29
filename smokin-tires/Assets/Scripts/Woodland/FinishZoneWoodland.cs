using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZoneWoodland : MonoBehaviour
{
	public WoodlandGameManager gameManager;
		
		// When an object enters the finish zone, let the
		// game manager know that the current game has ended
		void OnTriggerEnter(Collider other)
		{
			//audioPlayer.Play();
			//FindObjectOfType<GameManager>().FinishedGame();
			gameManager.FinishedGame();
			this.enabled = false;
			//GameObject.Find("PlayAgainScreen").GetComponent<Canvas>();

		}
	}


