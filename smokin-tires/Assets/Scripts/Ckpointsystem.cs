using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Ckpointsystem : MonoBehaviour
{
    [Header("Cars")]
    public Transform Car01; //player
    public Transform Car02; //Blue
    public Transform Car03; //Yellow
    public Transform Car04; //Red

    [Header("Distances")]
    public float Car01Dist;
    public float Car02Dist;
    public float Car03Dist;
    public float Car04Dist;

    [Header("Placements")]
    public float First;
    public float Fourth;
    public float Second;
    public float Third;

    [Header("UI")]
    public TMP_Text firstPos;
    public TMP_Text secondPos;
    public TMP_Text thirdPos;
    public TMP_Text fourthPos;

    public float[] DistanceArrays;
    public GameObject NextCheckPoint;

    void Start()
    {
        NextCheckPoint.SetActive(false);
    }


    void Update()
    {
        Car01Dist = Vector3.Distance(transform.position, Car01.position);
        Car02Dist = Vector3.Distance(transform.position, Car02.position);
        Car03Dist = Vector3.Distance(transform.position, Car03.position);
        Car04Dist = Vector3.Distance(transform.position, Car04.position);

        //finds the smallest distance to the next checkpoint, which tells us the leader
        First = Mathf.Min(Car01Dist, Car02Dist, Car03Dist, Car04Dist);
        //finds the largest distance to the next checkpoint, which tells us the car in the fourth/last position
        Fourth = Mathf.Max(Car01Dist, Car02Dist, Car03Dist, Car04Dist);


        #region First
        if (Car01Dist == First)
        {
            firstPos.text = "Player";
        }
        if (Car02Dist == First)
        {
            firstPos.text = "Blue";
        }
        if (Car03Dist == First)
        {
            firstPos.text = "Yellow";
        }
        if (Car04Dist == First)
        {
            firstPos.text = "Red";
        }
        #endregion


        #region Fourth
        if (Car01Dist == Fourth)
        {
            fourthPos.text = "Player";
        }
        if (Car02Dist == Fourth)
        {
            fourthPos.text = "Blue";
        }
        if (Car03Dist == Fourth)
        {
            fourthPos.text = "Yellow";
        }
        if (Car04Dist == Fourth)
        {
            fourthPos.text = "Red";
        }
        #endregion


        #region Third
        //:TODO maybe switch case this
        if (Car01Dist < Fourth && Car01Dist > First)
        {
            Third = Car01Dist;
            thirdPos.text = "Player";
        }

        if (Car02Dist < Fourth && Car02Dist > First)
        {
            Third = Car02Dist;
            thirdPos.text = "Blue";
        }

        if (Car03Dist < Fourth && Car03Dist > First)
        {
            Third = Car03Dist;
            thirdPos.text = "Yellow";
        }

        if (Car04Dist < Fourth && Car04Dist > First)
        {
            Third = Car04Dist;
            thirdPos.text = "Red";
        }
        #endregion



        //: TODO switch case this
        #region Second
        if (Car01Dist != First && Car01Dist != Third && Car01Dist != Fourth)
        {
            Second = Car01Dist;
            secondPos.text = "Player";
        }

        if (Car02Dist != First && Car02Dist != Third && Car02Dist != Fourth)
        {
            Second = Car02Dist;
            secondPos.text = "Blue";
        }

        if (Car03Dist != First && Car03Dist != Third && Car03Dist != Fourth)
        {
            Second = Car03Dist;
            secondPos.text = "Yellow";
        }

        if (Car04Dist != First && Car04Dist != Third && Car04Dist != Fourth)
        {
            Second = Car01Dist;
            secondPos.text = "Red";
        }
        #endregion



}
}