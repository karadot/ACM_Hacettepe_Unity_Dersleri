using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField]
    int _CoinCount = 4;

    [SerializeField]
    GameObject door;

    [SerializeField]
    string completedMessage = "Kapı Açıldı";

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

    void ActivateDoor () {
        door.SetActive (true);
    }
}