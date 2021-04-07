using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    Text coinText, timeText;

    [SerializeField]
    GameObject InGamePanel, FinishPanel, HighScoreText;

    private void Awake () {
        InGamePanel.SetActive (true);
        FinishPanel.SetActive (false);
    }

    public void AltinTextGuncelle (int altinMiktari) {
        coinText.text = "Toplanacak Altın:" + altinMiktari;
    }

    public void AltinTextGuncelle (string message) {
        coinText.text = message;
    }

    public void TimeTextUpdate (string time) {
        timeText.text = time;
    }

    public void FinishUI (bool isNewHighScore) {
        InGamePanel.SetActive (false);
        FinishPanel.SetActive (true);
        HighScoreText.SetActive (isNewHighScore);
    }

}