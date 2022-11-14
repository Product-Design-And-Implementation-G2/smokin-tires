using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    public static PlayerPreferences Instance;

    public CarCycleManager carInstance;

    public static int carIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCarIndex()
    {
        carIndex = carInstance.ReturnIndex();
    }
    public int ReturnCarIndex()
    {
        return carIndex;
    }
}
