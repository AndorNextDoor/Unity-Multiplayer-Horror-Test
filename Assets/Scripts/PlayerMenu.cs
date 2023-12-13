using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;


public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private Transform playersContainer;
    private GameObject playerMenu;



    private bool IsMenuActive = false;

    private void Awake()
    {
        LobbyManager.Instance.OnJoinedLobbyUpdate += RefreshCurrentLobby;
        playerMenu = transform.GetChild(0).gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!IsMenuActive)
            {
                ShowUI();
            }
            else
            {
                HideUI();
            }
        }
    }


    private void ShowUI()
    {
        playerMenu.SetActive(true);
        IsMenuActive = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideUI()
    {
        playerMenu.SetActive(false);
        IsMenuActive=false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void OnLeaveButtonPressed()
    {
        LobbyManager.Instance.LeaveLobby();
    }

    private void RefreshCurrentLobby(object sender, LobbyManager.LobbyEventArgs e)
    {


        if (playersContainer.childCount - 1 == e.lobby.Players.Count)
            return;


        for (int i = 0; i < playersContainer.childCount; i++)
        {
            if (i > 0)
            {
                Destroy(playersContainer.GetChild(i).gameObject);
            }
        }

        Transform container = playersContainer.GetChild(0);

        foreach (Player player in e.lobby.Players)
        {
            Transform currentContainer = currentContainer = Instantiate(container, playersContainer);

            string playername = player.Data["PlayerName"].Value;
            currentContainer.GetChild(0).GetComponent<TextMeshProUGUI>().text = playername;

            currentContainer.gameObject.SetActive(true);
        }
    }
}
