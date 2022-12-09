using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    WoodlandGameManager woodlandGameManager;

    private LineRenderer linerenderer;
    private GameObject Track;
    public GameObject player;
    public GameObject[] usersCars;
    public GameObject Ai;
    public GameObject AiSphere;
    public GameObject MiniMapCam;
    public GameObject PlayerSphere;
    public int carIndex;
    void Awake()
    {
        UpdateCarIndex();

        if (carIndex == 0)
        {
            player = usersCars[0];
            usersCars[0].gameObject.SetActive(true);
            usersCars[1].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 1)
        {
            player = usersCars[1];
            usersCars[1].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 2)
        {
            player = usersCars[2];
            usersCars[2].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[1].gameObject.SetActive(false);
            usersCars[3].gameObject.SetActive(false);
        }
        if (carIndex == 3)
        {
            player = usersCars[3];
            usersCars[3].gameObject.SetActive(true);
            usersCars[0].gameObject.SetActive(false);
            usersCars[1].gameObject.SetActive(false);
            usersCars[2].gameObject.SetActive(false);
        }
    }


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


        linerenderer.SetPosition(num_of_path, linerenderer.GetPosition(95));

    }

    private void UpdateCarIndex()
    {
        carIndex = PlayerPreferences.carIndex;

    }


    // Update is called once per frame
    void Update()
    {

        MiniMapCam.transform.position = (new Vector3(player.transform.position.x, MiniMapCam.transform.position.y, player.transform.position.z));
        //MiniMapCam.transform.position = (new Vector3(Ai.transform.position.x, MiniMapCam.transform.position.y, Ai.transform.position.z));

        PlayerSphere.transform.position = (new Vector3(player.transform.position.x, PlayerSphere.transform.position.y, player.transform.position.z));
        AiSphere.transform.position = (new Vector3(Ai.transform.position.x, AiSphere.transform.position.y, Ai.transform.position.z));



    }
}
