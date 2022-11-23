using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_manager : MonoBehaviour
{
 internal enum driver
    {
        AI,
        keyboard
    }
    [SerializeField] driver driverController;

    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public bool handbrake;
    [HideInInspector] public bool boosting;

    private void FixedUpdate()
    {
        switch (driverController) { 
            case driver.AI:
                break;
            case driver.keyboard:
                break;
        }
    }
}
