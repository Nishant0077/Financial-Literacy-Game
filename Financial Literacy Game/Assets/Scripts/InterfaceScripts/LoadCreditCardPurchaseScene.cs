﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCreditCardPurchaseScene : MonoBehaviour {

    private string sceneToLoad = "Day2_CreditCardPurchase";

    public void OnMouseDown()
    {
        // this button's index in the scene button list in Interface Manager script
        PersistentManagerScript.currentlyClickedSceneButtonindex = 5;
        SceneManager.LoadScene(sceneToLoad);
       
    }
}