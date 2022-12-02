using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private LineRenderer linerenderer;
    private GameObject Track;

    public GameObject Vehicles;
    public GameObject Ai;
    public GameObject AiSphere;
    public GameObject MiniMapCam;
    public GameObject PlayerSphere;

    // Start is called before the first frame update
    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
        Track = this.gameObject;

        int num_of_path = Track.transform.childCount;
        linerenderer.positionCount = num_of_path + 1;

        for(int x = 0; x < num_of_path; x++)
        {
            linerenderer.SetPosition(x, new Vector3(Track.transform.GetChild(x).transform.position.x, 4, Track.transform.GetChild(x).transform.position.z));
        }

        linerenderer.startWidth = 7f;
        linerenderer.endWidth = 7f;
        
    }

    // Update is called once per frame
    void Update()
    {
        MiniMapCam.transform.position = (new Vector3(Vehicles.transform.position.x, MiniMapCam.transform.position.y, Vehicles.transform.position.z));
        //MiniMapCam.transform.position = (new Vector3(Ai.transform.position.x, MiniMapCam.transform.position.y, Ai.transform.position.z));

        PlayerSphere.transform.position = (new Vector3(Vehicles.transform.position.x, PlayerSphere.transform.position.y, Vehicles.transform.position.z));
        AiSphere.transform.position = (new Vector3(Ai.transform.position.x, AiSphere.transform.position.y, Ai.transform.position.z));



    }
}
