using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //game modes
    private bool timeTrialMode = false;
    private bool timerMode = true;

    [SerializeField] GameObject finishZone;
    [SerializeField] private GameObject camera;

    // Place holders to allow connecting to other objects
    public Transform spawnPoint;
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
    private float timeLeft;
    private float timePassed;
    private bool isRunning = false;
    private bool isFinished = false;
    private bool playerFailed = false;

    //coin collection
    public int coinAmount;

    //waypoints
    public GameObject[] waypoints;
    public int collectedWaypoints;
    public int currentWaypoint;

    // So that we can access the player's controller from this script
    private PrometeoCarController carController;

    //car index
    public int carIndex;

    // Use this for initialization
    void Start()
    {
        Debug.Log("carindex " + PlayerPreferences.carIndex);
        //choose the right car with carindex
        UpdateCarIndex();

        //set cinematic camera audiolistener off
        camera.GetComponent<AudioListener>().enabled = false;

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

        // Finds the Car Controller script on the Player
        //carController = player.GetComponent<PrometeoCarController>();

        // Disables controls before the game starts.
        //TODO: Fix erros with this carController when launching the game
        //carController.enabled = false;

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
        Debug.Log("Start game was run");
        //set waypoint collected amount to 0 and enable disabled waypoints
        collectedWaypoints = 0;
        EnableWaypoints();

        //set map camera un-active before starting
        camera.SetActive(false);
        //set player active before starting
        // TODO: Fix the map camera
        player.SetActive(true);

        // TODO: Make these game modes into their own methods

        //time trial game mode
        timeLeft = 30;

        //timer mode
        timePassed = 0;

        //set bool variables to wanted states
        isRunning = true;
        isFinished = false;
        playerFailed = false;

        // Move the player to the spawn point, and allow it to move.
        PositionPlayer();
        
        //TODO: this needs to be fixed
        //carController.enabled = true;

        //set finish zone un-active
        finishZone.SetActive(false);
    }
    public void NewLap()
    {
        collectedWaypoints = 0;
        EnableWaypoints();
        currentWaypoint = 0;
        finishZone.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            UpdateCurrentTime();
        }
        // Add time to the clock if the game is running
        if (isRunning && timerMode)
        {
            timePassed += Time.deltaTime;
        } 
        else if (isRunning && timeTrialMode)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
               PlayerFailed();
            }
        }

        //if player presses down tab, they can see the current lap times
        if (Input.GetKey("tab"))
        {
            tabScreen.SetActive(true);
        } else
        {
            tabScreen.SetActive(false);
        }

        if (Input.GetKey("p"))
        {
            RespawnAtWaypoint();
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
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
        Debug.Log("Player positioned");
    }

    // Runs when the player enters the finish zone
    public void FinishedGame()
    {
        UpdateLapTime();
        UpdateScoreboard();
        timePassed = 0;
        UpdateCurrentTime();

        //isRunning = false;
        //isFinished = true;
        //carController.enabled = false;

        NewLap();
    }

    public void FinishedGame2()
    {
        // TODO: make this into a function that runs when player has completed 5 laps
    }

    private void EnableWaypoints()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].SetActive(true);
        }
    }

    // Runs when the player runs out of time
    public void PlayerFailed()
    {
        isRunning = false;
        playerFailed = true;
        //carController.enabled = false;
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
        lapTimeText.text = timePassed.ToString() + "\n";
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
        if(currentWaypoint == 0)
        {
            PositionPlayer();
        } else if (currentWaypoint == 1) {
            player.transform.position = waypoint1.position;
            player.transform.rotation = waypoint1.rotation;
        }
        else if (currentWaypoint == 2) {
            player.transform.position = waypoint2.position;
            player.transform.rotation = waypoint2.rotation;
        }
        else if (currentWaypoint == 3)
        {
            player.transform.position = waypoint3.position;
            player.transform.rotation = waypoint3.rotation;
        }
        
    }

    //This section creates a simple Graphical User Interface (GUI)
    void OnGUI()
    {
        if (!isRunning)
        {
            string message;

            if (isFinished) { 
                //message = "Click or Press Enter to Play Again"; 

            }
            //else { message = "Click or Press Enter to Play"; }

            //Define a new rectangle for the UI on the screen
            //Rect startButton = new Rect(Screen.width / 2 - 120, Screen.height / 2, 240, 30);
            /*
            if (GUI.Button(startButton, message) || Input.GetKeyDown(KeyCode.Return))
            {
                //resets movement
                // TODO: freeze vehicle velocity or slow it down dramatically. It must be zero when player is spawned back at a spawnpoint.
                //start the game if the user chooses to play
                StartGame();
            }*/
        }

        // If the player finished the game in time, show their time
       /*
        * if (isFinished)
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, 80, 220, 60), "YOU WIN! Your Time was:");
            GUI.Label(new Rect(Screen.width / 2 - 0, 100, 50, 50), (((int)timePassed).ToString()));
            GUI.Box(new Rect(Screen.width / 2 - 110, 150, 220, 70), "Coins collected:");
            GUI.Label(new Rect(Screen.width / 2 - 0, 170, 110, 70), coinAmount.ToString());
        }*/
       if( isFinished)
        {
            //restartGameScreen.SetActive(true);

                //public GameObject startScreen;
                // public GameObject restartGameScreen;
        }

       //TODO: make a new ui extension of this
        // If the player has failed the time challenge
        if (playerFailed)
        {
            GUI.Box(new Rect(Screen.width / 2 - 90, 150, 180, 70), "You failed");
            GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Coins collected:");
            GUI.Label(new Rect(Screen.width / 2 - 30, 470, 110, 70), coinAmount.ToString());
        }
    }
}