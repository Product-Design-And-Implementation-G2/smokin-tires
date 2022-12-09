using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WoodlandCheckpoints : MonoBehaviour
{

    public float[] DistanceArrays;

    [Header("Cars Car01 = Player Car")]
    public GameObject[] usersCars;
     //public Transform Car01;
    public Transform Car02;
    //public Transform Car03;
    //public Transform Car04;

    float First;
    //float Fourth;
    float Second;
    //float Third;

    [Header("UI")]
    public TextMeshProUGUI Car01Text;
    public TMP_Text Car02Text;
    //public TMP_Text Car03Text;
    //public TMP_Text Car04Text;

    public GameObject NextCheckPoint;
    public WoodlandGameManager gameManager;

    void Start()
    {
        NextCheckPoint.SetActive(false);
    }


    void Update()
    {

        DistanceArrays[0] = Vector3.Distance(transform.position, usersCars[gameManager.carIndex].transform.position);
        DistanceArrays[1] = Vector3.Distance(transform.position, Car02.position);
        //DistanceArrays[2] = Vector3.Distance(transform.position, Car03.position);
        //DistanceArrays[3] = Vector3.Distance(transform.position, Car04.position);

        Array.Sort(DistanceArrays);

        First = DistanceArrays[0];
        Second = DistanceArrays[1];
        //Third = DistanceArrays[2];
        //Fourth = DistanceArrays[3];

        float Car01Dist = Vector3.Distance(transform.position, usersCars[gameManager.carIndex].transform.position);
        float Car02Dist = Vector3.Distance(transform.position, Car02.position);
        //float Car03Dist = Vector3.Distance(transform.position, Car03.position);
        //float Car04Dist = Vector3.Distance(transform.position, Car04.position);

        #region Car01UI
        if (Car01Dist == First)
        {
            Car01Text.text = "1/2";
        }
        if (Car01Dist == Second)
        {
            Car01Text.text = "2/2";
        }
    
        #endregion

        #region Car02UI
        if (Car02Dist == First)
        {
            Car02Text.text = "1st";
        }
        if (Car02Dist == Second)
        {
            Car02Text.text = "2nd";
        }
      
        #endregion

        //#region Car03UI
        //if (Car03Dist == First)
        //{
        //    Car03Text.text = "1st";
        //}
        //if (Car03Dist == Second)
        //{
        //    Car03Text.text = "2nd";
        //}
        //if (Car03Dist == Third)
        //{
        //    Car03Text.text = "3rd";
        //}
        //if (Car03Dist == Fourth)
        //{
        //    Car03Text.text = "4th";
        //}
        //#endregion

        //#region Car04UI
        //if (Car04Dist == First)
        //{
        //    Car04Text.text = "1st";
        //}
        //if (Car04Dist == Second)
        //{
        //    Car04Text.text = "2nd";
        //}
        //if (Car04Dist == Third)
        //{
        //    Car04Text.text = "3rd";
        //}
        //if (Car04Dist == Fourth)
        //{
        //    Car04Text.text = "4th";
        //}
        //#endregion

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player tag noticed");
            NextCheckPoint.SetActive(true);
            gameObject.SetActive(false);

        }
        if (other.tag == "Bluecar")
        {
            Debug.Log("Bluecar");
        }
        if (other.tag == "Yellowcar")
        {
            Debug.Log("Yellowcar");
        }
        if (other.tag == "Redcar")
        {
            Debug.Log("Redcar");
        }
    }
}

