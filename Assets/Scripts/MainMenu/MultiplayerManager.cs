using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    public Button QuickPlaySearchButton;
    public TMP_InputField NameInput;

    private void Start()
    {
        SetPlayerName(null);
    }

    public void SetPlayerName(string name)
    {
        if(QuickPlaySearchButton.interactable = !string.IsNullOrEmpty(name))
        {
            if (name.Length > 25)
            {
                NameInput.text = name.Substring(0, 25);
            }
        }
    }

    // Quick play
    public void QuickPlay()
    {
        if (!GameState.IsOffline)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    // Server browser selection in menu
    public void ServerBrowserSelect()
    {

    }

    #region PhotonOverrides
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waiting for an opponent, creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined a room");

        // TODO
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PhotonNetwork.LoadLevel("Course1");
    }
    #endregion
}
