using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDisablerDesert : MonoBehaviour
{
    public DesertGameManager gameManager;

    //enables coin collection
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //destroy waypoint layer objects
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            IncrementWaypointAmount();
        }
    }
    public void IncrementWaypointAmount()
    {
        gameManager.WaypointCount();
    }
}
