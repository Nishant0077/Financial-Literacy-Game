using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSavingsAmountScene : MonoBehaviour {

    private string sceneToLoad = "SavingsAmountScene";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 3;
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
