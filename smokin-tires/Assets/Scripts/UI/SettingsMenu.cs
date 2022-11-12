using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;

    public Camera mainMenuCamera;
    public Camera garageCamera;

    private void Start()
    {
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


        //set cameras
        mainMenuCamera.enabled = true;
        garageCamera.enabled = false;
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("carMixer", volume);
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
    public void SwapCamera()
    {
        if(mainMenuCamera.enabled == true)
        {
            garageCamera.enabled = true;
            mainMenuCamera.enabled = false;
        } else if (garageCamera.enabled == true)
        {
            mainMenuCamera.enabled = true;
            garageCamera.enabled = false;
        }
        
    }
}
