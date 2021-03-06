﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPlanButton : MonoBehaviour
{

    HealthCareController hc;
    float monthlyDeduction = 200;
    float savingsWithNoPlan;
    // Use this for initialization

    private void Start()
    {
        hc = GameObject.FindObjectOfType<HealthCareController>();
        savingsWithNoPlan = PlayerPrefs.GetFloat("CurrentSavingsAmount");
    }

    public void OnMouseDown()
    {
        // set the back to inbox button active
        HealthCareController.b.gameObject.SetActive(true);
        hc.monthlyDeduction = monthlyDeduction;
        hc.projectedSavingsAmount = savingsWithNoPlan - (monthlyDeduction * 6);
        hc.projectedSavingsText.text = "Your projected savings over 6 months: " + hc.projectedSavingsAmount;
        PlayerPrefs.SetFloat("CurrentSavingsAmount", hc.projectedSavingsAmount);
        hc.currentPlan = HealthCareController.healthCarePlanType.Standard;
        PlayerPrefs.SetString("CurrentHealthPlan", hc.currentPlan.ToString());
    }
}
