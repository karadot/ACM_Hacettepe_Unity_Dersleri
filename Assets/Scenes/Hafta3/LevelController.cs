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
        StartCoroutine (StartGame ());
    }

    IEnumerator StartGame () {

        uiController.ActivateCountDownTimer (true);
        for (int i = 3; i >= 0; i--) {
            uiController.SetCountDownTimerText (i.ToString ());
            yield return new WaitForSeconds (1f);
        }
        uiController.SetCountDownTimerText ("Başla");
        yield return new WaitForSeconds (1f);
        uiController.ActivateCountDownTimer (false);
        while (!isCompleted) {
            GameTime += Time.deltaTime;
            yield return null;
        }
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
        bool isNewHighScore = ScoreManager.SaveScore (sceneName, gameTime);
        //oyun sonu panelimizi açması için uiController fonksiyonunu çağırıyoruz
        uiController.FinishUI (isNewHighScore);
        //player objesini deaktif hale getiriyoruz.
        player.SetActive (false);
        //fareyi görünür hale getirip kilidini kaldırıyoruz.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}