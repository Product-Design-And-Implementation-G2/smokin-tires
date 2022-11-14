using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;

    [SerializeField] private GameObject mainMenuCamera2;
    [SerializeField] private GameObject garageCamera2;

    //[SerializeField] private new GameObject camera;

    private void Start()
    {
        //set up cameras
        mainMenuCamera2.SetActive(true);
        garageCamera2.SetActive(false);

        //set quality settings
        int qualityLevel = QualitySettings.GetQualityLevel();
        graphicsDropdown.value = qualityLevel;

        resolutions = Screen.resolutions;
        //clear resolution list
        resolutionDropdown.ClearOptions();

        //sorts the resolutions array into a string list and inputs it to the dropdown menu
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //checks which resolution the user has
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Game successfully closed");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("BGMMixer", volume);
    } 
    public void SetQuality(int qualityIndex)
    {
        //TODO: Set the graphics quality to the user's quality at first
        //Debug.Log(qualityIndex.ToString());
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SwapToCarCamera()
    {
        mainMenuCamera2.SetActive(false);
        garageCamera2.SetActive(true);
        mainMenuCamera2.GetComponent<AudioListener>().enabled = false;
        garageCamera2.GetComponent<AudioListener>().enabled = true;
    }
    public void SwapToMenuCamera()
    {
        mainMenuCamera2.SetActive(true);
        garageCamera2.SetActive(false);
        mainMenuCamera2.GetComponent<AudioListener>().enabled = true;
        garageCamera2.GetComponent<AudioListener>().enabled = false;
    }
}
