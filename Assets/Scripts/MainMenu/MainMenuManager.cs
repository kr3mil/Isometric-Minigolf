using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    public Button MultiplayerButton;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = GameState.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
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

    #region Photon overrides
    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED TO MASTER");
        MultiplayerButton.interactable = true;
    }
    #endregion
}
