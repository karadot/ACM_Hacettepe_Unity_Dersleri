using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactables {

    Animator animator;
    AudioSource source;

    bool isOpened = false;

    private void Start () {
        animator = GetComponent<Animator> ();
        source = GetComponent<AudioSource> ();
    }
    public override void Interact () {

        if (!CanInteract)
            return;
        base.Interact ();
        isOpened = !isOpened;
        animator.SetBool ("Open", isOpened);
        source.Play ();
    }
}