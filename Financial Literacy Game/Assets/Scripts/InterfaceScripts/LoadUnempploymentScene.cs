using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUnempploymentScene : MonoBehaviour {

	private string sceneToLoad = "Day2_UnemploymentScene";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 9;
        SceneManager.LoadScene(sceneToLoad);

    }
}
