using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WoodlandLapSystem : MonoBehaviour
{
    public int TotalLaps;
    [SerializeField] public int CurrentLaps;
    public bool isPlayer;

    public float minTimeBetweenLaps;
    public float timeBetweenLaps;
    public bool CanDoLaps = false;

    public bool isTriggered;
    public WoodlandGameManager gameManager;

    public float timeCounter;


    private void Start()
    {
        timeBetweenLaps = minTimeBetweenLaps;
    }
    void Update()
    {
        timeBetweenLaps -= Time.deltaTime;
        if (timeBetweenLaps < 0)
        {
            timeBetweenLaps = 0;
        }

        timeCounter += Time.deltaTime;
    }
    private void IncrementLaps()
    {
        if (timeCounter > 0)
        {
            CurrentLaps++;
        }
    }

    private void PlayerIncrementLaps()
    {
        if (timeBetweenLaps <= 0)
        {
            CurrentLaps++;
            gameManager.FinishLap();
            timeBetweenLaps = minTimeBetweenLaps;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lap")
        {
            if (isTriggered == false && CurrentLaps != TotalLaps)
            {
                if (isPlayer)
                {
                    PlayerIncrementLaps();
                    isTriggered = true;
                }
                else
                {
                    IncrementLaps();
                    isTriggered = true;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Lap")
        {
            timeCounter = 0;
            isTriggered = false;
            //if (CurrentLaps == TotalLaps)
            //{
            Debug.Log("Game over");
            gameManager.FinishedGame();
            //}
        }
    }
}

