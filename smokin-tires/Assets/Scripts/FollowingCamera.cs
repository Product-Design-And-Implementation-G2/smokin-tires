using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform Player;
    public Vector3 CameraOffSet;

    // Update is called once per frame
    void Update()
    {
       transform.position = Player.position +CameraOffSet;
    }

}
