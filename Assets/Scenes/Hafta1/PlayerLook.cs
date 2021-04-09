using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
basit bir fps camera kontrolcüsü
bu scripti karaktere atayın, kamerayı da karakterin alt objesi olarak atayın, istediğini şekilde kamerayı konumlandırın
*/
public class PlayerLook : MonoBehaviour {

    float yatayDonus = 90;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    float angleLimit = 60;

    public bool yInverted = false;

    float camRotation = 0;

    //başlangıçta fareyi kilitleyip görünmez hale getiriyoruz
    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update () {
        //mouse sağ sol hareketlerini xInput adlı değişkene atıyoruz, ardından objemizi bu veriye göre döndürüyoruz
        float xInput = Input.GetAxis ("Mouse X");
        transform.Rotate (0, yatayDonus * Time.deltaTime * xInput, 0);

        //mouse ileri geri hareketlerini yInput adlı değişkene atıyoruz.
        float yInput = Input.GetAxis ("Mouse Y");
        //eğer y değerini ters almak istiyorsak buna göre negatif halini camRotation değerimize ekliyoruz
        camRotation += yInverted?yInput: -yInput;
        //camRotation için bir kontrol yapıyoruz, çok fazla aşağı veya yukarıya döndürülmesini engelliyoruz böylece. 
        //Clamp ile yaptığımız camRotation değerini 2 değer arasında kalmasını sağlamak
        camRotation = Mathf.Clamp (camRotation, -angleLimit, angleLimit);
        /*
        Aşağı yukarı bakarken kullandığımız eksen şu durumda x, yani soldan sağa bir çubuk geçirmişiz de kameramızı buna göre döndürüyoruz diye düşünebilirsiniz.
        her ne kadar vector3 olarak görsek de, hem rotationiçin aslında unity'nin Quaternion kullanmasından
        hem de daha doğru hesaplamalar yapmamızı sağladığı için Quaternion sınıfından yararlanıyoruz. Her eksende olması gereken açıyı verdiğimiz Euler fonksiyonu ile
        kamerayı ayarlıyoruz.
        */
        cameraTransform.localRotation = Quaternion.Euler (camRotation, 0, 0);
    }
}