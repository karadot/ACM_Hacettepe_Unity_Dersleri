using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//UI Objelerimizi kontrol etmek için oluşturduğumuz script
public class UIController : MonoBehaviour {

    //Coin ve Time değerlerini yazmak için kullanacağımız değişkenler
    [SerializeField]
    Text coinText, timeText, countDownTimer;

    //Oyun içi ve oyun sonu durumlarda aktif-deaktif edeceğimiz objeler
    [SerializeField]
    GameObject InGamePanel, FinishPanel, HighScoreText;

    //Başlangıçta oyun-içi panelimizi aktif edip, oyun sonu paneli deaktif hale getiriyoruz.
    private void Awake () {
        InGamePanel.SetActive (true);
        FinishPanel.SetActive (false);
        ClearAllTexts ();
    }

    void ClearAllTexts () {
        coinText.text = "";
        timeText.text = "";
        countDownTimer.text = "";
    }
    //Coin metnini güncellemek için int tipinde değer alan fonksiyon
    public void UpdateCoinText (int altinMiktari) {
        coinText.text = "Toplanacak Altın:" + altinMiktari;
    }
    //Coin metnini güncellemek için string tipinde değer alan değişken. Dikkat ederseniz bir önceki fonksiyon ile aynı isme sahip
    //Bu sayede aynı isimdeki fonksiyonları farklı değişkenlerle kullanabiliyoruz.
    public void UpdateCoinText (string message) {
        coinText.text = message;
    }
    //Time metnini güncellemek için kullandığımız değişken
    public void TimeTextUpdate (string time) {
        timeText.text = time;
    }

    //Oyun sonunda çağırdığımız fonksiyon, oyuniçi paneli deaktif edip, oyun sonu paneli aktif hale getiriyoruz
    public void FinishUI (bool isNewHighScore) {
        InGamePanel.SetActive (false);
        FinishPanel.SetActive (true);
        //Ayrıca, yeni bir yüksek skor durumunda, ekstra bir obje gösteriyoruz.
        HighScoreText.SetActive (isNewHighScore);
    }

    public void ActivateCountDownTimer (bool isActive) {
        countDownTimer.gameObject.SetActive (isActive);
    }

    public void SetCountDownTimerText (string message) {
        countDownTimer.text = message;
    }

}