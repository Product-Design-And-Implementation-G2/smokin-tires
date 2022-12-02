using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    private void Awake()
    {
      if(Instance == null)
        {
            Instance= this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("BGMMixer", volumeSlider.value);
    }

}
