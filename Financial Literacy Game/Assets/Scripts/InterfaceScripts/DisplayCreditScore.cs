using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCreditScore : MonoBehaviour {

    public Text textObject;
    public string textToDisplay;

	// Use this for initialization
	void Start ()
    {
        // initialise string textToDisplay here
        textToDisplay = "The credit score is:" + PlayerPrefs.GetInt("CreditScore");
        textObject.text = textToDisplay;
	}
	
	
}
