using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnemploymentGenerator : MonoBehaviour {

    int randomNumber;
    public Text unemploymentText;
    public Text impactOnSavingsText;
    public Text currentSavingsText;
    public Text newSavingsText;
    int numberOfUnemploymentScenarios = 3;
    float unemploymentPenalty = 2000;
    float impactOnSavings;
    float employmentBonus = 1000;
    float currentSavingsAmount;
    float newSavingsAmount;

    // Use this for initialization
    void Start()
    {

        currentSavingsAmount = PlayerPrefs.GetFloat("CurrentSavingsAmount");
        currentSavingsText.text = "Current Savings Amount: " + currentSavingsAmount.ToString();

        randomNumber = Random.Range(1, 4);   

        if (randomNumber == 1)
        {
            unemploymentText.text = "You have handled your responsibilies perfectly. Keep up the good work!";
            impactOnSavings = -employmentBonus;  // player would receive a bonus
        }

        if (randomNumber == 2)
        {
            unemploymentText.text = "Your performance was below expectations. You are on a probation now and are " +
                "expected to improve over the next quarter";
            impactOnSavings = 0;  // player would gain/lose nothing
        }

        if (randomNumber == 3)
        {
            unemploymentText.text = "Your performance was below expectations. " +
                "You have been terminated from your position";
            impactOnSavings = unemploymentPenalty;  // player would live off their savings
        }

        if (impactOnSavings >= 0)
            impactOnSavingsText.text = "Impact on Savings: $" + impactOnSavings.ToString();

        else
        {
            impactOnSavingsText.text = "Bonus: $" + (impactOnSavings * -1).ToString();
        }

        newSavingsAmount = currentSavingsAmount - impactOnSavings;
        newSavingsText.text = "New savings amount: " + newSavingsAmount.ToString();
        PlayerPrefs.SetFloat("CurrentSavingsAmount", newSavingsAmount);

    }
}
