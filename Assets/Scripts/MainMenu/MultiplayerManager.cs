using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour
{
    public Button QuickPlaySearchButton;
    public TMP_InputField NameInput;

    private void Start()
    {
        SetPlayerName(null);
    }

    private void Update()
    {

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

    }

    // Server browser selection in menu
    public void ServerBrowserSelect()
    {

    }
}
