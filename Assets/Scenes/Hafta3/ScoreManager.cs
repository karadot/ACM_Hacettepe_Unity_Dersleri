using UnityEngine;

public static class ScoreManager {

    public static bool SaveScore (string sceneName, float score) {
        //Sahne için daha önce score kaydedildiyse bunu okuyoruz, eğer kayıt yoksa Mathf.Infinity ile çok büyük-sonsuza denk bir sayı vermesini belirtiyoruz
        float currHighScore = GetHighScore (sceneName);
        //eğer kaydedilmiş süreden daha kısa sürede oyun tamamlandıysa, yeni yüksek skor elde yapılmış demektir
        bool isNewHighScore = score < currHighScore;
        //yeni yüksek skorumuzu kaydediyoruz.
        if (isNewHighScore || currHighScore != Mathf.Infinity)
            PlayerPrefs.SetFloat (sceneName, score);
        return isNewHighScore;
    }
    //Belirtilen isimdeki float tipindeki score değerini elde etmemizi sağlar
    public static float GetHighScore (string sceneName) {
        return PlayerPrefs.GetFloat (sceneName, Mathf.Infinity);
    }
}