using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Jumper : MonoBehaviour {

    Rigidbody rigidbody;

    [SerializeField]
    float height = 2;

    Vector3 jumpVector;

    private void Start () {
        rigidbody = GetComponent<Rigidbody> ();
        float y = Mathf.Sqrt (height * -2 * Physics.gravity.y);
        jumpVector.y = y;
    }
    void FixedUpdate () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            rigidbody.velocity = jumpVector;
        }
    }
}