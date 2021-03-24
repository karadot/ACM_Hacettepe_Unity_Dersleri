using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    [SerializeField][Range (45, 90)]
    float donusHizi = 5f;

    void Update () {
        transform.Rotate (0, donusHizi * Time.deltaTime, 0);
    }
}