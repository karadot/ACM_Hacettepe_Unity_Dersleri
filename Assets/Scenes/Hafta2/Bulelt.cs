using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Bulelt : MonoBehaviour {

    // Start is called before the first frame update
    void Start () {
        Destroy (gameObject, 2f);
    }

    private void OnCollisionEnter (Collision other) {
        Destroy (gameObject);
    }
}