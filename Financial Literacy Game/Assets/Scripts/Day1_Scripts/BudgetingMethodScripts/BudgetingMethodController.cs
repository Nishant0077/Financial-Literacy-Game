using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetingMethodController : MonoBehaviour
{

    public int generatedNumber;
    public Text generatedNumberText;
    public Text budgetingMethodText;
    public int savingsPercentage;
    float temp;
    float savings;



    // determine hourly wage and tax rate 
    public void OnMouseDown()
    {
        generatedNumber = Random.Range(1, 7);  // 7 is not included
        generatedNumberText.text = "You have rolled a " + generatedNumber;


        // decide the taxrate and hourly wage based on the generated number
        switch (generatedNumber)
        {
            case 1:
                savingsPercentage = 10;
                break;

            case 2:
                savingsPercentage = 20;
                break;

            case 3:
                savingsPercentage = 30;
                break;

            default:
                savingsPercentage = 15;
                break;

                
        }

        
        DisplayBudgetingMethodText();
        temp = (PlayerPrefs.GetInt("BudgetingMethod"));
        savings = ((float) (temp / 100)) * (PlayerPrefs.GetFloat("Income") * 4);
        Debug.Log("Savings percent is : " + PlayerPrefs.GetInt("BudgetingMethod"));
        Debug.Log("The weekly income is " + (PlayerPrefs.GetFloat("Income")));
        Debug.Log("The monthly savings is " + savings);
        // store the budgeting method in playerprefs
        PlayerPrefs.SetInt("BudgetingMethod", savingsPercentage);
        PlayerPrefs.SetFloat("Savings", savings);
        


    }

    public void DisplayBudgetingMethodText()
    {
        switch (savingsPercentage)
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
        
}
