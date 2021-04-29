using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Collectibles : MonoBehaviour {

    [SerializeField]
    UnityEvent onCollected;
    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            onCollected?.Invoke ();
            Destroy (gameObject);
        }
    }
}