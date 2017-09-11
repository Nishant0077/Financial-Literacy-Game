using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHourlyWageScene : MonoBehaviour {

    private string sceneToLoad = "HourlyWageScene";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 2;
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
