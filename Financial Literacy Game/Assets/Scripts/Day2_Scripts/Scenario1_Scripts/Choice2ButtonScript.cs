using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice2ButtonScript : MonoBehaviour {

    float currentSavingsAmount;
    float newSavingsAmount;
    float creditCardInterestFactor = 0.10f;
    float priceOfPurchasedItem = 300;   // the debt right now

    public Text currentSavingsText;
    public Text newSavingsText;

    private void Start()
    {
        currentSavingsAmount = PlayerPrefs.GetFloat("CurrentSavingsAmount");
        currentSavingsText.text = "Current Savings of 6 months: " + currentSavingsAmount;
       
    }

    public void OnMouseDown()
    {
        newSavingsAmount = currentSavingsAmount - priceOfPurchasedItem;
        newSavingsText.text = "Your new savings: " + newSavingsAmount;
    }
}
