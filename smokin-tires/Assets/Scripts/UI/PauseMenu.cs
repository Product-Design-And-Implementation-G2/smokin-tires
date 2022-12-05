using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject speedometerUI;
    public GameObject scoreGUI;
    public GameManager gameManager;
    public bool isRunning;
    public Animator transition;
    public float transitionTime = 1f;
    //public AudioListener audioListener;

    private void Start()
    {
        isRunning = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isRunning) { 
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
            }
        }
    }

    public void Resume()
    {
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        AudioListener.pause = false;
        //stop menu music
        FindObjectOfType<AudioManager2>().Stop("Lakeside_bgm");
        //start music
        FindObjectOfType<AudioManager2>().Play("MenuTheme");
        //Debug.Log("Loading menu");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        scoreGUI.SetActive(false);
        speedometerUI.SetActive(false);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        transition.SetTrigger("Start");
        //delay code for animation
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(levelIndex);
    }

}
