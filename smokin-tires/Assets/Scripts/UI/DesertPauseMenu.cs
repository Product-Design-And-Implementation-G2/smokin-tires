using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesertPauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject optionsMenuUI;

    public GameObject speedometerUI;

    public GameObject scoreGUI;

    public DesertGameManager gameManager;

    public Animator transition;
    public float transitionTime = 1f;

    void Update()
    {
        if (gameManager.isRunning)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
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
        //stop music
        FindObjectOfType<AudioManager2>().Stop("DesertTheme");
        //start menu music
        FindObjectOfType<AudioManager2>().Play("MenuTheme");

        //TODO: Create a variable (don't hardcode this in)
        Time.timeScale = 1f;
        //SceneManager.LoadScene("UI");
        pauseMenuUI.SetActive(false);
        scoreGUI.SetActive(false);
        speedometerUI.SetActive(false);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 2));
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

