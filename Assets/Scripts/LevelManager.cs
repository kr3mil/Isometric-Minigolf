using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private Transform SpawnPoint;
    private void Awake()
    {
        SpawnPoint = GameObject.FindGameObjectWithTag("StartPosition").transform;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(PlayerPrefab, SpawnPoint.position, Quaternion.identity);
    }
}
