﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalSceneNextButton : MonoBehaviour {

    string scenetoLoad = "Day1_TestScene";

    public void OnMouseDown()
    {
        SceneManager.LoadScene(scenetoLoad);
    }
}