using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour {
    private void OnTriggerEnter (Collider other) {
        LevelController.Instance.Finished ();
        /*
        if (other.CompareTag ("Player")) {
            Scene currentScene = SceneManager.GetActiveScene ();
            int sceneIndex = currentScene.buildIndex + 1;
            if (sceneIndex < SceneManager.sceneCountInBuildSettings) {
                SceneManager.LoadScene (sceneIndex);
            }
        }*/
    }
}