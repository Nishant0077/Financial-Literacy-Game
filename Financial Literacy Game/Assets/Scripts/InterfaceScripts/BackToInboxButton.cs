using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToInboxButton : MonoBehaviour {

    private string sceneToLoad = "EmailInterface";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
