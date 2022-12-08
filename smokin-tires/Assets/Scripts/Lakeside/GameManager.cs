using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject finishZone;
    [SerializeField] private GameObject camera;

    // Place holders to allow connecting to other objects
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;

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
    public TMP_Text gameFinishText;

    public AudioSource victorySound;

    // Flags that control the state of the game
    private float timePassed;
    public bool isRunning = false;
    
    //players collected coins
    public int coinAmount;

    //waypoints
    //public GameObject[] waypoints;
    public int collectedWaypoints;
    public int currentWaypoint;

    //used for determening whether player is going slow enough to respawn
    public int currentCarSpeed;

    //car index
    public int carIndex;

    //all the cars totalwaypoints
    public GameObject carBlue;
    public GameObject carYellow;
    public GameObject carRed;

    public GameObject FinishObject;

    public Transform BluesTarget;
    public Transform YellowsTarget;
    public Transform RedsTarget;

    public bool isSpawned = false;
    public float TimeAfter = 20f;

    public Transform firstAIWaypoint;

    public LakeCountdown countdown;
    public TMP_Text countdownText;

    public Transform currentPlayerRespawnPoint;

    void Start()
    {
        try
        {
            FindObjectOfType<AudioManager2>().Stop("MenuTheme");
        }
        catch (InvalidCastException e)
        {
            Debug.Log(e);
        }

        //stop menu music

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
        //set all cars target position to null
        //SetAICarPositionsToNull();
        
        //start music
        try
        {
            FindObjectOfType<AudioManager2>().Play("Lakeside_bgm");
        }
        catch (InvalidCastException e)
        {
            Debug.Log("Ran into an error when trying to play lakeside_bgm");
            Debug.Log(e);
        }

        countdown.GetComponent<LakeCountdown>().enabled = true;

        //set finishpoint unactive
        Debug.Log("set active false to finish point");
        FinishObject.SetActive(false);
        isSpawned = false;

        Debug.Log("Start game was run");
        //set waypoint collected amount to 0 and enable disabled waypoints
        collectedWaypoints = 0;

        //set all cars current laps to 0 in the lap system script
        usersCars[carIndex].GetComponent<LapSystem>().CurrentLaps = 0;
        carBlue.GetComponent<LapSystem>().CurrentLaps = 0;
        carYellow.GetComponent<LapSystem>().CurrentLaps = 0;
        carRed.GetComponent<LapSystem>().CurrentLaps = 0;

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

        //clear the scoreboard tab for a new game
        scoreboardText.text = "";
    }
    
    //TODO: couldnt get this to work
    private void SetAICarPositionsToNull()
    {
        BluesTarget.position = firstAIWaypoint.position;
        BluesTarget.rotation = firstAIWaypoint.rotation;

        YellowsTarget.position = firstAIWaypoint.position;
        YellowsTarget.rotation = firstAIWaypoint.rotation;

        RedsTarget.position = firstAIWaypoint.position;
        RedsTarget.rotation = firstAIWaypoint.rotation;
    }

    private void positionAllCarsToStart()
    {
        PositionPlayer();
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        carBlue.transform.position = spawnPoint2.position;
        carBlue.transform.rotation = spawnPoint2.rotation;
        carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        carYellow.transform.position = spawnPoint3.position;
        carYellow.transform.rotation = spawnPoint3.rotation;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        carRed.transform.position = spawnPoint4.position;
        carRed.transform.rotation = spawnPoint4.rotation;
        carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public void NewLap()
    {
        collectedWaypoints = 0;
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            UpdateCurrentTime();
            TimeAfter = TimeAfter - Time.deltaTime;
        }
        // Add time to the clock if the game is running
        if (isRunning)
        {
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
        } 

        if (TimeAfter <= 0 && isSpawned == false)
        {
            Debug.Log("Spawn lap object");
            FinishObject.SetActive(true);
            TimeAfter = 20f;
            isSpawned = true;
        }
    }
    public void RespawnPlayer()
    {
        player.transform.position = currentPlayerRespawnPoint.position;
        player.transform.rotation = currentPlayerRespawnPoint.rotation;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("Player respawned");
    }


    //Runs when the player needs to be positioned back at a respawn point
    public void PositionPlayer()
    {
        player.transform.position = spawnPoint1.position;
        player.transform.rotation = spawnPoint1.rotation;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("Player positioned");
    }
    public void PositionBlue()
    {
        carBlue.transform.position = BluesTarget.position;
        carBlue.transform.rotation = BluesTarget.rotation;
        carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //carBlue.

        Debug.Log("carBlue positioned");
    }
    public void PositionYellow()
    {
        carYellow.transform.position = YellowsTarget.position;
        carYellow.transform.rotation = YellowsTarget.rotation;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("carYellow positioned");
    }
    public void PositionRed()
    {
        carRed.transform.position = RedsTarget.position;
        carRed.transform.rotation = RedsTarget.rotation;
        carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("carRed positioned");
    }

    // Runs when the player enters the finish zone
    public void FinishedGame()
    {
        restartGameScreen.SetActive(true);
        if (usersCars[carIndex].GetComponent<LapSystem>().CurrentLaps == 3)
        {
            gameFinishText.text = "You won!!!";
            victorySound.Play();
        } else
        {
            gameFinishText.text = "You lost!!!";
        }
        Time.timeScale = 0.25f;
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

    public void GetCoins(int receivedCoinAmount)
    {
        coinAmount = receivedCoinAmount;
    }

    public void WaypointCount()
    {
        collectedWaypoints++;
        currentWaypoint++;
    }
    
    public void UpdateScoreboard()
    {
        scoreboardText.text = scoreboardText.text + timePassed.ToString("F2") + "\n";
    }
    public void UpdateCurrentTime()
    {
        currentTimeText.text = timePassed.ToString("F2");
    }
    public void UpdateCoinAmountText()
    {
        // TODO: make this limit the number of values displayed to 5. Also 
        coinAmountText.text = coinAmount.ToString();
    }
    // TODO: couldnt get this to work with updated checkpoint/lap system
    public void RespawnAtWaypoint()
    {
        if (currentWaypoint == 0)
        {
            PositionPlayer();
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        } else if (currentWaypoint == 1) {
            player.transform.position = waypoint1.position;
            player.transform.rotation = waypoint1.rotation;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else if (currentWaypoint == 2) {
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