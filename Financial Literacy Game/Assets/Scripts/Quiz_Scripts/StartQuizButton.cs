using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartQuizButton : MonoBehaviour {

    string sceneToLoad = "scene0";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
        PlayerPrefs.SetString("PlayerID", System.DateTime.Now.ToString());
    }
}
