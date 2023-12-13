using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject itemToPickUp;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private string playerInteractText;
    

    public void Interact(Transform interactorTransform)
    {
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, range, playerLayer); 


        //Get item to player from transform
        
    }

    public string GetInteractText()
    {
        return playerInteractText;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // You can change the color to whatever you prefer
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
