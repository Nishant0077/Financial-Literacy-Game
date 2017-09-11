using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSchoolScene : MonoBehaviour {

    private string sceneToLoad = "Day2_School";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 8;
        SceneManager.LoadScene(sceneToLoad);
       
    }
}
