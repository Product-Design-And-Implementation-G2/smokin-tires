using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CarCycleManager : MonoBehaviour
{
    public GameObject[] choiceOfCar;
    public int index;

    void Start()
    {
        index = 0;

        if (index == 0)
            choiceOfCar[0].gameObject.SetActive(true);
    }
    //next picture
    public void Next()
    {
        if(index == choiceOfCar.Length -1)
        {
            Debug.Log("Out of bounds");
        } else {
        choiceOfCar[index].gameObject.SetActive(false);
        index++;
        choiceOfCar[index].gameObject.SetActive(true);
        }
        Debug.Log(index);
    }
    //previous picture
    public void Previous()
    {
        if(index == 0)
        {
            Debug.Log("Out of bounds");

        } else {
            choiceOfCar[index].gameObject.SetActive(false);
            index--;
            choiceOfCar[index].gameObject.SetActive(true);
        }
        Debug.Log(index);
    }
    public int ReturnIndex()
    {
        return index;
    }
}
