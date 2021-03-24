using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {
    float yatayDonus = 90;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    float angleLimit = 60;

    public bool yInverted = false;

    float camRotation = 0;
    // Start is called before the first frame update
    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update () {
        float xInput = Input.GetAxis ("Mouse X");
        transform.Rotate (0, yatayDonus * Time.deltaTime * xInput, 0);

        float yInput = Input.GetAxis ("Mouse Y");
        camRotation += yInverted?yInput: -yInput;
        camRotation = Mathf.Clamp (camRotation, -angleLimit, angleLimit);
        cameraTransform.localRotation = Quaternion.Euler (camRotation, 0, 0);
    }
}