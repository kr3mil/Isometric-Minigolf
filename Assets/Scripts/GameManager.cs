using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("Course1", LoadSceneMode.Single);
    }
}
