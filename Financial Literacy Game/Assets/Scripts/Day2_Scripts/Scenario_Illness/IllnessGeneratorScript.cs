using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllnessGeneratorScript : MonoBehaviour {

    int randomNumber;
    int numberOfIllnessScenarios = 4;
    public Text illnessText;
    public Text impactOnSavingsText;
    public Text currentSavingsText;
    public Text newSavingsText;
    float noIllnessPenalty = 0;
    float illness1Penalty = 1000;
    float illness2Penalty = 2500;
    float illness3Penalty = 5000;
    float impactOnSavings;
    float currentSavingsAmount;
    float newSavingsAmount;
    string healthPlan;
    float healthPlanDiscount;   // it's a multiplication factor (always <= 1)

	// Use this for initialization
	void Start () {

        healthPlan = PlayerPrefs.GetString("CurrentHealthPlan");
        currentSavingsAmount = PlayerPrefs.GetFloat("CurrentSavingsAmount");
        currentSavingsText.text = "Current Savings Amount: " + currentSavingsAmount.ToString();

        if (healthPlan == "Basic")
        {
            healthPlanDiscount = 0.2f;
        }

        if (healthPlan == "Standard")
        {
            healthPlanDiscount = 0.4f;
        }

        if (healthPlan == "Premium")
        {
            healthPlanDiscount = 0.6f;
        }

        else healthPlanDiscount = 0.0f;

        Debug.Log("Your health plan is " + healthPlan);

        randomNumber = Random.Range(1, numberOfIllnessScenarios + 1);

        if (randomNumber == 1)   // no illness scenario 
        {
            illnessText.text = "You are perfectly healthy";
            impactOnSavings = noIllnessPenalty * (1 - healthPlanDiscount);
        }

        if (randomNumber == 2)   // illness scenario #1
        {
            illnessText.text = "You have cavities in your teeth. Immediate treatment is advised.";
            impactOnSavings = illness1Penalty * (1 - healthPlanDiscount);
        }

        if (randomNumber == 3)   // illness scenario #2 
        {
            illnessText.text = "You have a severe vitamin deficiency. You will be prescribed an extended course " +
                                "of medication.";
            impactOnSavings = illness2Penalty * (1 - healthPlanDiscount);
        }

        if (randomNumber == 4)   // illness scenario #3 
        {
            illnessText.text = "You have been diagnosed with kidney stones. They will need to be removed surgically";
            impactOnSavings = illness3Penalty * (1 - healthPlanDiscount);
        }

        impactOnSavingsText.text = "Medical Bill: $" + impactOnSavings.ToString();
        newSavingsAmount = currentSavingsAmount - impactOnSavings;
        newSavingsText.text = "New savings amount: " + newSavingsAmount.ToString();
        PlayerPrefs.SetFloat("CurrentSavingsAmount", newSavingsAmount);

    }
	
	
}
