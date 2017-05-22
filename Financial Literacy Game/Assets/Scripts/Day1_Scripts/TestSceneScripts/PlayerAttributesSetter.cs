using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesSetter : MonoBehaviour {
    
    public Text creditScoreText;
    public Text goalAmountText;
    public Text budgetingMethodText;
    public Text weeklyIncomeText;
    public Text MonthlySavingsText;
    public Text taxRateText;
    public Text hourlyWageText;
    public Text currentSavingsText;
    public int creditScore;
    public int goalAmount;
    public int hourlyWage;
    public float taxRate;
    // budgeting method------------------
    public float savingsFactor;
    public float discretionaryFactor;
    public float fixedExpensesFactor;
    //-------------------------------
    public float weeklyIncome;
    public float currentSavingsAmount;



    // Use this for initialization
    void Start () {


        /* This is where the code for calculating the player's Wage, Budget etc will be written
         * The player's answers from the quiz will be taken into account and a formula will be written to generate 
         * that player's unique attributes at the start of the game
         * For testing purposes, arbitrary values will be provided
         */

       // monthlySavingsAmount = 500;
        
        
        creditScore = 600;
        taxRate = 15f;
        hourlyWage = 20;
        savingsFactor = 0.25f;
        //currentSavingsAmount = monthlySavingsAmount * 26;
        weeklyIncome =  ((40 * hourlyWage) - ((taxRate / 100) * (40 * hourlyWage)));
        currentSavingsAmount = weeklyIncome * savingsFactor * 26;


        goalAmountText.text = "Goal Amount: " + PlayerPrefs.GetInt("GoalAmount");
        budgetingMethodText.text = "Budgeting Method: 50/ 25/ 25";
       // MonthlySavingsText.text = "Monthly Savings: " + monthlySavingsAmount;
        weeklyIncomeText.text = "Weekly Income after tax: " + weeklyIncome;
        creditScoreText.text = "Credit Score: " + creditScore;
        taxRateText.text = "Tax Rate: " + taxRate;
        hourlyWageText.text = "Hourly Wage: " + hourlyWage;
        currentSavingsText.text = "Current Savings over 6 Months: " + currentSavingsAmount;

        PlayerPrefs.SetInt("CreditScore", creditScore);
        PlayerPrefs.SetFloat("TaxRate", taxRate);
        PlayerPrefs.SetInt("HourlyWage", hourlyWage);
        PlayerPrefs.SetFloat("CurrentSavingsAmount", currentSavingsAmount);



    }
	
}
