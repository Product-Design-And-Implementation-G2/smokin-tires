using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSandMap : MonoBehaviour
{
    // Place holders to allow connecting to other objects
    public Transform spawnPoint;
    [SerializeField] GameObject finishZone;
    public GameObject player;


    //ai car
    public int AIwaypointCount;
    public Transform[] AI_waypoints;

    // Use this for initialization
    void Start()
    {
        //Tell Unity to allow character controllers to have their position set directly. This will enable our respawn to work
        Physics.autoSyncTransforms = true;

        //set finish zone un-active
        finishZone.SetActive(false);

        StartGame();

    }

    //This resets to game back to the way it started
    private void StartGame()
    {
        // Move the player to the spawn point, and allow it to move.
        PositionPlayer();
        //carController.enabled = true;

        ReturnCurrentWaypoint();
    }

    //Runs when the player needs to be positioned back at the spawn point
    public void PositionPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }

    // Runs when the player enters the finish zone
    public void FinishedGame()
    {
        AIwaypointCount = 0;
        finishZone.SetActive(false);
        AI_waypoints[AIwaypointCount].gameObject.SetActive(true);
    }

    public void IncrementAIWaypointCount()
    {
        if(AIwaypointCount < 9)
        {
            AIwaypointCount++;
            ReturnCurrentWaypoint();
        }
    }
    public Transform ReturnCurrentWaypoint()
    {
        if(AIwaypointCount == 8)
        {
            return finishZone.transform;
        } else {
            return AI_waypoints[AIwaypointCount];
        }
    }

    public void ActivateNextAIWaypoint()
    {
        if(AIwaypointCount == 8)
        {
            finishZone.SetActive(true);
        } else
        {
            AI_waypoints[AIwaypointCount].gameObject.SetActive(true);
        }
    }
}

