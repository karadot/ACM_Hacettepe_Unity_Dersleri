using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    //Sahnede toplanması gereken coin sayisi
    [SerializeField]
    int _CoinCount = 4;
    //Oyun zamanını tutacağımız değişken
    float gameTime = 0;

    //oyun sonunda aktif olacak obje
    [SerializeField]
    GameObject door;
    //altın toplama sonucunda gösterilecek metin
    [SerializeField]
    string completedMessage = "Kapı Açıldı";

    //karakter kontrollerine erişip kapatıp açabilmek için karakter objemizi tuttuğumuz değişken
    [SerializeField]
    GameObject player;

    //Coin toplama tamamlandı mı tamamlanmadı mı durumunu tutuyoruz
    bool isCompleted = false;

    //Coin sayısını properties üzerinden değiştirerek, duruma göre farklı işlemler gerçekleştirebiliyoruz.
    public int CoinCount {
        get => _CoinCount;
        set {
            if (value <= 0) {
                _CoinCount = 0;
                ActivateDoor ();
                uiController.UpdateCoinText (completedMessage);
            } else {
                _CoinCount = value;
                uiController.UpdateCoinText (_CoinCount);
            }
        }
    }

    //Zamanı da properties üzerinden değiştiriyoruz. 
    float GameTime {
        get => gameTime;
        set {
            gameTime = value;
            uiController.TimeTextUpdate (gameTime.ToString ());
        }
    }

    public static LevelController Instance;

    UIController uiController;

    //Başlangıç için temel ayarları yapıp, singleton objemizi oluşturuyoruz.
    private void Awake () {
        if (Instance != null) {
            Destroy (this);
        }
        Instance = this;
        door.SetActive (false);
        uiController = GetComponent<UIController> ();
        uiController.UpdateCoinText (_CoinCount);
    }

    private void Update () {
        //Tamamlanmadığı sürece zamanı güncellemeye devam ediyoruz
        if (!isCompleted)
            GameTime += Time.deltaTime;
    }

    //oyun sonu objesini-kapıyı aktif hale getiriyoruz.
    void ActivateDoor () {
        door.SetActive (true);
    }

    //oyun tamamlandığında 
    public void Finished () {

        isCompleted = true;
        //Playerprefs üzerinden veri okumak için sahne adını elde ediyoruz
        string sceneName = SceneManager.GetActiveScene ().name;
        //daha sonra sahne adının sonuna _score yazısını ekliyoruz. 
        sceneName += "_score";
        //Sahne için daha önce score kaydedildiyse bunu okuyoruz, eğer kayıt yoksa Mathf.Infinity ile çok büyük-sonsuza denk bir sayı vermesini belirtiyoruz
        float currScore = PlayerPrefs.GetFloat (sceneName, Mathf.Infinity);
        //eğer kaydedilmiş süreden daha kısa sürede oyun tamamlandıysa, yeni yüksek skor elde yapılmış demektir
        bool isNewHighScore = gameTime < currScore;
        //yeni yüksek skorumuzu kaydediyoruz.
        if (isNewHighScore || currScore != Mathf.Infinity)
            PlayerPrefs.SetFloat (sceneName, gameTime);
        //oyun sonu panelimizi açması için uiController fonksiyonunu çağırıyoruz
        uiController.FinishUI (isNewHighScore);
        //player objesini deaktif hale getiriyoruz.
        player.SetActive (false);
        //fareyi görünür hale getirip kilidini kaldırıyoruz.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}