using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEmployerButton : MonoBehaviour
{

    int hourlyWage;
    int raiseInWage = 10;
    float savingsAmount;
    float savingsFactor;


    private void Start()
    {
        hourlyWage = PlayerPrefs.GetInt("HourlyWage");
        savingsAmount = PlayerPrefs.GetFloat("SavingsAmount");
        savingsFactor = PlayerPrefs.GetFloat("SavingsFactor");
    }

    public void OnMouseDown()
    {
        //*********** player will be provided a bonus and a raise in wage*****************

        hourlyWage = hourlyWage + raiseInWage;
        // savingsAmount = savingsAmount + ((40 * hourlyWage) * savingsFactor * 26);        // put it in next scene       
        PlayerPrefs.SetInt("HourlyWage", hourlyWage);
        PlayerPrefs.SetFloat("SavingsAmount", savingsAmount);



    }
}
