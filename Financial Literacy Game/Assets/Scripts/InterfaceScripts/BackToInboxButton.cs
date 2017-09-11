using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToInboxButton : MonoBehaviour {

    private string sceneToLoad = "EmailInterface";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);

        // if current active scene index is within range and if the button being currently 
        // clicked is the most recently activated button, then increment current active scene button index
        if (PersistentManagerScript.currentlyClickedSceneButtonindex < PersistentManagerScript.numberOfSceneButtons - 1
            && PersistentManagerScript.currentlyClickedSceneButtonindex == PersistentManagerScript.mostRecentActiveSceneButtonIndex)
        PersistentManagerScript.mostRecentActiveSceneButtonIndex++;
        //Debug.Log("Current Active scene index = " + pm.currentActiveSceneButtonIndex);
    }
}
