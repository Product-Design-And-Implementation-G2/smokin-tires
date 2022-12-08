using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class WoodlandGameManager : MonoBehaviour
{
    [SerializeField] GameObject finishZone;
    [SerializeField] private GameObject camera;

    // Place holders to allow connecting to other objects
    //public Transform spawnPoint;

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    //public Transform spawnPoint3;
    //public Transform spawnPoint4;

    //all ai car compteritors
    //public GameObject carBlue;
    public GameObject carYellow;
    //public GameObject carRed;

    //ai car waypoint targets
    //public Transform BluesTarget;
    public Transform YellowsTarget;
    //public Transform RedsTarget;

    public GameObject player;
    public GameObject[] usersCars;
    public Transform waypoint1;
    public Transform waypoint2;
    public Transform waypoint3;

    //UI
    public GameObject startScreen;
    public GameObject restartGameScreen;
    public GameObject tabScreen;
    public TMP_Text lapTimeText;
    public TMP_Text currentTimeText;
    public TMP_Text coinAmountText;
    public TMP_Text scoreboardText;

    // Flags that control the state of the game
    private float timePassed;
    public bool isRunning = false;

    //players collected coins
    public int coinAmount;

    //waypoints
    public GameObject[] waypoints;
    public int collectedWaypoints;
    public int currentWaypoint;

    //used for determening whether player is going slow enough to respawn
    public int currentCarSpeed;

    //car index
    public int carIndex;

    //lapCheckpoint spawning variables
    public bool isSpawned = false;
    public float TimeAfter = 20f;
    public GameObject FinishObject;

    public TMP_Text gameFinishText;

    public WoodlandCountdown countdown;
    public TMP_Text countdownText;

    void Start()
    {
        //choose the right car with carindex
        UpdateCarIndex();

        //set cinematic camera audiolistener off
        //camera.GetComponent<AudioListener>().enabled = false;

        //set current spawn waypoint at spawn
        currentWaypoint = 0;

        if (carIndex == 0)
        {
            player = usersCars[0];
            usersCars[0].gameObject.SetActive(true);
            usersCars[1].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 1)
        {
            player = usersCars[1];
            usersCars[1].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 2)
        {
            player = usersCars[2];
            usersCars[2].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[1].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 3)
        {
            player = usersCars[3];
            usersCars[3].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[1].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
        }

        //Tell Unity to allow character controllers to have their position set directly. This will enable our respawn to work
        Physics.autoSyncTransforms = true;

        //set finish zone un-active
        finishZone.SetActive(false);

        //set map camera active before starting
        camera.SetActive(true);

        //set player un-active before starting
        player.SetActive(false);
    }
    private void UpdateCarIndex()
    {
        //carIndex = playerPreferences.carIndex;
        carIndex = PlayerPreferences.carIndex;
    }

    //This resets to game back to the way it started
    public void StartGame()
    {

        //play the countdown sequence
        
        countdown.GetComponent<WoodlandCountdown>().enabled = true;

        FinishObject.SetActive(false);
        isSpawned = false;

        //set all cars current laps to 0 in the lap system script
        usersCars[carIndex].GetComponent<WoodlandLapSystem>().CurrentLaps = 0;
        carYellow.GetComponent<WoodlandLapSystem>().CurrentLaps = 0;

        positionAllCarsToStart();

        //set map camera un-active before starting
        camera.SetActive(false);
        //set player active before starting
        // TODO: Fix the map camera
        player.SetActive(true);

        //timer mode
        timePassed = 0f;

        //set bool variables to wanted states
        //isRunning = true;

        // Move the player to the spawn point, and allow it to move.
        //PositionPlayer();

        //set finish zone un-active
        //finishZone.SetActive(false);
    }

    private void positionAllCarsToStart()
    {
        PositionPlayer();

        //player.transform.position = spawnPoint1.position;
        //player.transform.rotation = spawnPoint1.rotation;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        carYellow.transform.position = spawnPoint2.position;
        carYellow.transform.rotation = spawnPoint2.rotation;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        //carRed.transform.position = spawnPoint3.position;
        //carRed.transform.rotation = spawnPoint3.rotation;
        //carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        //carBlue.transform.position = spawnPoint4.position;
        //carBlue.transform.rotation = spawnPoint4.rotation;
        //carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public void FinishLap()
    {
        UpdateScoreboard();
        timePassed = 0;
        UpdateCurrentTime();
        NewLap();
    }
    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public void NewLap()
    {
        collectedWaypoints = 0;
        //EnableWaypoints();
        currentWaypoint = 0;
        finishZone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            UpdateCurrentTime();
            timePassed += Time.deltaTime;

            //if player presses down tab, they can see the current lap times
            if (Input.GetKey("tab"))
            {
                tabScreen.SetActive(true);
            }
            else
            {
                tabScreen.SetActive(false);
            }

            TimeAfter = TimeAfter - Time.deltaTime;
            if (TimeAfter <= 0 && isSpawned == false)
            {
                Debug.Log("Spawn lap object");
                FinishObject.SetActive(true);
                TimeAfter = 20f;
                isSpawned = true;
            }
        }

        //check if player has all 3 necessary waypoints to enter the finish zone
        if (collectedWaypoints == 3)
        {
            finishZone.SetActive(true);
        }
    }

    //Runs when the player needs to be positioned back at the spawn point
    public void PositionPlayer()
    {
        player.transform.position = spawnPoint1.position;
        player.transform.rotation = spawnPoint1.rotation;
        Debug.Log("Player positioned");
    }

    // Runs when the player enters the finish zone
    public void FinishedGame()
    {
        restartGameScreen.SetActive(true);
        if (usersCars[carIndex].GetComponent<WoodlandLapSystem>().CurrentLaps == 1)
        {
            gameFinishText.text = "You won!!!";
        }
        else
        {
            gameFinishText.text = "You lost!!!";
        }
        countdown.GetComponent<WoodlandCountdown>().enabled = false;
        Time.timeScale = 0.25f;
    }

    //unused in the newer lapSystem
    private void EnableWaypoints()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].SetActive(true);
        }
    }

    public void GetCoins(int receivedCoinAmount)
    {
        coinAmount = receivedCoinAmount;
    }

    public void WaypointCount()
    {
        collectedWaypoints++;
        currentWaypoint++;
    }

    public void UpdateLapTime()
    {
        //lapTimeText.text += timePassed.ToString();
        //lapTimeText.text = timePassed.ToString() + "\n";
    }
    public void UpdateScoreboard()
    {
        scoreboardText.text = scoreboardText.text + timePassed.ToString() + "\n";
    }
    public void UpdateCurrentTime()
    {
        currentTimeText.text = timePassed.ToString();
    }
    public void UpdateCoinAmountText()
    {
        // TODO: make this limit the number of values displayed to 5. Also 
        coinAmountText.text = coinAmount.ToString();
    }

    public void RespawnAtWaypoint()
    {
        //currentCarSpeed = player.GetComponent<PrometeoCarController>().ReturnCarSpeed();
        //Debug.Log(currentCarSpeed);

        if (currentWaypoint == 0)
        {
            PositionPlayer();
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else if (currentWaypoint == 1)
        {
            player.transform.position = waypoint1.position;
            player.transform.rotation = waypoint1.rotation;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else if (currentWaypoint == 2)
        {
            player.transform.position = waypoint2.position;
            player.transform.rotation = waypoint2.rotation;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else if (currentWaypoint == 3)
        {
            player.transform.position = waypoint3.position;
            player.transform.rotation = waypoint3.rotation;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}