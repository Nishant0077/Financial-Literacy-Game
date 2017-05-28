using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScamController : MonoBehaviour
{

    public int generatedNumber;
    public Text generatedNumberText;
    public Text scamText;
    public Text goalAmountText;
    public Text budgetingMethodText;
    public Text savingsText;
    float originalSavings;
    float currentSavings;
    //private BudgetingMethodController bc;

    public void Start()
    {
        generatedNumber = 0;

        originalSavings = PlayerPrefs.GetFloat("Savings");

        DisplayBudgetingMethodText();
        goalAmountText.text = "The goal amount is " + PlayerPrefs.GetInt("GoalAmount");
        savingsText.text = "The savings is " + PlayerPrefs.GetFloat("Savings");

    }

    public void DisplayBudgetingMethodText()
    {
        switch (PlayerPrefs.GetInt("BudgetingMethod"))
        {
            case 1:
                budgetingMethodText.text = "The budgeting method is 65/5/30";
                break;

            case 2:
                budgetingMethodText.text = "The budgeting method is 60/10/30";
                break;

            case 3:
                budgetingMethodText.text = "The budgeting method is 58/12/30";
                break;

            default:
                budgetingMethodText.text = "The budgeting method is 58/12/30";
                break;

        }
    }

    public void OnMouseDown()
    {
        generatedNumber = Random.Range(1, 7);  // 7 is not included
        generatedNumberText.text = "You have rolled a " + generatedNumber;



        switch (generatedNumber)
        {
            case 1:
                scamText.text = "You did not fall for the scam";
                savingsText.text = "The savings is " + originalSavings;
                break;

            case 2:
                scamText.text = "You lost $20 to the scam";
                currentSavings = originalSavings - 20;
                PlayerPrefs.SetFloat("Savings", currentSavings);
                savingsText.text = "The savings is " + currentSavings;
                Debug.Log("You lost $20");
                break;

            case 3:
                scamText.text = "You lost $50 to the scam";
                currentSavings = originalSavings - 50;
                PlayerPrefs.SetFloat("Savings", currentSavings);
                savingsText.text = "The savings is " + currentSavings;
                Debug.Log("You lost $50");
                break;

            default:
                scamText.text = "You did not fall for the scam";
                savingsText.text = "The savings is " + originalSavings;
                break;

        }



        // store the taxrate and hourly wage in playerprefs
        // PlayerPrefs.SetInt("CreditScore", creditScore);
        //creditScoreText.text = "The credit score is " + creditScore;

    }
}
