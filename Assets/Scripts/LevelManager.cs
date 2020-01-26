using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    public GameObject PlayerPrefab;

    public IEnumerable<Transform> SpawnPoints;
    [SerializeField] private int m_CurrentHole;
    private GameObject m_LevelParent;
    private void Awake()
    {
        m_LevelParent = GameObject.FindGameObjectWithTag("LevelParent");
        SpawnPoints = m_LevelParent.GetComponentsInChildren<Transform>().Where(x => x.tag == "StartPosition").Select(x => x.transform);
        SpawnPlayer();
        Debug.Log("Spawn points: " + SpawnPoints.Count());
        #region DEBUG
        //m_CurrentHole--;
        //StartCoroutine(DebugEndHole());
        #endregion
    }

    public void EndHole(GameObject player)
    {
        if ((m_CurrentHole += 1) < SpawnPoints.Count())
        {
            var script = player.GetComponent<PlayerController>();
            script.MoveToNextHole(SpawnPoints.ElementAt(m_CurrentHole), true);
        }
        else
        {
            Debug.Log("LAST HOLE");
        }
        
    }

    public void ResetHole(GameObject player)
    {
        player.GetComponent<PlayerController>().MoveToNextHole(SpawnPoints.ElementAt(m_CurrentHole), false);
    }

    private IEnumerator DebugEndHole()
    {
        yield return new WaitForSeconds(1);
        EndHole(GameObject.FindWithTag("Player"));
    }

    private void SpawnPlayer()
    {
        var player = GameState.IsOffline ? GameObject.Instantiate(PlayerPrefab, SpawnPoints.ElementAt(0).position, Quaternion.identity) : PhotonNetwork.Instantiate(PlayerPrefab.name, SpawnPoints.ElementAt(0).position, Quaternion.identity);
    }
}
