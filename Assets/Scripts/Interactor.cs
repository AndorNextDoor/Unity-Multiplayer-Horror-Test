using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

interface IInteractable
{
    public void Interact(Transform interactorTransform);
    string GetInteractText();
}

public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform InteractorSource;
    [SerializeField] private float InteractRange;
    [SerializeField] private TextMeshProUGUI interactText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactText.text = interactObj.GetInteractText();
                interactText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact(transform);
                }
            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }

}
