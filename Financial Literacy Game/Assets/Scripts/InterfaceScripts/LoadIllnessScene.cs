using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIllnessScene : MonoBehaviour {

    private string sceneToLoad = "Day2_IllnessScene";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 7;
        SceneManager.LoadScene(sceneToLoad);

    }
}
