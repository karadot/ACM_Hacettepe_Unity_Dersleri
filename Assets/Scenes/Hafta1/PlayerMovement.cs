using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float speed = 5f, height = 2f, gravity = -9.81f;

    [SerializeField]
    float groundDistance = .1f;

    [SerializeField]
    Vector3 motion, verticalMotion;

    float jumpVelocityY;

    CharacterController controller;

    bool isGrounded = false;

    private void Start () {
        controller = GetComponent<CharacterController> ();
        jumpVelocityY = Mathf.Sqrt (height * -2 * gravity);
    }
    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        motion = transform.right * x + transform.forward * z;
        motion *= (speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere (transform.position, groundDistance);
        if (isGrounded && verticalMotion.y < 0) {
            verticalMotion.y = -2f;
        }
        verticalMotion.y += Time.deltaTime * gravity;

        if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
            verticalMotion.y = jumpVelocityY;
        }
    }
    private void FixedUpdate () {
        controller.Move (motion);
        controller.Move (verticalMotion * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos () {
        Gizmos.DrawSphere (transform.position, groundDistance);
    }
}