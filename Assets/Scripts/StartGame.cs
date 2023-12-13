using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour, IInteractable
{
    GameManager gameManager;
    [SerializeField] private string playerInteractText;

    public void Interact(Transform interactorTransform)
    {
        GameManager.Instance.StartSession();
    }

    public string GetInteractText()
    {
        return playerInteractText;
    }

}
