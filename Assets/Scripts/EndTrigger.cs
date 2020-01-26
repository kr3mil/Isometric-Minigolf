using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public int HoleNum;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            var script = other.GetComponent<PlayerController>();
            if (script.IsShooting)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().EndHole(other.gameObject);
            }
        }
    }
}
