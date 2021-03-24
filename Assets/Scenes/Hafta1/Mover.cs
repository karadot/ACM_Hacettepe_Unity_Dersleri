using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    [SerializeField]
    float hareketHizi = 3f;
    Vector3 hareket;
    private void Start () {
        hareket = new Vector3 (hareketHizi, 0, 0);
    }
    // Update is called once per frame
    void Update () {

        if (transform.position.x > 10) {
            hareket = new Vector3 (-hareketHizi, 0, 0);
        } else if (transform.position.x < -10) {
            hareket = new Vector3 (hareketHizi, 0, 0);
        }
        //transform.position += hareket * Time.deltaTime;
        //transform.position = transform.position + hareket;
        transform.Translate (hareket * Time.deltaTime);

    }
}