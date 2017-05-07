using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetingMethodController : MonoBehaviour
{

    public int generatedNumber;
    public Text generatedNumberText;
    public Text budgetingMethodText;
    public int budgetingMethod;



    // determine hourly wage and tax rate 
    public void OnMouseDown()
    {
        generatedNumber = Random.Range(1, 7);  // 7 is not included
        generatedNumberText.text = "You have rolled a " + generatedNumber;


        // decide the taxrate and hourly wage based on the generated number
        switch (generatedNumber)
        {
            case 1:
                budgetingMethod = 1;
                break;

            case 2:
                budgetingMethod = 2;
                break;

            case 3:
                budgetingMethod = 3;
                break;

            default:
                budgetingMethod = 3;
                break;

                
        }

        // store the budgeting method in playerprefs
        PlayerPrefs.SetInt("BudgetingMethod", budgetingMethod);

        switch (budgetingMethod)
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
