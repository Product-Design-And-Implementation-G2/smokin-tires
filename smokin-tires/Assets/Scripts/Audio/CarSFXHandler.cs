using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class CarSFXHandler : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource high_on;
    public AudioSource tiresScreech;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;

    CarController CarController;

    void Awake()
    {
        CarController = GetComponentInParent<CarController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        
    }

    void UpdateEngineSFX()
    {
        float velocityMagnitude = CarController.GetVelocityMagnitude();
        float desiredEngineVolume = velocityMagnitude * 0.05f;
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);
        high_on.volume = Mathf.Lerp(high_on.volume, desiredEngineVolume, Time.deltaTime * 10);
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        high_on.pitch = Mathf.Lerp(high_on.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

  

}

