using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumPlanButton : MonoBehaviour
{

    HealthCareController hc;
    float monthlyDeduction = 500;
    float savingsWithNoPlan;
    // Use this for initialization

    private void Start()
    {
        hc = GameObject.FindObjectOfType<HealthCareController>();
        savingsWithNoPlan = PlayerPrefs.GetFloat("CurrentSavingsAmount");
    }

    public void OnMouseDown()
    {
        hc.monthlyDeduction = monthlyDeduction;
        hc.projectedSavingsAmount = savingsWithNoPlan - (monthlyDeduction * 6);
        hc.projectedSavingsText.text = "Your projected savings over 6 months: " + hc.projectedSavingsAmount;
        PlayerPrefs.SetFloat("CurrentSavingsAmount", hc.projectedSavingsAmount);
        hc.currentPlan = HealthCareController.healthCarePlanType.Premium;
        PlayerPrefs.SetString("CurrentHealthPlan", hc.currentPlan.ToString());
    }
}
