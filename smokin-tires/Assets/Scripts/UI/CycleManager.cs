using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CycleManager : MonoBehaviour
{
    public GameObject[] background;
    public int index;

    void Start()
    {
        index = 0;

        if (index == 0)
            background[0].gameObject.SetActive(true);
    }
    //next picture
    public void Next()
    {
        if(index == background.Length -1)
        {
            Debug.Log("Out of bounds");
        } else {
        background[index].gameObject.SetActive(false);
        index++;
        background[index].gameObject.SetActive(true);
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
            background[index].gameObject.SetActive(false);
            index--;
            background[index].gameObject.SetActive(true);
        }
        Debug.Log(index);
    }
}
