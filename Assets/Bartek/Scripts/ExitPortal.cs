using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour, iInteractable
{
    public void Interact()
    {
        Application.Quit();
    }
}
