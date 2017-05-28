using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEDProgramButton : MonoBehaviour {

    float weeklyIncome;
    int hourlyWage;
    float costOfProgram = 10000;
    float savingsAmount;

    private void Start()
    {
        hourlyWage = PlayerPrefs.GetInt("HourlyWage");
        savingsAmount = PlayerPrefs.GetFloat("currentSavingsAmount");
    }

    public void OnMouseDown()
    {
        Debug.Log("GED program selected");
        weeklyIncome = 20 * hourlyWage;
        savingsAmount = savingsAmount - costOfProgram;
        PlayerPrefs.SetFloat("WeeklyIncome", weeklyIncome);
        PlayerPrefs.SetFloat("CurrentSavingsAmount", savingsAmount);
        
    }
}
