using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public GameObject aiCar;
    private void Start()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(aiCar.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
    }

}
