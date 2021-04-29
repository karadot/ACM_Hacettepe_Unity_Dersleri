using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactables : MonoBehaviour {
    [SerializeField]
    protected bool canInteract;
    public bool CanInteract {
        get => canInteract;
        set => canInteract = value;
    }
    public virtual void Interact () {
        Debug.Log ("Interact wiht" + gameObject.name);
    }

}