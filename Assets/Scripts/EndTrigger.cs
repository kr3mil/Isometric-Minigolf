using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public int HoleNum;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().SetCollisions(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            var script = other.GetComponent<PlayerController>();
            if (GameState.IsShooting)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().EndHole(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
    }
}
