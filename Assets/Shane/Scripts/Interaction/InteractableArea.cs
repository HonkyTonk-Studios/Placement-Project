using System;
using UnityEngine;

//If the player enters an interactable area the player interaction script is notified and gets a reference to the interface/object.

public abstract class InteractableArea : MonoBehaviour, iInteractable
{
    //Events.
    public static event Action<InteractableArea> EnteredAreaEvent;
    public static event Action<InteractableArea> LeftAreaEvent;

    [SerializeField] protected bool PlayerIsInArea;

    public abstract void Interact();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        PlayerIsInArea = true;
        EnteredAreaEvent?.Invoke(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        PlayerIsInArea = false;
        LeftAreaEvent?.Invoke(this);
    }

    private void OnDestroy()
    {
        if (PlayerIsInArea)
            LeftAreaEvent?.Invoke(this);
    }
}
