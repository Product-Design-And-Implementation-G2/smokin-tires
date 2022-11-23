using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDisabler_ai : MonoBehaviour
{
    public GameManagerSandMap gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.SetActive(false);
            gameManager.IncrementAIWaypointCount();
            gameManager.ActivateNextAIWaypoint();
        }
    }
}

