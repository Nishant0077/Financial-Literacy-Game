using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHourlyWageScene : MonoBehaviour {

    private string sceneToLoad = "HourlyWageScene";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
