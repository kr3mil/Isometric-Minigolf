using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    public IEnumerable<Transform> SpawnPoints;
    private int m_CurrentHole = 0;
    private GameObject m_LevelParent;
    private void Awake()
    {
        m_LevelParent = GameObject.FindGameObjectWithTag("LevelParent");
        SpawnPoints = m_LevelParent.GetComponentsInChildren<Transform>().Where(x => x.tag == "StartPosition").Select(x => x.transform);
        SpawnPlayer();
        Debug.Log("Spawn points: " + SpawnPoints.Count());
    }

    public void EndHole(GameObject player)
    {
        if ((m_CurrentHole += 1) < SpawnPoints.Count())
        {
            var script = player.GetComponent<PlayerController>();
            script.MoveToNextHole(SpawnPoints.ElementAt(m_CurrentHole));
        }
        else
        {
            Debug.Log("LAST HOLE");
        }
        
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(PlayerPrefab, SpawnPoints.ElementAt(0).position, Quaternion.identity);
    }
}
