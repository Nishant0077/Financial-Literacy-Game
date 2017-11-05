using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice2ButtonScript : MonoBehaviour
{

    float currentSavingsAmount;
    float newSavingsAmount;
    float creditCardInterestFactor = 0.10f;
    float priceOfPurchasedItem = 300;   // the debt right now

    public Text currentSavingsText;
    public Text newSavingsText;

    private void Start()
    {
        currentSavingsAmount = PlayerPrefs.GetFloat("CurrentSavingsAmount");
        //currentSavingsText.text = "Current project sain: " + currentSavingsAmount;

    }

    public void OnMouseDown()
    {
        // activate the back to inbox button
        CreditCardGameController.b.gameObject.SetActive(true);
        newSavingsAmount = (currentSavingsAmount - priceOfPurchasedItem);
        newSavingsText.text = "Your new projected savings for next 6 months: " + newSavingsAmount;
    }
}