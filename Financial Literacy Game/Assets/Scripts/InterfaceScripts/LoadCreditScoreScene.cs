using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCreditScoreScene : MonoBehaviour {

    private string sceneToLoad = "CreditScoreScene";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 0;
        SceneManager.LoadScene(sceneToLoad);

    }
}
