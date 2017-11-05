using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice1ButtonScript : MonoBehaviour {


    float currentSavingsAmount;
    float newSavingsAmount;
    float creditCardInterestFactor = 0.10f;
    float priceOfPurchasedItem = 300;   // the debt right now
    public Text currentSavingsText;
    public Text newSavingsText;
    private int sceneID = 1;
    Dictionary<object, object> scene1Dict = new Dictionary<object, object>();
    GameSaver gs;

    private void Start()
    {
        gs = new GameSaver(sceneID);

        // we access the "uber" static dictionary, access the dictionary stored at key 0, then access
        // the value in this dictionary by using one of the enums defined in the "enums" class as key
        currentSavingsAmount = (float)PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1][PlayerAttributeEnums.currentSavingsAmount];
        Debug.Log("Current Savings Amount is " + currentSavingsAmount);
    }


    public void OnMouseDown()
    {
        // activate the back to inbox button
        CreditCardGameController.b.gameObject.SetActive(true);

        //set the new savings amount
        newSavingsAmount = ((currentSavingsAmount - (creditCardInterestFactor * priceOfPurchasedItem * 6)) - (25 * 6));

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

        // this is just a test to see if the dictionaries for scenes 0 and 1 hold the same values or not
        // ideally, there is only one value different -> current savings amount and the following
        // print statement must return exactly one "false". This test passes.
        foreach (object key in PersistentManagerScript.sceneToPlayerAttributesMap[0].Keys)
        {
            Debug.Log(PersistentManagerScript.sceneToPlayerAttributesMap[0][key].Equals(PersistentManagerScript.sceneToPlayerAttributesMap[1][key]));
        }
    }
}
