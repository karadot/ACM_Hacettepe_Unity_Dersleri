using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlatform : MonoBehaviour {

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
            float distance = other.transform.position.y - hoverCenter.y;
            if (Mathf.Abs (distance) < hoverAralik) {
                distance = Mathf.Sign (distance);
                controller.AddVerticalVelocity (-distance * jumpForce);
            } else {
                controller.SetVerticalVelocity (-distance * jumpForce);
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