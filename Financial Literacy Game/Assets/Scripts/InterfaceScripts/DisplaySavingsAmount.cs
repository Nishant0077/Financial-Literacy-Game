using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySavingsAmount : MonoBehaviour {

    public Text textObject;
    public string textToDisplay;

    // Use this for initialization
    void Start()
    {
        // initialise string textToDisplay here
        textToDisplay = "The savings amount is:" + PlayerPrefs.GetFloat("SavingsAmount");
        textObject.text = textToDisplay;
    }
}
