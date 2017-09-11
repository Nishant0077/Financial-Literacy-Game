using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHealthPlanScene : MonoBehaviour {

    private string sceneToLoad = "Day1_HealthCarePlan";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 4;
        SceneManager.LoadScene(sceneToLoad);
        
    }
}
