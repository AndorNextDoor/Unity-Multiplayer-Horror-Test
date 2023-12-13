using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private LobbyManager lobbyManager;

    [SerializeField] private Transform lobbiesContainer;
    [SerializeField] private Transform lobbiesListMenu;



    private void Awake()
    {
        lobbyManager.OnLobbyListChanged += ShowLobbyList;
        lobbyManager.OnJoinedLobby += DisableMainMenu;
    }



    private void ShowLobbyList(object sender, LobbyManager.OnLobbyListChangedEventArgs e)
    {
        lobbiesListMenu.gameObject.SetActive(true);

        for (int i = 0; i < lobbiesContainer.childCount; i++)
        {
            if(i > 0)
            {
                Destroy(lobbiesContainer.GetChild(i).gameObject);
            }
        }

        Transform container = lobbiesContainer.GetChild (0);

        foreach (Lobby lobby in e.lobbyList)
        {
            Transform currentContainer = Instantiate(container,lobbiesContainer);
            currentContainer.GetChild(0).GetComponent<TextMeshProUGUI>().text = lobby.Name;
            currentContainer.GetChild(1).GetComponent<TextMeshProUGUI>().text = lobby.Players.Count + "/" + lobby.MaxPlayers;

            Button button = currentContainer.GetChild(2).GetComponent<Button>();
            button.onClick.AddListener(() => lobbyManager.JoinLobby(lobby));
            currentContainer.gameObject.SetActive(true);
        }
    }


    private void DisableMainMenu(object sender, LobbyManager.LobbyEventArgs e)
    {
        this.gameObject.SetActive(false);

    }


    public void SetPlayerName(string _PlayerName)
    {
        if (_PlayerName != null)
        {
            PlayerPrefs.SetString("PlayerName", _PlayerName.Trim());
        }
    }
}
