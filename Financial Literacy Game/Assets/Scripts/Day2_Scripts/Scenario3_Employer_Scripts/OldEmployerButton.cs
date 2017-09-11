using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEmployerButton : MonoBehaviour {

    int hourlyWage;
    int raiseInWage = 2;
    float bonus = 500;
    float savingsAmount;
    float savingsFactor;
    

    private void Start()
    {
        hourlyWage = PlayerPrefs.GetInt("HourlyWage");
        savingsAmount = PlayerPrefs.GetFloat("CurrentSavingsAmount");
        savingsFactor = PlayerPrefs.GetFloat("SavingsFactor");        
    }

    public void OnMouseDown()
    {
        GameController.b.gameObject.SetActive(true);

        //*********** player will be provided a bonus and a raise in wage*****************

        hourlyWage = hourlyWage + raiseInWage;
        // savingsAmount = savingsAmount + ((40 * hourlyWage) * savingsFactor * 26);        // put it in next scene
        Debug.Log("Old savings amount is " + savingsAmount);
        savingsAmount = savingsAmount + bonus;
        Debug.Log("New savings amount is" + savingsAmount);
        PlayerPrefs.SetInt("HourlyWage", hourlyWage);
        PlayerPrefs.SetFloat("CurrentSavingsAmount", savingsAmount);
        


    }
}
