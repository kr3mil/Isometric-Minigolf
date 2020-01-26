using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Physics.IgnoreLayerCollision(8, 8, true);
    }
    public void LoadLevel()
    {
        GameState.IsOffline = true;
        SceneManager.LoadScene("Course1", LoadSceneMode.Single);
    }
}
