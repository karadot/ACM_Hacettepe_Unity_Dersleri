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

    [SerializeField]
    bool isGrounded = false, useGravity;

    [SerializeField]
    LayerMask groundLayers;

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
        isGrounded = Physics.CheckSphere (transform.position, groundDistance, groundLayers);
        if (isGrounded && verticalMotion.y < 0) {
            verticalMotion.y = -2f;
        }
        if (useGravity)
            verticalMotion.y += Time.deltaTime * gravity;

        if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
            verticalMotion.y = jumpVelocityY;
        }
    }
    private void FixedUpdate () {
        controller.Move (motion);
        controller.Move (verticalMotion * Time.fixedDeltaTime);
    }

    public void AddVerticalVelocity (float y) {
        verticalMotion.y += y * Time.fixedDeltaTime;
    }

    public void SetVerticalVelocity (float y) {
        verticalMotion.y = y;
    }

    public void UseGravity (bool g) {
        useGravity = g;
        verticalMotion.y = 0;
    }

    private void OnDrawGizmos () {
        Gizmos.color = isGrounded?Color.red : Color.green;
        Gizmos.DrawSphere (transform.position, groundDistance);
    }

}