using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float speed = 5f, height = 2f, gravity = -9.81f, drag = 3f;

    [SerializeField]
    float groundDistance = .1f;

    [SerializeField]
    Vector3 motion, verticalMotion;
    [SerializeField]
    Vector3 externalMotion = Vector3.zero;

    float jumpVelocityY;

    CharacterController controller;

    [SerializeField]
    bool isGrounded = false, useGravity = true;

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
            if (externalMotion != Vector3.zero)
                externalMotion = Vector3.zero;

        }

        if (useGravity) {
            verticalMotion.y += Time.deltaTime * gravity;
        }

        if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
            verticalMotion.y = jumpVelocityY;
        }
    }

    public void DoJump (float jumpHeight) {
        float target = Mathf.Sqrt (jumpHeight * (-2.0f) * gravity);
        verticalMotion.y = float.IsNaN (target) ? 3 : target;
    }

    private void FixedUpdate () {
        controller.Move (motion);

        if (externalMotion != Vector3.zero) {
            controller.Move (externalMotion * Time.fixedDeltaTime);
            externalMotion.x -= drag * Time.fixedDeltaTime * Mathf.Sign (externalMotion.x);
            externalMotion.z -= drag * Time.fixedDeltaTime * Mathf.Sign (externalMotion.z);
            externalMotion.x = Mathf.Abs (externalMotion.x) < 1f?0 : externalMotion.x;
            externalMotion.z = Mathf.Abs (externalMotion.z) < 1f?0 : externalMotion.z;
        }

        controller.Move (verticalMotion * Time.fixedDeltaTime);
    }

    public void AddVerticalVelocity (float y) {
        verticalMotion.y += y * Time.fixedDeltaTime;
    }

    public void SetVerticalVelocity (float y) {
        verticalMotion.y = y;
    }

    public void SetExternalMotion (Vector3 extMotion, bool jump) {
        externalMotion.z = extMotion.z;
        externalMotion.x = extMotion.x;
        if (jump) {
            DoJump (extMotion.y);
        } else {
            SetVerticalVelocity (extMotion.y);
        }

    }

    public void AddExternalMotion (Vector3 extMotion) {
        externalMotion.z += extMotion.z * Time.deltaTime;
        externalMotion.x += extMotion.x * Time.deltaTime;
        verticalMotion.y += extMotion.y * Time.deltaTime;
    }

    public void UseGravity (bool g) {
        useGravity = g;
        verticalMotion.y = 0;
    }

    private void OnDrawGizmos () {
        Gizmos.color = isGrounded?Color.red : Color.green;
        Gizmos.DrawSphere (transform.position, groundDistance);

        Gizmos.DrawRay (transform.position, externalMotion);
    }

}