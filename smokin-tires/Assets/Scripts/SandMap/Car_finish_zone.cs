using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_finish_zone : MonoBehaviour
{
	//A reference to the game manager
	public GameManager gameManager2;

	// When an object enters the finish zone, let the
	// game manager know that the current game has ended
	void OnTriggerEnter(Collider other)
	{
		gameManager2.FinishedGame();
	}
}
