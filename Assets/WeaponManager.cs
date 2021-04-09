using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Silahımızı kontrol etmek ve buna uygun animasyonları oynatmak için kullandığımız fonksiyon.
public class WeaponManager : MonoBehaviour {
    //Weapon bileşenine sahip objemizi tuttuğumuz değişken
    [SerializeField]
    Weapon weapon;

    [SerializeField]
    Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();
    }

    void Update () {
        //Sol mouse tıklandıysa ve silah ateş edebilir durumdaysa, animasyonumuzu aktif hale getiriyoruz.
        if (Input.GetMouseButtonDown (0) && weapon.CanShot) {
            animator.SetTrigger ("Shoot");
        }
    }

    //Animation Event olarak kullandığımız fonksiyon. Bu sayede animasyonun belirli bir anında atışı gerçekleştirmemiz mümkün.
    public void FireWeapon () {
        weapon.Shoot ();
    }
}