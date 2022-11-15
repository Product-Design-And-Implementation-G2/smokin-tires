using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //game modes
    private bool timeTrialMode = false;
    private bool timerMode = true;

    [SerializeField] GameObject finishZone;
    [SerializeField] private new GameObject camera;

    // Place holders to allow connecting to other objects
    public Transform spawnPoint;
    public GameObject player;
    public GameObject[] usersCars;

    //UI
    public GameObject startScreen;
    public GameObject restartGameScreen;
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

    public int collectedWaypoints;

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

        //carIndex = 0;

        if (carIndex == 0)
        { usersCars[0].gameObject.SetActive(true);
            usersCars[1].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 1)
        { usersCars[1].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 2)
        { usersCars[2].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[1].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 3)
        { usersCars[3].gameObject.SetActive(true);
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
       // player.SetActive(false);
    }
     private void UpdateCarIndex()
    {
        //carIndex = playerPreferences.carIndex;
        carIndex = PlayerPreferences.carIndex;
    }


    //This resets to game back to the way it started
    public void StartGame()
    {
        collectedWaypoints = 0;

        //set map camera un-active before starting
        camera.SetActive(false);
        //set player active before starting
        // TODO: Fix the map camera
        //player.SetActive(true);

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
       // PositionPlayer();
        
        //TODO: this needs to be fixed
        //carController.enabled = true;

        //set finish zone un-active
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
        timePassed = 0;
        UpdateCurrentTime();
        UpdateScoreboard();
        //isRunning = false;
        //isFinished = true;
        //carController.enabled = false;
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

        // If the player has failed the time challenge
        if (playerFailed)
        {
            GUI.Box(new Rect(Screen.width / 2 - 90, 150, 180, 70), "You failed");
            GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Coins collected:");
            GUI.Label(new Rect(Screen.width / 2 - 30, 470, 110, 70), coinAmount.ToString());
        }
        // If the game is running, show the current time in time trial mode
        else if (isRunning && timeTrialMode)
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Time left");
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 100, 20, 30), ((int)timeLeft).ToString());
            //Box for coin collecting
            GUI.Box(new Rect(Screen.width / 2 - -360, Screen.height - 115, 130, 55), "Coins collected");
            GUI.Label(new Rect(Screen.width / 2 - -400, Screen.height - 95, 40, 30), coinAmount.ToString());
        }
        // If the game is running, show the current time in timer mode
        else if (isRunning && timerMode)
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Player time");
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 100, 20, 30), ((int)timePassed).ToString());
            //Box for coin collecting
            GUI.Box(new Rect(Screen.width / 2 - -360, Screen.height - 115, 130, 55), "Coins collected");
            GUI.Label(new Rect(Screen.width / 2 - -400, Screen.height - 95, 40, 30), coinAmount.ToString());
        }

    }
}