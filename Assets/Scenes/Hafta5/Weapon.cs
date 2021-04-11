using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public bool CanShot = true;
    public abstract void Shoot ();
}