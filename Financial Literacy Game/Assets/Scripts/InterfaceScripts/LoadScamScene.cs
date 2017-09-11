using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScamScene : MonoBehaviour {

    private string sceneToLoad = "Day2_Scam";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 6;
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
