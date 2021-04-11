using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon {
    [SerializeField]
    LineRenderer laser;

    [SerializeField]
    GameObject impact;

    [SerializeField]
    float time = .3f;

    [SerializeField]
    Camera mainCamera;

    private void Start () {
        laser.enabled = false;
    }

    public override void Shoot () {
        StartCoroutine (StartShoot ());
    }

    IEnumerator StartShoot () {
        CanShot = false;
        laser.enabled = true;
        float timer = 0;
        while (timer < time) {
            laser.SetPosition (0, laser.transform.position);
            Ray ray = mainCamera.ViewportPointToRay (new Vector2 (.5f, .5f));
            Vector3 endPosition = transform.position + transform.forward * 100;
            if (Physics.Raycast (ray, out RaycastHit hit, 100)) {
                GameObject impactObj = Instantiate (impact, hit.point, Quaternion.identity);
                impactObj.transform.LookAt (hit.point + hit.normal);
                endPosition = hit.point;
                Destroy (impactObj, .5f);
            }
            laser.SetPosition (1, endPosition);
            yield return null;
            timer += Time.deltaTime;
        }
        laser.enabled = false;
        CanShot = true;
    }
}