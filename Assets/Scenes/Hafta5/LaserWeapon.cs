using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Asıl Weapon sınıfından miras alan LaserWeapon sınıfı
public class LaserWeapon : Weapon {

    //Lazer görüntüsünü oluşturmak için kullanacağımız LineRenderer bileşeni
    [SerializeField]
    LineRenderer laser;

    //Lazerin değdiği noktalarda efekt oluşturmak için kullanacağımız particle system
    [SerializeField]
    ParticleSystem impact;

    //lazerin çalışma süresi
    [SerializeField]
    float time = .3f;

    //Lazerin değdiği obje rigidbody içeriyorsa uygulayacağımız kuvvet değeri
    [SerializeField]
    float force = 10f;

    //ekranın merkezinden hedef alabilmek için ana kameramız
    [SerializeField]
    Camera mainCamera;

    private void Start () {
        laser.enabled = false;
        impact.Stop ();
    }

    public override void Shoot () {
        //ateş etme komutu geldiğinde coroutine fonksiyonumuzu çağırıyoruz.
        StartCoroutine (StartShoot ());
    }

    IEnumerator StartShoot () {
        //ateş edilebilirliği deaktif hale getiriyoruz
        CanShot = false;
        //lazer görünümü veren lineRenderer bileşenimizi aktif hale getiriyoruz
        laser.enabled = true;
        //ateş edilen süreyi tutmak için değişken
        float timer = 0;
        //lazerin değdiğini noktada kullanacağımız efekti, ilk başta olabilecek en uzak konuma yerleştiriyoruz
        impact.transform.position = transform.position + transform.forward * 100;
        //lazerin değdiği noktalarda efektin çalışması için Play fonksiyonunu çağırıyoruz
        impact.Play ();
        /*
        While ile döngüye sokuyoruz, süslü parantezler içerisindeki kodlar verdiğimiz koşul sağlandığı sürece çalışacak.
        bu durumda timer<time yani ateş etme toplam süresi, ateş edilebilir zamandan küçük olduğu sürece, ateş edeceğiz
        */
        while (timer < time) {
            //Line renderer bileşeninin başlangıç konumunu bileşenin ait olduğu objenin pozisyonuna ayarlıyoruz
            laser.SetPosition (0, laser.transform.position);
            //ekranın ortasından çıkıp baktığımız yöne doğru bir ışın oluşturuyoruz
            Ray ray = mainCamera.ViewportPointToRay (new Vector2 (.5f, .5f));

            Vector3 endPosition = transform.position + transform.forward * 100;

            //oluşturduğumuz ışın 100 birim uzaklığa kadar yolluyoruz. Herhangi bir obje algılarsa, sonuçları hit isimli değişkene atayacak ve if blogu çalışacak
            if (Physics.Raycast (ray, out RaycastHit hit, 100)) {
                //oluşturduğumuz rayin objeye değdiği noktayı elde edip, çarpışma efektimizin konumunu buna göre ayarlıyoruz
                impact.transform.position = hit.point;
                //parçacıkların doğru şekilde yön alabilmesi için, ray çarpışması sonucu elde ettiğimi yüzeyin baktığı yön olan normal konumuna bakmasını sağlıyoruz.
                //hit.point+hit.normal şeklinde belirtmemizin sebebi, hit.normal'in yalnızca yön bilgisi içermesi. 
                //LookAt fonksiyonunda ise, direkt olarak bakmasını istediğimiz pozisyonu vermemiz gerekiyor.
                impact.transform.LookAt (hit.point + hit.normal);
                //line renderer için vereceğimiz end position değerini de çarptığmız nokta olarak belirliyoruz.
                //Böylece linerenderer objesinin bulunduğu konumdan, çarpma noktasına bir çizgi çekebileceğiz
                endPosition = hit.point;
                //Ray-ışının çarptığı objede rigidbody varsa buna kuvvet uyguluyoruz.
                Rigidbody hitRgb = hit.rigidbody;
                if (hitRgb != null) {
                    hitRgb.AddForceAtPosition (transform.forward * 10, hit.point);
                }
            }
            //işlemler sonucuna göre işlediğimiz endPosition değerini lineRenderer bileşenimize çizgiyi oluşturan 2.nokta olarak iletiyoruz.
            laser.SetPosition (1, endPosition);
            /*
            Burada döndürdüğümü null değeri kabaca 1 kare(frame) bekle manasına geliyor.
            Tabii ki bu Coroutine kullandığımız, yani fonksiyonumuzun dönüş tipi IEnumarator olduğu için böyle.
            null dışında kullanabileceğiniz diğer örnek komutlar ise new WaitForSeconds(), new WaitForFixedUpdate()
            */
            yield return null;
            //ne kadar sürenin geçtiğini timer değişkenimize ekliyoruz, böylece geçen süreden haberdar olabiliyoruz.
            timer += Time.deltaTime;
        }
        //efektleri deaktif hale getirip, tekrar ateş edebilir duruma getiriyoruz.
        impact.Stop ();
        laser.enabled = false;
        CanShot = true;
    }
}