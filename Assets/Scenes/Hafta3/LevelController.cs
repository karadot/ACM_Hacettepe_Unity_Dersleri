using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    //Sahnede tamamlanması gereken hedef sayısı
    [SerializeField]
    int _remainingTargets = 4;
    //Oyun zamanını tutacağımız değişken
    float gameTime = 0;

    //oyun sonunda aktif olacak obje
    [SerializeField]
    GameObject door;
    //farklı durumlarda gösterilecek mesaj metinleri
    [SerializeField]
    string completedMessage = "Kapı Açıldı", targetMessage = "Toplanacak Altın Sayısı";

    //karakter kontrollerine erişip kapatıp açabilmek için karakter objemizi tuttuğumuz değişken
    [SerializeField]
    GameObject player;

    //Coin toplama tamamlandı mı tamamlanmadı mı durumunu tutuyoruz
    bool isCompleted = false;

    //Hedef sayısını properties üzerinden değiştirerek, duruma göre farklı işlemler gerçekleştirebiliyoruz.
    public int TargetCount {
        get => _remainingTargets;
        set {
            if (value <= 0) {
                _remainingTargets = 0;
                ActivateDoor ();
                uiController.UpdateTargetText (completedMessage);
            } else {
                _remainingTargets = value;
                uiController.UpdateTargetText (targetMessage, _remainingTargets);
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

        door = GameObject.FindWithTag ("Finish");
        if (door == null) {
            Debug.LogWarning ("Can't find door");
            return;
        }
        player = GameObject.FindWithTag ("Player");
        if (player == null) {
            Debug.LogWarning ("Can't find player");
            return;
        }

        door.SetActive (false);

        uiController = GetComponent<UIController> ();

        StartCoroutine (StartGame ());

    }

    IEnumerator StartGame () {
        ActivatePlayerController (false);
        uiController.ActivateCountDownTimer (true);
        for (int i = 3; i >= 0; i--) {
            uiController.SetCountDownTimerText (i.ToString ());
            yield return new WaitForSeconds (1f);
        }
        uiController.SetCountDownTimerText ("Başla");
        yield return new WaitForSeconds (1f);
        uiController.ActivateCountDownTimer (false);
        uiController.UpdateTargetText (targetMessage, _remainingTargets);
        ActivatePlayerController (true);
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
        //player objesini kontrollerini deaktif hale getiriyoruz.
        ActivatePlayerController (false);
        //fareyi görünür hale getirip kilidini kaldırıyoruz.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ActivatePlayerController (bool isActive) {
        player.GetComponent<PlayerMovement> ().enabled = isActive;
        player.GetComponent<PlayerLook> ().enabled = isActive;
        player.GetComponent<WeaponManager> ().enabled = isActive;
    }

    public void ReloadCurrentScene () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

    public void LoadNextScene () {
        int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
        if (nextSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
            nextSceneIndex = 0;
        SceneManager.LoadScene (nextSceneIndex);
    }
}