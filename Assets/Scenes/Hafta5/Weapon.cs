using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//genel olarak silahlarda kullanacağımız temel-base sınıf
//hepsinde ihtiyacımız olan özellikleri ve fonksiyonları buraya eklememiz yeterli

public abstract class Weapon : MonoBehaviour {

    public bool CanShot = true;
    //abstract kelimesinin olduğu bu fonksiyon içerisinde komutlar bulunmuyor, dikkat ederseniz.
    //Burada yaptığımız şey temelde, Weapon sınıfından miras alan diğer objelerde bu sınıf muhakkak olmalı şeklinde bir belirtme aslında.
    public abstract void Shoot ();
}