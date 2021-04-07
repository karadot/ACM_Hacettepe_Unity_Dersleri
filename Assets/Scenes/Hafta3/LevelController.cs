using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [SerializeField]
    int _CoinCount = 4;

    float gameTime = 0;

    [SerializeField]
    GameObject door;

    [SerializeField]
    string completedMessage = "Kapı Açıldı";

    [SerializeField]
    GameObject player;

    bool isCompleted = false;

    public int CoinCount {
        get => _CoinCount;
        set {
            if (value <= 0) {
                _CoinCount = 0;
                ActivateDoor ();
                uiController.AltinTextGuncelle (completedMessage);
            } else {
                _CoinCount = value;
                uiController.AltinTextGuncelle (_CoinCount);
            }

        }
    }

    float GameTime {
        get => gameTime;
        set {
            gameTime = value;
            uiController.TimeTextUpdate (gameTime.ToString ());
        }
    }

    public static LevelController Instance;

    UIController uiController;

    private void Awake () {
        if (Instance != null) {
            Destroy (this);
        }
        Instance = this;
        door.SetActive (false);
        uiController = GetComponent<UIController> ();
        uiController.AltinTextGuncelle (_CoinCount);
    }

    private void Update () {
        if (!isCompleted)
            GameTime += Time.deltaTime;
    }

    void ActivateDoor () {
        door.SetActive (true);
    }

    public void Finished () {
        isCompleted = true;
        string sceneName = SceneManager.GetActiveScene ().name;
        sceneName += "_score";
        float currScore = PlayerPrefs.GetFloat (sceneName, Mathf.Infinity);
        bool isNewHighScore = gameTime < currScore;
        PlayerPrefs.SetFloat (sceneName, gameTime);
        uiController.FinishUI (isNewHighScore);
        player.SetActive (false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}