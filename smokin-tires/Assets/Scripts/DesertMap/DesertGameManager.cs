using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class DesertGameManager : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    // Place holders to allow connecting to other objects
    //public Transform spawnPoint;

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;

    //all ai car compteritors
    public GameObject carBlue;
    public GameObject carYellow;
    public GameObject carRed;

    //ai car waypoint targets
    public Transform BluesTarget;
    public Transform YellowsTarget;
    public Transform RedsTarget;

    public GameObject player;
    public GameObject[] usersCars;

    //UI
    public GameObject startScreen;
    public GameObject restartGameScreen;
    public GameObject tabScreen;
    public TMP_Text currentTimeText;
    public TMP_Text scoreboardText;

    // Flags that control the state of the game
    private float timePassed;
    public bool isRunning = false;

    //used for determening whether player is going slow enough to respawn
    public int currentCarSpeed;

    //car index
    public int carIndex;

    //lapCheckpoint spawning variables
    public bool isSpawned = false;
    public float TimeAfter = 20f;
    public GameObject FinishObject;

    public TMP_Text gameFinishText;

    public Countdown countdown;
    public TMP_Text countdownText;

    public Transform currentPlayerRespawnPoint;

    void Start()
    {
        //stop menu music
        FindObjectOfType<AudioManager2>().Stop("MenuTheme");
 
        //choose the right car with carindex
        UpdateCarIndex();

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
        //start music
        FindObjectOfType<AudioManager2>().Play("DesertTheme");

        //play the countdown sequence
        //countdown = new Countdown();
        countdown.GetComponent<Countdown>().enabled = true;

        FinishObject.SetActive(false);
        isSpawned = false;

        //set all cars current laps to 0 in the lap system script
        usersCars[carIndex].GetComponent<DesertLapSystem>().CurrentLaps = 0;
        carBlue.GetComponent<DesertLapSystem>().CurrentLaps = 0;
        carYellow.GetComponent<DesertLapSystem>().CurrentLaps = 0;
        carRed.GetComponent<DesertLapSystem>().CurrentLaps = 0;

        positionAllCarsToStart();

        //set map camera un-active before starting
        camera.SetActive(false);
        //set player active before starting
        player.SetActive(true);

        //timer mode
        timePassed = 0;

        // Move the player to the spawn point, and allow it to move.
        PositionPlayer();
    }

    private void positionAllCarsToStart()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        carYellow.transform.position = spawnPoint2.position;
        carYellow.transform.rotation = spawnPoint2.rotation;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carYellow.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        carRed.transform.position = spawnPoint3.position;
        carRed.transform.rotation = spawnPoint3.rotation;
        carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carRed.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        carBlue.transform.position = spawnPoint4.position;
        carBlue.transform.rotation = spawnPoint4.rotation;
        carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        carBlue.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public void FinishLap()
    {
        UpdateScoreboard();
        timePassed = 0;
        UpdateCurrentTime();
    }
    public void ResumeTime()
    {
        Time.timeScale = 1f;
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
    }

    public void RespawnPlayer()
    {
        player.transform.position = currentPlayerRespawnPoint.position;
        player.transform.rotation = currentPlayerRespawnPoint.rotation;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("Player respawned");
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
        if (usersCars[carIndex].GetComponent<DesertLapSystem>().CurrentLaps == 3)
        {
            gameFinishText.text = "You won!!!";
        }
        else
        {
            gameFinishText.text = "You lost!!!";
        }
        countdown.GetComponent<Countdown>().enabled = false;
        Time.timeScale = 0.25f;
    }

    public void UpdateScoreboard()
    {
        scoreboardText.text = scoreboardText.text + timePassed.ToString("F2") + "\n";
    }
    public void UpdateCurrentTime()
    {
        currentTimeText.text = timePassed.ToString("F2");
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
