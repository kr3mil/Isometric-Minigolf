using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    public Button MultiplayerButton;
    public GameObject MainPanel;
    public TextMeshProUGUI ConnectionText;
    public GameObject ReconnectButton;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = GameState.GameVersion;
        TryConnect();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PhotonNetwork.Disconnect();
        }
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

    public void TryConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
        SetConnectionText(null);
    }

    private void SetConnectionText(bool? state)
    {
        if(state != null)
        {
            if (state == true)
            {
                ConnectionText.color = Color.green;
                ConnectionText.text = "Connected";
                ReconnectButton.SetActive(false);
            }
            else
            {
                ConnectionText.color = Color.red;
                ConnectionText.text = "Disconnected";
                MultiplayerButton.interactable = false;
                ReconnectButton.SetActive(true);
            }
        }
        else
        {
            ConnectionText.color = Color.white;
            ConnectionText.text = "Connecting";
            ReconnectButton.SetActive(false);
        }
    }

    #region Photon overrides
    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED TO MASTER");
        MultiplayerButton.interactable = true;
        SetConnectionText(true);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        foreach(Transform child in MainPanel.transform)
        {
            if (child.name == "MenuStrip")
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
        SetConnectionText(false);
    }
    #endregion
}
