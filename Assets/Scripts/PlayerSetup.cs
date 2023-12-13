using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        if(IsOwner)
        {
            SetActivePlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetActivePlayer()
    {
        this.GetComponent<PlayerController>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
