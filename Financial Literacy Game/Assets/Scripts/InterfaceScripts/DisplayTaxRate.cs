using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTaxRate : MonoBehaviour {

    public Text textObject;
    public string textToDisplay;

    // Use this for initialization
    void Start()
    {
        // initialise string textToDisplay here
        textToDisplay = "The tax rate is:" + PlayerPrefs.GetFloat("TaxRate");
        textObject.text = textToDisplay;
    }
}
