using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoins : MonoBehaviour
{
    public int coins;
    [SerializeField] GameObject AllCoins;

    //enables coin collection
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //option to destroy the objects
            Destroy(other.gameObject);
            //option to set the coins as deactivated for later use
            //other.gameObject.SetActive(false);
            coins++;
            updateCoinAmount(coins);
        }
    }
    public void updateCoinAmount(int a)
    {
        FindObjectOfType<LakeGameManager>().GetCoins(a);
    }
}
