using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEmployerScene : MonoBehaviour {

    private string sceneToLoad = "Day2_Employer";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
