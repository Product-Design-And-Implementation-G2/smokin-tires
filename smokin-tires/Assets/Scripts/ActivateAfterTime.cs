using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterTime : MonoBehaviour
{
    public float TimeAfter = 15f;
    public GameObject FinishObject;
    public bool isSpawned = false;

    // Update is called once per frame
    void Update()
    {
        TimeAfter = TimeAfter - Time.deltaTime;
        if(TimeAfter <= 0 && isSpawned == false)
        {
            Debug.Log("Spawn lap object");
            FinishObject.SetActive(true);
            TimeAfter = 15f;
            isSpawned = true;
            //TODO: add update isSpawned value to gameManagers
        }
    }
}
