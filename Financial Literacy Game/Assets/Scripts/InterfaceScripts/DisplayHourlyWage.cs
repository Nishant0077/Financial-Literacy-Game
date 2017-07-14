using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHourlyWage : MonoBehaviour {

    public Text textObject;
    public string textToDisplay;

    // Use this for initialization
    void Start()
    {
        // initialise string textToDisplay here
        textToDisplay = "The hourly wage is:" + PlayerPrefs.GetInt("HourlyWage");
        textObject.text = textToDisplay;
    }
}
