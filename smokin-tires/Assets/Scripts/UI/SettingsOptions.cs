using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsOptions : MonoBehaviour
{
    //TODO: Change name of the script to SettingsMenu
    public AudioMixer audioMixer;
    public AudioMixer audioMixer_music;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;
    public Slider slider_cars;
    public Slider slider_music;


    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MasterVolume", slider_music.value);
        PlayerPrefs.SetFloat("CarMixer", slider_cars.value);
    }
    private void Start()
    {
        slider_music.value = PlayerPrefs.GetFloat("MasterVolume", slider_music.value);
        slider_cars.value = PlayerPrefs.GetFloat("CarMixer", slider_cars.value);

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
    public void QuitGame()
    {
        Debug.Log("Game successfully closed");
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("CarMixer", volume);
        audioMixer.SetFloat("CarMixer", Mathf.Log10(volume) * 70);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer_music.SetFloat("MasterVolume", Mathf.Log10(volume) * 70);
    }
    public void SetQuality(int qualityIndex)
    {
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
}
