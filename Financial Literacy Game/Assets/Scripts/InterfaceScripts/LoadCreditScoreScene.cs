using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCreditScoreScene : MonoBehaviour {

    private string sceneToLoad = "CreditScoreScene";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
