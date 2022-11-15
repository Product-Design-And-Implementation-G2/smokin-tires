using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public int coins;

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
        FindObjectOfType<GameManager>().GetCoins(a);
        FindObjectOfType<GameManager>().UpdateCoinAmountText();
    }
}
