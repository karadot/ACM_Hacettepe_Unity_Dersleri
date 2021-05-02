using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactables {

    Animator animator;
    AudioSource source;

    bool isOpened = false;

    [SerializeField]
    UnityEngine.Events.UnityEvent onDoorOpened;

    private void Start () {
        animator = GetComponent<Animator> ();
        source = GetComponent<AudioSource> ();
    }
    public override void Interact () {

        base.Interact ();
        isOpened = !isOpened;
        if (isOpened)
            onDoorOpened?.Invoke ();
        animator.SetBool ("Open", isOpened);
        source.Play ();
    }
}