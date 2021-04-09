using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Basit bir menü kontrolcüsü
public class MainMenuController : MonoBehaviour {

    //Arayüz animasyonlarını içeren animator bileşenini tuttuğumuz değişken
    [SerializeField]
    Animator canvasAnimator;

    //Butonlara atayabileceğimiz, animator içerisindeki "Scenes" adındaki parametreyi değiştiren fonksiyon
    public void ChangePanel (bool isScenesPanel) {
        canvasAnimator.SetBool ("Scenes", isScenesPanel);
    }
    //Butonlara atayabileceğimiz, verilen isimdeki sahneyi yüklememizi sağlayan fonksiyon.
    public void LoadScene (string sceneName) {
        SceneManager.LoadScene (sceneName);
    }
}