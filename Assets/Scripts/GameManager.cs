using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsGameStarted;

    private void Awake()
    {
        Instance = this;
    }


    public void StartSession()
    {
        StartSessionServerRpc();
    }

    [ServerRpc]
    private void StartSessionServerRpc()
    {
        Debug.Log("GameStarted");
        IsGameStarted = true;

    }

}
