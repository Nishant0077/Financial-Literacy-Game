using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCardGameController : MonoBehaviour
{
    public static Button b;
    float currentSavingsAmount = 0;
    float newSavingsAmount = 0;
    float minPayment = 15;
    float creditCardInterestFactor = 0.01416f;  // arbitrary (17% annual interest)
    float priceOfPurchasedItem = 300;   // the debt right now
    public Text currentSavingsText;
    public Text newSavingsText;
    private static int sceneID = 1;
    Dictionary<object, object> scene1Dict = new Dictionary<object, object>();
    GameSaver gs;
    float earningsOverSixMonths = 0;

    // Use this for initialization
    void Start()
    {
        b = GameObject.FindGameObjectsWithTag("BackToInboxCreditCardScene")[0].GetComponentInChildren<Button>();
        b.gameObject.SetActive(false);
        gs = new GameSaver(sceneID);

        // we access the "uber" static dictionary, access the dictionary stored at key 0, then access
        // the value in this dictionary by using one of the enums defined in the "enums" class as key
        currentSavingsAmount = (float)PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1][PlayerAttributeEnums.currentSavingsAmount];
        Debug.Log("Current Savings Amount is " + currentSavingsAmount);

        // earnings over 6 months is what the player will have in their account 6 months from now
        // in addition to the money in their account currently
        // earningsOverSixMonths = weekly income after tax * saving factor * 26 weeks

        earningsOverSixMonths = (float)PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1][PlayerAttributeEnums.weeklyIncome] *
            (float)PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1][PlayerAttributeEnums.savingsFactor] * 26;
    }

    float ExtraChargesDueToMinPayment(float debt, float minPayment, float interestFactor)
    {
        float principal = debt;
        float totalPayment = 0;
        int monthCounter = 0;
        float amountPaidExtra = 0;

        while (debt > 0)
        {
            totalPayment += minPayment;
            debt = debt + (interestFactor * debt) - minPayment;
            monthCounter++;
        }

        amountPaidExtra = ((totalPayment + debt) - principal);
        return amountPaidExtra;
    }

    public void DoStuffWhenButtonIsClicked(int buttonID)
    {
        // activate the back to inbox button
        CreditCardGameController.b.gameObject.SetActive(true);

        //set the new savings amount based on which button was clicked
        // the new savings amount is actually the amount that will be in the player's 
        //account after six months based on their choices in this scene
        if (buttonID == 1)
        {
            newSavingsAmount = (currentSavingsAmount + (earningsOverSixMonths)) - priceOfPurchasedItem -
                ExtraChargesDueToMinPayment(priceOfPurchasedItem, minPayment, creditCardInterestFactor); 
        }

        if (buttonID == 2)
        {
            newSavingsAmount = currentSavingsAmount + earningsOverSixMonths - priceOfPurchasedItem;
        }

        // update the savings amount text
        newSavingsText.text = "Your new projected savings for next 6 months: " + newSavingsAmount;
        Debug.Log("Current savings amount is: " + currentSavingsAmount);
        Debug.Log("New savings amount is: " + newSavingsAmount);

        // create a dictionary solely for the attributes that were changed in this scene
        Dictionary<PlayerAttributeEnums, object> attributesToSave = new Dictionary<PlayerAttributeEnums, object>();

        // add only those attributes that were changed in this scene
        attributesToSave.Add(PlayerAttributeEnums.currentSavingsAmount, newSavingsAmount);

        // call the method from the "SaveGame" Class and pass the dictionary of modified attributes as
        // parameter
        gs.SaveGame(attributesToSave);

        /*
        // this is just a test to see if the dictionaries for scenes 0 and 1 hold the same values or not
        // ideally, there is only one value different : current savings amount and the following
        // print statement must return exactly one "false". This test passes.
        foreach (object key in PersistentManagerScript.sceneToPlayerAttributesMap[0].Keys)
        {
            Debug.Log(PersistentManagerScript.sceneToPlayerAttributesMap[0][key].Equals(PersistentManagerScript.sceneToPlayerAttributesMap[1][key]));
        }
        */
    }
}