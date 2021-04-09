using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Space tuşuna basıldığında objeyi belirtilen yüksekliğe zıplatan script 
[RequireComponent (typeof (Rigidbody))]
public class Jumper : MonoBehaviour {

    Rigidbody rigidbody;

    [SerializeField]
    float height = 2;

    Vector3 jumpVector;
    //Başlangıç içerisinde bileşenimize erişiyoruz,
    private void Start () {
        rigidbody = GetComponent<Rigidbody> ();
        // belirtilen yüksekliğe zıplatmak için ihtiyacımız olan Vector3 tipindeki değişkenin y ekseni (yukarı-aşağı) için gerekli kuvveti hesaplıyoruz.
        //kullandığımız formül fizikten geliyor. C# bilmiyorsanız gözünüz korkmasın,
        //basitçe yükseklik*-2*yerçekimi.y, bu değerleri çarparak yerçekimine karşı bir kuvvet elde ediyoruz. Unutmadan karekökünü alıyoruz.  
        float y = Mathf.Sqrt (height * -2 * Physics.gravity.y);
        jumpVector.y = y;
    }
    void FixedUpdate () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            //rigidbody ile hareket için AddForce gibi çeşitli yöntemler bulunuyor, ben burada velocity yani o anki hareket değerini direkt değiştirmeyi tercih ettim.
            rigidbody.velocity = jumpVector;
        }
    }
}