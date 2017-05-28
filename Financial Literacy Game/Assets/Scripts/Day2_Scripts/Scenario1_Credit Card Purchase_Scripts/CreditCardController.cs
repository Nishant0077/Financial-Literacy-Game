using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCardController : MonoBehaviour
{

    public int generatedNumber;
    public Text generatedNumberText;
    public Text creditCardText;
    public Text goalAmountText;
    public Text budgetingMethodText;
    public Text savingsText;
    //private BudgetingMethodController bc;

    public void Start()
    {
        //DisplayBudgetingMethodText();
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
                creditCardText.text = "You made a 200$ purchase and return the item";
                
                break;

            case 2:
                creditCardText.text = "You made an unbudgeted 300$ purchase and pay only the minimum $25 with interest";
                break;

            case 3:
                creditCardText.text = "You made an unbudgeted 500$ purchase and pay off the debt in full through savings";
                break;

            default:
                creditCardText.text = "You made a 300$ purchase and pay off the debt in full";
                break;

        }

        

        // store the taxrate and hourly wage in playerprefs
       // PlayerPrefs.SetInt("CreditScore", creditScore);
        //creditScoreText.text = "The credit score is " + creditScore;

    }
}
