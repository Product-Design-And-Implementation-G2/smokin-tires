using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionCompetitors : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Competitor_lakeside");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
