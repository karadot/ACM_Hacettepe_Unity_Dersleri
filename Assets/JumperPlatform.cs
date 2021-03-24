using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlatform : MonoBehaviour {

    [SerializeField]
    float jumpForce = 5f;
    private void OnCollisionEnter (Collision other) {
        Rigidbody otherRigidbody = other.rigidbody;

        if (otherRigidbody != null) {
            otherRigidbody.AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void OnTriggerStay (Collider other) {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody> ();

        if (otherRigidbody != null) {
            float randomX = Random.Range (-5, 5);
            float randomY = Random.Range (10, 20);
            float randomZ = Random.Range (-5, 5);
            otherRigidbody.AddForce (new Vector3 (0, randomY, 0));
        }
    }

}