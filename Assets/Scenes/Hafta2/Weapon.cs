using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Verdiğimiz oyun objesini oluşturan ve belirttiğimiz konum ve yönde, belirttiğimiz kuvvetle fırlatan script
public class Weapon : MonoBehaviour {
    //Fırlatmak istediğimiz mermi prefabı
    [SerializeField]
    GameObject bullet;
    //Fırlatma için kullanacağımız kuvvet
    [SerializeField]
    float bulletForce = 50f;

    //Merminin yaratılacağı konum ve rotasyon bilgisini referans alacağı obje
    [SerializeField]
    Transform gunEnd;

    //Ateş edip edemeyeceği durumunu tuttuğumuz değişken
    public bool CanShot = true;

    //Belirtilen bullet objesini yaratıp rigidbody bileşenine erişerek kuvvet ekleyen kod
    public void Shoot () {
        /*
        Instantiate komutu tam olarak obje yaratma işlemini gerçekleştiriyor. 
        isterseniz gördüğünüz gibi, yaratma sonucunda oluşan objeyi GameObject tipinde bir değişkende tutabiliyorsunuz. 
        Ama bu bir seçenek, isterseniz sadece Instantiate diyebilirsiniz.
        ilk değişkenimiz bullet, yaratılacak obje, diğer değişkenler de pozisyon ve yön bilgileri.
        Eğer yarattığımız obje, child obje olarak sahneye eklensin isterseniz 
        direkt olarak gunEnd Transform bileşenini verebilirsiniz.
        Örn: Instantiate(bullet,gunEnd);
        Bu sayede child obje haline gelecektir. Ancak bir mermi yarattığımız için bu konumda bunu kullanmadım ben.
        Daha fazla detay için dokümanlara muhakkak göz atın.
        */
        GameObject newBullet = Instantiate (bullet, gunEnd.position, gunEnd.rotation);
        //Rigidbody bileşenini elde ediyoruz.
        Rigidbody BulletRigidbody = newBullet.GetComponent<Rigidbody> ();
        //Eğer elde etmeye çalıştığımız bileşen yoksa, BulletRigidbody null değere sahip olacaktır.
        //Bu durumda kendi elimizle AddComponent<Rigidbody>(); ile biz bu bileşeni objeye ekliyoruz. 
        if (BulletRigidbody == null)
            BulletRigidbody = newBullet.AddComponent<Rigidbody> ();
        /*
        Burada newBullet.transform.forward ile merminin ileri yönünü gösteren bir vektör elde ediyoruz, 
        çarpma işlemi ile de bu vektörü büyütüyoruz. 
        Örneğin: (0,0,1) vektörü elde edersek ve bunu 50 ile çarparsak, yeni vektör (0,0,50) olacak
        ddforce ile ekleyeceğimiz kuvvet Z ekseninde 50 birimlik bir kuvvet olacak yani.
        ForceMode için farklı seçenekler bulunuyor, deneyip görmenizi isterim. 
        Ayrıca dokümanlarda daha detaylı bilgi de bulabilirsiniz.
        */
        BulletRigidbody.AddForce (newBullet.transform.forward * bulletForce, ForceMode.Impulse);
    }
}