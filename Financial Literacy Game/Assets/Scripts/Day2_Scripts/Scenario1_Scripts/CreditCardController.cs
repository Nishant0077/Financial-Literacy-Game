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

    public void Start()
    {
        goalAmountText.text = "The goal amount is " + PlayerPrefs.GetInt("GoalAmount"); 
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
