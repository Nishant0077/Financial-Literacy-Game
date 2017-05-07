using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HourlyRateController : MonoBehaviour {

    public int generatedNumber;
    public Text generatedNumberText;
    public Text hourlyWageText;
    public Text taxRateText;
    public Text incomeText;
    private float taxRate = 0;
    private int hourlyWage = 0;

    
    float income = 0;

    // determine hourly wage and tax rate 
    public void OnMouseDown()
    {
        generatedNumber = Random.Range(1, 7);  // 7 is not included
        generatedNumberText.text = "You have rolled a " + generatedNumber;


        // decide the taxrate and hourly wage based on the generated number
        switch(generatedNumber)
        {
            case 1:
                hourlyWage = 10;
                taxRate = 5;
                break;

            case 2:
                hourlyWage = 15;
                taxRate = 10;
                break;

            case 3:
                hourlyWage = 20;
                taxRate = 15;
                break;

            default:
                hourlyWage = 9;
                taxRate = 20.17f;
                break;

        }

        
        income = (40 * hourlyWage) - ((taxRate / 100) * (40 * hourlyWage));
              
        taxRateText.text = "The tax rate is " + taxRate + "%";
        hourlyWageText.text = "The hourly wage is " + hourlyWage;
        incomeText.text = "The weekly income after tax is : " + income;
        PlayerPrefs.SetFloat("TaxRate", taxRate);       // store the taxrate and hourly wage in playerprefs
        PlayerPrefs.SetInt("HourlyWage", hourlyWage);
        PlayerPrefs.SetFloat("Income", income);
        
    }
}
