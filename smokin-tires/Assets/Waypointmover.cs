using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypointmover : MonoBehaviour
{
    [SerializeField] private Path waypoints;
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private float distanceThreshold = 0.1f;

    //The current waypoint target that the object is moving towards
    private Transform currentWaypoint;

    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        //set the next waypoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }   
    }
}
