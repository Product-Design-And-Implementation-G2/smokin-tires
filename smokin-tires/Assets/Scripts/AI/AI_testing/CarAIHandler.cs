using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIHandler : MonoBehaviour
{
    [SerializeField] private Transform targetPositionTransform;
    //stores the referfence to the waypoint system this object will use
    [SerializeField] private Path nodes;
    private Transform currentWaypoint;

    private CarController2 carController;    
    private Vector3 targetPosition;
    public GameManagerSandMap gameManager;


    private void Awake()
    {
        carController = GetComponent<CarController2>();
    }


    private void Update()
    {
        Sensors();
        //Debug.Log(carController.GetCarSpeed());
        //SetTargetPosition(targetPositionTransform.position);
        SetTargetPosition(gameManager.ReturnCurrentWaypoint().position);
        float forwardAmount = 0f;
        float turnAmount = 0f;

        float reachedTargetDistance = 7f;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if(distanceToTarget > reachedTargetDistance)
        {
            //still too far, keep going

            Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;

            float dot = Vector3.Dot(transform.forward, dirToMovePosition);

            //Debug.Log(dot);
            if (dot > 0)
            {
                //target in front
                forwardAmount = 5f;

                float stoppingDistance = 35f;
                float stoppingSpeed = 35f;
                if (distanceToTarget < stoppingDistance && carController.GetCarSpeed() > stoppingSpeed)
                {
                    //within stopping distance and moving forward too fast
                    forwardAmount = -1f;
                    carController.ApplyBreaking();
                }
                //else if(distanceToTarget < )
            } else 
                {
                //target behind
                float reverseDistance = 25f;
                if (distanceToTarget > reverseDistance)
                {
                    //too far to reverse
                    forwardAmount = 1f;
                } else { 
                    forwardAmount = -1f;
                }
                }

            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            //Debug.Log(angleToDir);
            if (angleToDir > 0.7f)
            {
                turnAmount = -1f;
            }
            else if (angleToDir < -0.7f)
            {
                turnAmount = 1f;
            } else
            {
                turnAmount = 0f;
            }
        } else 
            {
                //target reached
                if (carController.GetCarSpeed() > 10f)
                {
                    forwardAmount = -1f;
                    carController.ApplyBreaking();
                }
                else
                {
                    forwardAmount = 0f;
                }

                forwardAmount = 0f;
                turnAmount = 0f;
            }

        carController.SetInputs(forwardAmount, turnAmount);
    }
    
    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    [Header("Sensors")]
    public float sensorLength = 3f;
    public float frontSensorPosition = 0.5f;
    public float leftSideSensorPosition = 0.5f;

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos.z += frontSensorPosition;

        //front sensor
        if(Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength)) {

        }
        Debug.DrawLine(sensorStartPos, hit.point);

        //front right sensor
        sensorStartPos.x += leftSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);

        //front left sensor
        sensorStartPos.x -=  2*leftSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {

        }
        Debug.DrawLine(sensorStartPos, hit.point);
    }
    
}
