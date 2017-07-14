using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSavingsAmountScene : MonoBehaviour {

    private string sceneToLoad = "SavingsAmountScene";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
