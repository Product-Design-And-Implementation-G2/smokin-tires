using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertRespawnSystem : MonoBehaviour
{
    public DesertGameManager gameManager;
    public GameObject[] respawnPoints;
    public GameObject currentRespawnPoint;
    public int respawnIndex;

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
            if (respawnIndex >= respawnPoints.Length)
            {
                StartCoroutine(RestoreRespawns());
                respawnIndex = 0;
            }
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

