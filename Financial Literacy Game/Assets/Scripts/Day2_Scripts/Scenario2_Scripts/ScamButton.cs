using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScamButton : MonoBehaviour {

    float currentSavingsAmount;
    float newSavingsAmount;
    float boostAmount;
    float punishmentAmount;
    public Text currentSavingsAmountText;
    public Text buttonText;
    public Text newSavingsAmountText;
    public Text goalAmountText;
    int goalAmount;

    private void Start()
    {
        currentSavingsAmount = PlayerPrefs.GetFloat("CurrentSavingsAmount") * 3;   // total 18 months have passed
        goalAmount = PlayerPrefs.GetInt("GoalAmount");
        boostAmount = (goalAmount - currentSavingsAmount);
        punishmentAmount = 0.20f * currentSavingsAmount;    // completely arbitrary
        goalAmountText.text = "Goal Amount: " + goalAmount;
        currentSavingsAmountText.text = "Current Savings in your bank: " + currentSavingsAmount;
        buttonText.text = "Click here to get a boost of " + "$" + boostAmount;
    }

    public void OnMouseDown()
    {
        newSavingsAmount = currentSavingsAmount - punishmentAmount;
        buttonText.text = "You were scammed! *Laughs in evil*";
        newSavingsAmountText.text = "New Savings Amount: " + newSavingsAmount;
    }
}
