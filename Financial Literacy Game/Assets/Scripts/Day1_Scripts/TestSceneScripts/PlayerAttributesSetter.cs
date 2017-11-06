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
    public int hoursWorkedInWeek = 40;
    public float taxRate;
    // budgeting method------------------
    public float savingsFactor;
    public float discretionaryFactor;
    public float fixedExpensesFactor;
    //-------------------------------
    public float weeklyIncome;
    public float annualIncome;
    public float currentSavingsAmount;
    public int isEnrolledInSchool = 0;   // player prefs doesn't have bool, this is a workaround
    public int hoursOfEmployment = 40; // default for the hours worked in a week




    // Use this for initialization
    void Start () {


        /* This is where the code for calculating the player's Wage, Budget etc will be written
         * The player's answers from the quiz will be taken into account and a formula will be written to generate 
         * that player's unique attributes at the start of the game
         * For testing purposes, arbitrary values will be provided
         */

       // monthlySavingsAmount = 500;
        
        
        creditScore = 600;
        taxRate = 15f;   // TODO
        hourlyWage = 20;
        savingsFactor = 0.25f;  // 25% of all money in bank account is savings 
        //weeklyIncome = hoursWorkedInWeek * hourlyWage;
        annualIncome = weeklyIncome * 52;
        
        //currentSavingsAmount = monthlySavingsAmount * 26;
        weeklyIncome =  ((hoursWorkedInWeek * hourlyWage) - ((taxRate / 100) * (hoursWorkedInWeek * hourlyWage)));
        currentSavingsAmount = weeklyIncome * savingsFactor * 26; // this is for 6 months


        goalAmountText.text = "Goal Amount: " + PlayerPrefs.GetInt("GoalAmount");
        budgetingMethodText.text = "Budgeting Method: 50/ 25/ 25";
       // MonthlySavingsText.text = "Monthly Savings: " + monthlySavingsAmount;
        weeklyIncomeText.text = "Weekly Income after tax: " + weeklyIncome;
        creditScoreText.text = "Credit Score: " + creditScore;
        taxRateText.text = "Tax Rate: " + taxRate;
        hourlyWageText.text = "Hourly Wage: " + hourlyWage;
        currentSavingsText.text = "Current Savings projected over 6 Months: " + currentSavingsAmount;

        // the following dictionary datastructure is to store all the player attributes as defined above in the dictionary
        // of PersistentManagerScript
        // The key for this dictionary should be an enum
        Dictionary<object, object> playerAttributeDictForScene0 = new Dictionary<object, object>();

        // now we start adding each attribute to the dictionary
 
        playerAttributeDictForScene0[PlayerAttributeEnums.goalAmount] = goalAmount;
        playerAttributeDictForScene0[PlayerAttributeEnums.creditScore] = creditScore;        
        playerAttributeDictForScene0[PlayerAttributeEnums.savingsFactor] = savingsFactor;
        playerAttributeDictForScene0[PlayerAttributeEnums.discretionaryFactor] = discretionaryFactor;
        playerAttributeDictForScene0[PlayerAttributeEnums.fixedExpensesFactor] = fixedExpensesFactor;
        playerAttributeDictForScene0[PlayerAttributeEnums.hourlyWage] = hourlyWage;
        playerAttributeDictForScene0[PlayerAttributeEnums.currentSavingsAmount] = currentSavingsAmount;
        playerAttributeDictForScene0[PlayerAttributeEnums.hoursOfEmployment] = hoursOfEmployment;
        playerAttributeDictForScene0[PlayerAttributeEnums.taxRate] = taxRate;
        playerAttributeDictForScene0[PlayerAttributeEnums.isEnrolledInSchool] = isEnrolledInSchool;
        playerAttributeDictForScene0[PlayerAttributeEnums.weeklyIncome] = weeklyIncome;

        // we have now stored the above dictionary in the static dictionary with the key
        // as this scenes ID. This scene is just given an arbitrary value of 0
        PersistentManagerScript.sceneToPlayerAttributesMap.Add(0, playerAttributeDictForScene0);






    }
	
}
