using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Silahımızı kontrol etmek ve buna uygun animasyonları oynatmak için kullandığımız fonksiyon.
public class WeaponManager : MonoBehaviour {
    //Weapon bileşenine sahip objemizi tuttuğumuz değişken
    [SerializeField]
    Weapon[] weapon;

    int activeWeapon = 0;

    [SerializeField]
    Animator animator;

    void Start () {
        animator = GetComponent<Animator> ();
        for (int i = 0; i < weapon.Length; i++) {
            weapon[i].gameObject.SetActive (false);
        }
        weapon[activeWeapon].gameObject.SetActive (true);
    }

    void Update () {
        //Sol mouse tıklandıysa ve silah ateş edebilir durumdaysa, animasyonumuzu aktif hale getiriyoruz.
        if (Input.GetMouseButtonDown (0) && weapon[activeWeapon].CanShot) {
            animator.SetTrigger ("Shoot");
        }
        float wheel = Input.GetAxis ("Mouse ScrollWheel");
        if (wheel > 0) {
            ChangeWeapon (true);
        } else if (wheel < 0) {
            ChangeWeapon (false);
        }
    }
    // 5/3 => 2

    //Animation Event olarak kullandığımız fonksiyon. Bu sayede animasyonun belirli bir anında atışı gerçekleştirmemiz mümkün.
    public void FireWeapon () {
        weapon[activeWeapon].Shoot ();
    }

    void ChangeWeapon (bool next) {
        weapon[activeWeapon].gameObject.SetActive (false);
        activeWeapon += next?1: -1;
        if (activeWeapon < 0) {
            activeWeapon = weapon.Length - 1;
        }
        activeWeapon = activeWeapon % weapon.Length;
        weapon[activeWeapon].gameObject.SetActive (true);
    }
}