using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPlatform : MonoBehaviour {

    [SerializeField]
    float jumpForce = 5f;

    [SerializeField]
    float hoverHeight = 5, hoverAralik = 2;

    Vector3 hoverCenter;

    private void Start () {
        hoverCenter = transform.position + transform.up * hoverHeight;
    }

    private void OnValidate () {
        hoverCenter = transform.position + transform.up * hoverHeight;
    }
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

        PlayerMovement controller = other.GetComponent<PlayerMovement> ();

        if (controller != null) {
            Vector3 hoverMotion = hoverCenter - other.transform.position;
            hoverMotion.Normalize ();
            float distance = Vector3.Distance (other.transform.position, hoverCenter);
            Debug.DrawRay (other.transform.position, hoverMotion);

            if (distance > hoverAralik) {
                controller.SetExternalMotion (hoverMotion * jumpForce, false);
            } else {
                controller.AddExternalMotion (hoverMotion * jumpForce * 5);
            }
        }
    }

    private void OnTriggerEnter (Collider other) {
        PlayerMovement controller = other.GetComponent<PlayerMovement> ();

        if (controller != null) {
            controller.UseGravity (false);
        }
    }

    private void OnTriggerExit (Collider other) {

        PlayerMovement controller = other.GetComponent<PlayerMovement> ();

        if (controller != null) {
            controller.UseGravity (true);
        }

    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (hoverCenter, .25f);
    }

}