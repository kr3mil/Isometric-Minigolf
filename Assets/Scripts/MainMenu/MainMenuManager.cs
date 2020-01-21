using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void Singleplayer()
    {
        GameState.IsOffline = true;
    }
    public void Muliplayer()
    {
        GameState.IsOffline = false;
        // TODO connect to multiplayer using photon
    }
    // Exit game
    public void ExitGame() => Application.Quit();
}
