using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactables : MonoBehaviour {
    [SerializeField]
    protected bool canInteract;

    [SerializeField]
    UnityEvent onInteract;

    public bool CanInteract {
        get => canInteract;
        set => canInteract = value;
    }

    public virtual void Interact () {
        if (!CanInteract)
            return;
        Debug.Log ("Interact wiht" + gameObject.name);
    }

}