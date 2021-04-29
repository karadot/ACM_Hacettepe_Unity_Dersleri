using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private void OnTriggerEnter (Collider other) {
        /*
        if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {

        }*/
        if (other.CompareTag ("Player")) {
            LevelController.Instance.TargetCount--;
            Destroy (gameObject);
        }
    }
}