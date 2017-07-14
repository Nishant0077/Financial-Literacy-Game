using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTaxRateScene : MonoBehaviour {

    private string sceneToLoad = "TaxRateScene";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
