using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScamController : MonoBehaviour
{       
    public static Button b;
    float currentSavingsAmount;
    float newSavingsAmount;
    float boostAmount;
    float punishmentAmount;
    public Text currentSavingsAmountText;
    public Text buttonText;
    public Text newSavingsAmountText;
    public Text goalAmountText;
    int goalAmount;
    static int sceneID = 2;

    public void Start()
    {
        // get the back to inbox button and deactivate it
        b = GameObject.FindGameObjectsWithTag("BackToInboxScamScene")[0].GetComponentInChildren<Button>();
        b.gameObject.SetActive(false);

        // get the current savings amount from the previous scene's dictionary in the uber dictionary
        currentSavingsAmount = (float)PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1]
            [PlayerAttributeEnums.currentSavingsAmount];  // NOTE: 6 months have passed since the previous scene

        // get the goal amount from the previous scenes dictionary in the uber dictionary
        goalAmount = (int)PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1][PlayerAttributeEnums.goalAmount];

        // set the amount of savings that will be deducted due to scam
        punishmentAmount = 0.20f * currentSavingsAmount;    // completely arbitrary
        goalAmountText.text = "Goal Amount: " + goalAmount;
        currentSavingsAmountText.text = "Current Savings in your bank: " + currentSavingsAmount;
        buttonText.text = "Click here to get a boost of " + "$" + boostAmount;
    }

    // this function will be called by the buttons in the scene
    public void DoStuffWhenAButtonIsClicked(int buttonID)
    {
        // the scam button is pressed
        if (buttonID == 1)
        {
            ScamController.b.gameObject.SetActive(true);
            newSavingsAmount = currentSavingsAmount - punishmentAmount;
            buttonText.text = "You were scammed! *Laughs in evil*";
            newSavingsAmountText.text = "New Savings Amount: " + newSavingsAmount;
        }

        else
        {
            ScamController.b.gameObject.SetActive(true);
        }
    }
}
