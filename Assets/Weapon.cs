using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float bulletForce = 50f;

    [SerializeField]
    Transform gunEnd;

    private void Update () {
        if (Input.GetMouseButtonDown (0)) {
            GameObject newBullet = Instantiate (bullet, gunEnd.position, gunEnd.rotation);
            Rigidbody BulletRigidbody = newBullet.GetComponent<Rigidbody> ();
            BulletRigidbody.AddForce (newBullet.transform.forward * bulletForce, ForceMode.Impulse);
        }
    }

}