using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using UnityEngine;

public class AutoConnectToLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        LobbyManager.Instance.Authenticate();
        AuthenticationService.Instance.SignedIn += () =>
        {
            try
            {
                LobbyManager.Instance.QuickCreateOrJoinLobby();

                

            }catch (LobbyServiceException ex)
            {
                Debug.LogException(ex);
            }

           
        };

    }
}
