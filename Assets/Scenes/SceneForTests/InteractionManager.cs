using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    Interactables currInteractable;

    private void Update () {
        if (currInteractable != null && Input.GetKeyDown (KeyCode.E)) {
            currInteractable.Interact ();
        }
    }
    private void OnTriggerEnter (Collider other) {
        Interactables temp = other.GetComponent<Interactables> ();
        if (temp != null)
            currInteractable = temp;
    }

    private void OnTriggerExit (Collider other) {
        if (currInteractable != null && other.gameObject == currInteractable.gameObject)
            currInteractable = null;
    }
}