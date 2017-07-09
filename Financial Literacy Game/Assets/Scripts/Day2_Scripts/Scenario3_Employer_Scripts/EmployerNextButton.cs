using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmployerNextButton : MonoBehaviour {

    string sceneToLoad = "Day2_School";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
