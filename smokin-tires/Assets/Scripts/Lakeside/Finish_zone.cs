using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Finish_zone : MonoBehaviour
{
	//victory sound
	public AudioSource audioPlayer;
	public GameManager gameManager;
	public AudioManager audioManager;

	// When an object enters the finish zone, let the
	// game manager know that the current game has ended
	void OnTriggerEnter(Collider other)
	{
		//audioPlayer.Play();
		//FindObjectOfType<GameManager>().FinishedGame();
		//audioManager.
		gameManager.FinishedGame();
	}
}
