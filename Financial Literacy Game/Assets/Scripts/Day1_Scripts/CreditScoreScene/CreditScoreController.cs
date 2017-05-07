using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScoreController : MonoBehaviour
{

    public int generatedNumber;
    public Text generatedNumberText;
    public Text creditScoreText;
    
    private int creditScore = 0;
    

    // determine hourly wage and tax rate 
    public void OnMouseDown()
    {
        generatedNumber = Random.Range(1, 7);  // 7 is not included
        generatedNumberText.text = "You have rolled a " + generatedNumber;


        // decide the taxrate and hourly wage based on the generated number
        switch (generatedNumber)
        {
            case 1:
                creditScore = 300;
                break;

            case 2:
                creditScore = 400;
                break;

            case 3:
                creditScore = 500;
                break;

            default:
                creditScore = 700;
                break;

        }

              // store the taxrate and hourly wage in playerprefs
        PlayerPrefs.SetInt("CreditScore", creditScore);
        creditScoreText.text = "The credit score is " + creditScore;
        
    }
}
