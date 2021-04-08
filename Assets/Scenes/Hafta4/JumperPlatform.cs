using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlatform : MonoBehaviour {

    [SerializeField]
    float jumpHeight = 5f;

    private void OnTriggerEnter (Collider other) {
        PlayerMovement controller = other.GetComponent<PlayerMovement> ();

        if (controller != null) {
            //controller.DoJump (jumpHeight);
            controller.SetExternalMotion (transform.up * jumpHeight, true);
        }
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position + transform.up * jumpHeight, .25f);
    }
}