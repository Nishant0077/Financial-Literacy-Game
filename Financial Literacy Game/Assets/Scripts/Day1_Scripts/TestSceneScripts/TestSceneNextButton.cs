﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneNextButton : MonoBehaviour {

    private string sceneToLoad = "Day1_HealthCarePlan";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
