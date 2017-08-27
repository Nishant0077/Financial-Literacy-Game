using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScamScene : MonoBehaviour {

    private string sceneToLoad = "Day2_Scam";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
