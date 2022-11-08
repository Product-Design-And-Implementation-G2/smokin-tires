using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoins : MonoBehaviour
{
    public int coins;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //enables coin collection
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            coins++;
            Debug.Log("Coin collected: " + coins);

        }
    }


}
