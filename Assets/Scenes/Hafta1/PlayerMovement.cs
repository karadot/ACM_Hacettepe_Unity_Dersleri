using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    float speed = 5f, height = 2f, gravity = -9.81f;

    [SerializeField]
    float groundDistance = .1f;

    [SerializeField]
    Vector3 motion, verticalMotion;
    [SerializeField]
    Vector3 externalMotion = Vector3.zero;

    float jumpVelocityY;

    CharacterController controller;

    [SerializeField]
    bool isGrounded = false, useGravity = true;

    [SerializeField]
    LayerMask groundLayers;

    //PlayerMovement scripti aktif olduğu anda çalışacak Start fonksiyonu
    //içerisinde CharacterController adlı scripte erişiyoruz.
    //ardından, sık sık kullanacağımız zıplama değerini baştan hesaplatıyoruz
    private void Start () {
        controller = GetComponent<CharacterController> ();
        //şayet yüksekliğini oyun içerisinde değiştirmeyi isterseniz, bu komutu tekrar kullanmanız gerekir. 
        jumpVelocityY = Mathf.Sqrt (height * -2 * gravity);
    }
    // Update is called once per frame
    void Update () {
        //oyuncudan yatay(A-D) ve dikey (W-S) tuş basım değerlerini okuyoruz
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        //hareket değerimizi, objenin şu anki halinin sağı ve ileri yönüne göre oluşturuyoruz.
        motion = transform.right * x + transform.forward * z;
        //ardından oluşturduğumuz hareket değerini hız ve deltaTime ile çarpıyoruz. deltaTime ile çarparak 1 karede ne kadar hareket etmesi gerektiğini de hesaplamış oluyoruz.
        motion *= (speed * Time.deltaTime);

        /*
        Physics sınıfının içerisinde bulunan CheckSphere ile belirli bir alanın içerisinde obje var mı yok mu kontrol etmemiz mümkün
        Bu yapıyı kullanarak, karakter yere değiyor mu bunu da gayet kontrol edebiliriz.
        ilk değişken, hayali küremizin merkezini belirtiyor, 2. değişken küremiz ne kadar geniş olacak, yarıçapı yani
        3. değişken ise hangi layer içerisindeki objeleri kontrol edeceğiz, bunu belirttiğimiz değişken. 
        Dokümanı muhakkak inceleyin derim, örneğin her layer kontrol edilsin istiyorsanız 3. değişkeni yazmasanız da olur
        */
        isGrounded = Physics.CheckSphere (transform.position, groundDistance, groundLayers);

        //Basitçe "eğer yere değiyorsak ve dikey hareketimiz aşağı doğruysa" diyoruz
        if (isGrounded && verticalMotion.y < 0) {
            //dikey hareketi -2f gibi belirlediğimiz bir değere sabitliyoruz.
            verticalMotion.y = -2f;
            //eğer düşüyorsak ve yere değdiysek, dış kuvvetler varsa bunları da sıfırlıyoruz.
            if (externalMotion != Vector3.zero)
                externalMotion = Vector3.zero;
        }

        //Yer çekimi aktif olarak belirlediysek, dikey hareketimizden yerçekimi değerini çıkararak aşağı yönlü hareket etmesini sağlıyoruz.
        if (useGravity) {
            verticalMotion.y += Time.deltaTime * gravity;
        }
        //Eğer Space tuşuna basıldıysa ve karakter yerdeyse, vertical motion değerimizi daha önce hesapladığımız zıplama için gerekli kuvvet değerine eşitliyoruz.
        if (Input.GetKeyDown (KeyCode.Space) && isGrounded) {
            verticalMotion.y = jumpVelocityY;
        }

        //eğer dış kuvvetler 0 değilse, zamanla bunu da azaltmak istiyoruz, ki sürekli aynı yöne doğru hareket etmesin, bir nevi hayali bir sürtünme ile dış etkiyi azaltıyoruz diyebiliriz.
        if (externalMotion != Vector3.zero) {
            externalMotion.x -= Time.deltaTime * Mathf.Sign (externalMotion.x);
            externalMotion.z -= Time.deltaTime * Mathf.Sign (externalMotion.z);
            //Bu 2 satırda yaptığımız ise basitçe "şayet x ve z değerleri 1den daha ufaksa, direkt sıfırla." böylece 
            externalMotion.x = Mathf.Abs (externalMotion.x) < 1f?0 : externalMotion.x;
            externalMotion.z = Mathf.Abs (externalMotion.z) < 1f?0 : externalMotion.z;
        }

    }
    //Belirtilen yüksekliğe zıplamayı sağlayacak Vector3 tipi hareketi hesaplar ve dikey harekete atama yapar.
    public void DoJump (float jumpHeight) {
        float target = Mathf.Sqrt (jumpHeight * (-2.0f) * gravity);
        //NaN hatası alma durumuna karşı kontrol sağlayıp, hata durumunda 3 değerini standart olarak atıyoruz, bunu değiştirebilirsiniz.
        verticalMotion.y = float.IsNaN (target) ? 3 : target;
    }

    //FixedUpdate özellikle fizikle etkileşime gireceğimiz işlemleri yaptığımız fonksiyon
    //etraftaki objelerle fiziksel olarak etkileşimde kalacağımız için bu fonksiyon içerisinde charactercontroller ile hareket gerçekleştiriyoruz.
    private void FixedUpdate () {
        controller.Move (motion);
        controller.Move (externalMotion * Time.fixedDeltaTime);

        controller.Move (verticalMotion * Time.fixedDeltaTime);
    }

    //dikey hareket için kuvvet eklemesi yapıyoruz.
    public void AddVerticalVelocity (float y) {
        verticalMotion.y += y * Time.fixedDeltaTime;
    }
    //dikey harekete direkt atama yapıyoruz
    public void SetVerticalVelocity (float y) {
        verticalMotion.y = y;
    }
    //Diğer objelerden gelen kuvveti değişkenlere atıyoruz
    //Dikey hareket için yine verticalMotion kullanıyoruz, hali hazırda yer çekimi uyguladığımızdan işler biraz daha kolay oluyor böyle.
    public void SetExternalMotion (Vector3 extMotion, bool jump) {
        externalMotion.z = extMotion.z;
        externalMotion.x = extMotion.x;
        if (jump) {
            DoJump (extMotion.y);
        } else {
            SetVerticalVelocity (extMotion.y);
        }

    }
    //varolan dış kuvvetimize ekleme yapmak için kullandığımız fonksiyon.
    public void AddExternalMotion (Vector3 extMotion) {
        externalMotion.z += extMotion.z * Time.deltaTime;
        externalMotion.x += extMotion.x * Time.deltaTime;
        verticalMotion.y += extMotion.y * Time.deltaTime;
    }

    //Yer çekimini aktif-deaktif etmek için kullandığımız fonksiyon
    public void UseGravity (bool g) {
        useGravity = g;
        verticalMotion.y = 0;
    }

    //Ekranda ikonlar çizerek bazı verileri görsel hale getiriyoruz
    private void OnDrawGizmos () {
        //İkon rengi değiştirmek için renk kodu
        Gizmos.color = isGrounded?Color.red : Color.green;
        //Bir küre ikonu oluşturup, objenin konumunda, groundDistance adlı değişkendeki değere sahip yarıçapa sahip olacak şekilde çizdiriyoruz.
        Gizmos.DrawSphere (transform.position, groundDistance);
        //DrawRay ile bir ışın oluşturuyoruz, ilk değişken karakterin konumu, yani ışının başlangıç noktası, 
        //2. değişken de çizeceğimiz ışının yönü, burada externalMotion ile dışarıdan uygulanan kuvveti görselleştirmek istedim
        Gizmos.DrawRay (transform.position, externalMotion);
    }

}