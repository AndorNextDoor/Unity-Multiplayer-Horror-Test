using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class TestRelay : MonoBehaviour
{

    public static TestRelay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    public async Task<String> CreateRelay()
    {
        try
        {
            Allocation allocation =  await RelayService.Instance.CreateAllocationAsync(3);

            



            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );


            NetworkManager.Singleton.StartHost();
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            return joinCode;
        }
        catch (RelayServiceException ex)
        {
            Debug.Log(ex);
            return null;
        }
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);


            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData,
                allocation.HostConnectionData
                );


            NetworkManager.Singleton.StartClient();
        }
        catch(RelayServiceException ex)
        {
            Debug.Log(ex);
        }
    }

}
