using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSandMap : MonoBehaviour
{
    // Place holders to allow connecting to other objects
    public Transform spawnPoint;
    public GameObject player;

    // Flags that control the state of the game
    //private float timeChallenge = 2000;
    private float timeLeft;
    private float timePassed;
    private bool isRunning = false;
    private bool isFinished = false;
    private bool playerFailed = false;

    //coin collection
    // [SerializeField] CollectableCoins coins;
    private int coinAmount;

    // So that we can access the player's controller from this script
    //private CarController carController;

    // Use this for initialization
    void Start()
    {
        //Tell Unity to allow character controllers to have their position set directly. This will enable our respawn to work
        Physics.autoSyncTransforms = true;

        // Finds the Car Controller script on the Player
        //carController = player.GetComponent<CarController>();

        // Disables controls before the game starts.
        //carController.enabled = false;

    }

    //This resets to game back to the way it started
    private void StartGame()
    {
        timeLeft = 30;
        isRunning = true;
        isFinished = false;
        playerFailed = false;
        timePassed = 0;

        // Move the player to the spawn point, and allow it to move.
        PositionPlayer();
        //carController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Add time to the clock if the game is running
        if (isRunning)
        {
            timeLeft -= Time.deltaTime;
            timePassed += Time.deltaTime;

        }
        if (timeLeft < 0 )
        {
            PlayerFailed();
        }
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
        isRunning = false;
        isFinished = true;
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
        // coinAmount = FindObjectOfType<CollectableCoins>().GetCoinAmount();
        coinAmount = receivedCoinAmount;
    }

    //This section creates the Graphical User Interface (GUI)
    void OnGUI()
    {
        if (!isRunning)
        {
            string message;

            if (isFinished){ message = "Click or Press Enter to Play Again";}
            else{ message = "Click or Press Enter to Play";}

            //Define a new rectangle for the UI on the screen
            Rect startButton = new Rect(Screen.width / 2 - 120, Screen.height / 2, 240, 30);

            if (GUI.Button(startButton, message) || Input.GetKeyDown(KeyCode.Return))
            {
                //start the game if the user chooses to play
                StartGame();
            }
        }

        // If the player finished the game in time, show their time
        if (isFinished)
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, 80, 220, 60), "YOU WIN! Your Time was:");
            GUI.Label(new Rect(Screen.width / 2 - 0, 100, 50, 50), (((int)timePassed).ToString()));
            GUI.Box(new Rect(Screen.width / 2 - 110, 150, 220, 70), "Coins collected:");
            GUI.Label(new Rect(Screen.width / 2 - 0, 170, 110, 70), coinAmount.ToString());
        }
        // If the player has failed the time challenge
        else if (playerFailed)
        {
            GUI.Box(new Rect(Screen.width / 2 - 90, 150, 180, 70), "You failed");
            GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Coins collected:");
            GUI.Label(new Rect(Screen.width / 2 - 30, 470, 110, 70), coinAmount.ToString());
        }
        // If the game is running, show the current time
        else if (isRunning)
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Time left");
            GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 100, 20, 30), ((int)timeLeft).ToString());
            //Box for coin collecting
            GUI.Box(new Rect(Screen.width / 2 - -360, Screen.height - 115, 130, 55), "Coins collected");
            GUI.Label(new Rect(Screen.width / 2 - -400, Screen.height - 95, 40, 30), coinAmount.ToString());
        }

    }
}

