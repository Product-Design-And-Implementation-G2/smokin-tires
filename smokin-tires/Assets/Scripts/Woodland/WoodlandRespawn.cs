using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class WoodlandRespawn : MonoBehaviour
{
    public WoodlandGameManager gameManager;
    public GameObject[] respawnPoints;
    public GameObject currentRespawnPoint;
    public int respawnIndex;

    public float Distance;

    float oldDistance =0f;

    public GameObject goingTheWrongWayScreen;
    private void Start()
    {

        respawnIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RespawnTag")
        {
            respawnPoints[respawnIndex].SetActive(false);
            currentRespawnPoint = respawnPoints[respawnIndex];
            gameManager.currentPlayerRespawnPoint = currentRespawnPoint.transform;
            respawnIndex++;
            if(respawnIndex >= 5)
            {
                StartCoroutine(RestoreRespawns());
                respawnIndex = 0;
            }
        }
    }

    private void Update()
    {
        if (gameManager.isRunning) { 
        Distance = Vector3.Distance(respawnPoints[respawnIndex].transform.position, gameObject.transform.position);



        if (Distance > oldDistance) { 
            print("Player is moving away from the next point");
            goingTheWrongWayScreen.SetActive(true);
        } 
        else if (oldDistance > Distance)
        {
            goingTheWrongWayScreen.SetActive(false);
        }
        oldDistance = Distance;
        }
    }

   IEnumerator RestoreRespawns()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < respawnPoints.Length; i++)
        {
            respawnPoints[i].SetActive(true);
        }
    }
}
