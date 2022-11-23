using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCameraControlScript : MonoBehaviour
{

    public GameObject attachedVechicle;
    public GameObject cameraFolder;
    public Transform[] camLocation;
    public int locationIndicator = 2;

    [Range(0, 1)] public float smoothTime = 0.5f;

    void Start()
    {
        //this kept producing errors ("tag not found"). I assigned it manually.
        //attachedVechicle = GameObject.FindGameObjectWithTag("NonArcadeCar");
        cameraFolder = attachedVechicle.transform.Find("Camera").gameObject;
        camLocation = cameraFolder.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {/*
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(locationIndicator >= 4 || locationIndicator < 2)
            {
                locationIndicator = 2;
            } else
            {
                locationIndicator++;
            }

            transform.position = camLocation[locationIndicator].position * (1 - smoothTime) + transform.position * smoothTime;
            transform.LookAt(camLocation[1].transform);
        } */


    }
}
