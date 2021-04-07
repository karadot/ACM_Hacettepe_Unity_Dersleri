using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    Animator canvasAnimator;
    public void ChangePanel (bool isScenesPanel) {
        canvasAnimator.SetBool ("Scenes", isScenesPanel);
    }

    public void LoadScene (string sceneName) {
        SceneManager.LoadScene (sceneName);
    }
}