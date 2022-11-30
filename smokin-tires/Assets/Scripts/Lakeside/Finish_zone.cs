using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Finish_zone : MonoBehaviour
{
	//victory sound
	public GameManager gameManager;

	// When an object enters the finish zone, let the
	// game manager know that the current game has ended
	void OnTriggerEnter(Collider other)
	{
		gameManager.FinishLap();
		this.enabled = false;
	}
}
