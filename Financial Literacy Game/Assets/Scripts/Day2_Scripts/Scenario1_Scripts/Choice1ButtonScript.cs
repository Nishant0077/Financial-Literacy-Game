using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice1ButtonScript : MonoBehaviour {

    float currentSavingsAmount;
    float newSavingsAmount;
    float creditCardInterestFactor = 0.10f;
    float priceOfPurchasedItem = 300;   // the debt right now

    public Text currentSavingsText;
    public Text newSavingsText;

    private void Start()
    {
        currentSavingsAmount = PlayerPrefs.GetFloat("CurrentSavingsAmount");
        currentSavingsText.text = "Current Savings: " + currentSavingsAmount;
    }

    public void OnMouseDown()
    {
        newSavingsAmount = (currentSavingsAmount - (creditCardInterestFactor * priceOfPurchasedItem * 6)) - (25 * 6);
        newSavingsText.text = "Your new savings: " + newSavingsAmount;

    }
}
