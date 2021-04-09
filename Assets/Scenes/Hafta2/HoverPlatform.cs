using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger alanı içerisine giren rigidbody veya playerMovement içeren objeleri havada süzülecek şekilde hareket ettirmeyi sağlayan script.
public class HoverPlatform : MonoBehaviour {

    //hareketi sağlarken kullanacağımız kuvvet çarpanı
    [SerializeField]
    float force = 5f;

    //bu objeden ne kadar yüksekte bir alana çıkaracağız ve nasıl bir aralıkta süzülme hareketini sağlayacağımızı tuttuğumuz değişkenler
    [SerializeField]
    float hoverHeight = 5, hoverRange = 2;

    //3 boyutlu dünya üzerinde süzülme hareketinin olacağı merkez pozisyon verisini tutan değişken
    Vector3 hoverCenter;

    //Başlangıçta yine hoverCenter değişkenimizin atamasını yapıyoruz. 
    private void Start () {
        SetHoverCenter ();
    }

    //OnValidate fonksiyonu aynı Start-Update gibi Unity'nin verdiği bir fonksiyon.
    //Editör üzerinde değişiklik yaptığımızda çalışmasını istediğimiz kodları buraya yazıyoruz.
    //Burada kullanma sebebim, 3 boyutlu pozisyon verisinin yüksekliğe göre oluşturulması. Editör üzerinde yüksekliği değiştirdiğimde bu da güncellensin istiyorum.
    private void OnValidate () {
        SetHoverCenter ();
    }

    //merkez noktayı girilen yüksekliğe göre hesaplatıyoruz
    void SetHoverCenter () {
        hoverCenter = transform.position + transform.up * hoverHeight;
    }

    //Trigger alanı içerisinde kalan objelere sürekli olarak kuvvet uygulayarak,
    // hoverCenter konumuna doğru hareket etme ve belirli aralıkta süzülmesini sağlıyoruz
    private void OnTriggerStay (Collider other) {
        //Öncelikle karakterimizi ne kadar hareket ettirmemiz gerekiyor, bunun yönünü elde ediyoruz.
        //Vektör hesabına dayanıyor buradaki mantık, sayı doğrusundan 4 noktasından 7 noktasına gitmek istediğimizde yaptığımız şey aradaki farkı bulma
        //ve hangi yönde olduğunu bilmek, 7(gitmek istediğimiz konum)-4(o anki konum) şeklinde hesabı yapabiliyoruz. Sonuç 3
        //Tam tersini düşünürsek, yani 7'den 4'e gitmek istersek bu sefer
        //4-7 işlemini gerçekleştirirsek, -3 değerini elde ediyoruz. - yönde 3 birim gideceğiz yani.
        Vector3 hoverMotion = hoverCenter - other.transform.position;
        //Normalize fonksiyonu bir vektörün uzunluğunu 1 birim hale getirmemizi sağlayan bir fonksiyon.
        //Bu sayede, mesafe nolursa olsun, sabit bir hareket değeri elde etmiş oluyoruz
        hoverMotion.Normalize ();
        //aralarındaki mesafeyi ölçüyoruz. Distance fonksiyonu bunu sağlıyor.
        float distance = Vector3.Distance (other.transform.position, hoverCenter);

        Rigidbody otherRigidbody = other.GetComponent<Rigidbody> ();

        //eğer rigidboy bileşenine sahipse
        if (otherRigidbody != null) {
            //eğer aralarındaki mesafe, belirlediğimiz aralıktan daha fazla ise direkt velocity değerine müdahale ediyoruz
            if (distance > hoverRange) {
                otherRigidbody.velocity = hoverMotion * force;
            } //eğer mesafe, aralıktan kısa ise, yani objemizin aralığın içinde ise, sadece kuvvet uyguluyoruz artık.
            else {
                otherRigidbody.AddForce (hoverMotion * force);
            }
        }

        PlayerMovement controller = other.GetComponent<PlayerMovement> ();
        //Aynı rigidbody içerisinde olduğu gibi bir mantık uyguluyoruz burada da.
        //Ekstra olarak yaptığım, DrawRay ile çizgi çekip, karakterin hangi yöne doğru bir kuvvete sahip olacağını görmek
        if (controller != null) {
            Debug.DrawRay (other.transform.position, hoverMotion);
            if (distance > hoverRange) {
                controller.SetExternalMotion (hoverMotion * force, false);
            } else {
                controller.AddExternalMotion (hoverMotion * force);
            }
        }
    }

    /*
    Trigger alanına bir obje girdiğinde çalışacak komut, 
    burada eğer alana giren objenin PlayerMovement script'i varsa, 
    bunun yer çekimi seçeneğini kontrol ettiğimiz fonksiyonu çağırıp, yerçekimini kapatıyoruz.
    */
    private void OnTriggerEnter (Collider other) {
        PlayerMovement controller = other.GetComponent<PlayerMovement> ();

        if (controller != null) {
            controller.UseGravity (false);
        }
    }
    /*
    Trigger alanından bir obje çıktığında çalışacak komut
     burada eğer dışarı çıkan objede PlayerMovement script'i varsa, 
    bunun yer çekimi seçeneğini kontrol ettiğimiz fonksiyonu çağırıp, tekrar yer çekimini aktif hale getiriyoruz.
    */
    private void OnTriggerExit (Collider other) {
        PlayerMovement controller = other.GetComponent<PlayerMovement> ();
        if (controller != null) {
            controller.UseGravity (true);
        }

    }

    //OnDrawGizmos fonksiyonu, belirttiğimiz şekilde sahnede ikonlar, basit görseller çizmemizi sağlar
    //Ben bu fonksiyon içerisinde hoverCenter konumunu bir küre oluşturarak çizme işlemini gerçekleştirdim.
    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (hoverCenter, .25f);
    }

}