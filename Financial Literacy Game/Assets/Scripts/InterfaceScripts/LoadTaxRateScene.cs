using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTaxRateScene : MonoBehaviour {

    private string sceneToLoad = "TaxRateScene";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 1;
        SceneManager.LoadScene(sceneToLoad);
      
    }
}
