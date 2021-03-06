﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPlanButton : MonoBehaviour
{

    HealthCareController hc;
    float monthlyDeduction = 0;
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
        hc.currentPlan = HealthCareController.healthCarePlanType.No;
        PlayerPrefs.SetFloat("CurrentSavingsAmount", hc.projectedSavingsAmount);
        PlayerPrefs.SetString("CurrentHealthPlan", hc.currentPlan.ToString());
        
    }
}
