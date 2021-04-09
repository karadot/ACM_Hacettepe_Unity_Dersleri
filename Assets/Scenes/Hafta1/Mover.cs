using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Objenin x ekseninde -10 ve 10 konumları arasında, belirtilen hızda gidip gelmesini sağlar.
public class Mover : MonoBehaviour {

    [SerializeField]
    float hareketHizi = 3f;
    //Kullanacağımız hareket vektörü
    Vector3 hareket;
    private void Start () {
        //başlangıçta hareket vektörümüzü oluşturuyoruz, 
        //dikkat edin, sadece x ekseninde hareketi istediğim için sadece ilk değişkene değer veriyorum, y ve z eksenleri için hareket değeri 0
        hareket = new Vector3 (hareketHizi, 0, 0);
    }
    //her bir karede(frame) çalışacak fonksiyonumuz
    void Update () {
        /*
        Temel olarak yaptığımız şey, x eksenindeki pozisyonu kontrol etmek
        eğer x'te 10 pozisyonunu aştıysak, hareketimiz -x yönünde olacak şekilde güncelliyoruz
        eğer x'te -10 pozisyonunu geçtiysek, bu sefer +x yönünde olacak şekilde güncelliyoruz.
        */
        if (transform.position.x > 10) {
            hareket = new Vector3 (-hareketHizi, 0, 0);
        } else if (transform.position.x < -10) {
            hareket = new Vector3 (hareketHizi, 0, 0);
        }
        // transform bileşeni içerisinde bulunan Translate verdiğimiz değer kadar objenin pozisyonunu değiştirmek mümkün.
        transform.Translate (hareket * Time.deltaTime);
        //isterseniz aşağıdaki satırlarda olduğu gibi direkt pozisyonu da değiştirebilirsiniz. 2. satırdaki yazım, 1. satırdakinin kısaltılmış hali diyebiliriz.
        //transform.position = transform.position + hareket * Time.deltaTime;
        //transform.position += hareket * Time.deltaTime;

    }
}