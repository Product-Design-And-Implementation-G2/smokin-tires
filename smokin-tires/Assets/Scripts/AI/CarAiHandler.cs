    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAiHandler : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints };
    public AIMode aiMode;

    //local variables
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    //components
    CarController carController;

    //Awake is called when the script isntance is being loaded.
    private void Awake()
    {
       carController = GetComponent<CarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch(aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;
        }


        //inputVector.x = 1.0f;
        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f;
        //TurnTowardTarget();
        //carController.GoForward();

        //Send the input to the car controller
        carController.SetInputVector(inputVector);


    }

    private void FollowPlayer()
    {
        if(targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
        
    }

    float TurnTowardTarget()
    {
        Vector2 VectorToTarget = targetPosition - transform.position;
        VectorToTarget.Normalize();

        //calculate angle towards the target
        float angleToTarget = Vector2.SignedAngle(transform.up, VectorToTarget);
        angleToTarget *= -1;

        //sample text
        float steerAmount = angleToTarget / 45.0f;

        //clamp steering to between -1 and 1
        steerAmount = Math.Clamp(steerAmount, -1.0f, 1.0f);
        Debug.Log(steerAmount);
        return steerAmount;
    }
}
