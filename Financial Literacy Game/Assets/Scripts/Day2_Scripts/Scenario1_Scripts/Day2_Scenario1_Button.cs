using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day2_Scenario1_Button : MonoBehaviour
{
    string sceneToLoad = "Day2_Scam";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
