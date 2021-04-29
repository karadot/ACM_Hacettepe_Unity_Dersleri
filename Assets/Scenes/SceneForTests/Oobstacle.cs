using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oobstacle : MonoBehaviour {
    [SerializeField]
    float force = 5;

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player")) {
            Debug.Log ("Player hit");
            Vector3 dir = other.transform.position - transform.position;
            dir.Normalize ();
            other.gameObject.GetComponent<PlayerMovement> ().SetExternalMotion (dir * force, true);
        }
    }
}