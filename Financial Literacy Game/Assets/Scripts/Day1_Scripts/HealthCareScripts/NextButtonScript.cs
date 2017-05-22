using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButtonScript : MonoBehaviour {

    string sceneToLoad = "Day2_Scenario1";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
