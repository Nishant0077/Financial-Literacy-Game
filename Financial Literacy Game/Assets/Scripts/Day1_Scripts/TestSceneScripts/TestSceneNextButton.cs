using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneNextButton : MonoBehaviour {

    private string sceneToLoad = "Day2_Scenario1";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
