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

    private void Start()
    { 
        // we access the "uber" static dictionary, access the dictionary stored at key 0, then access
        // the value in this dictionary by using one of the enums defined in the "enums" class as key
        currentSavingsAmount = (float)PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1][PlayerAttributeEnums.currentSavingsAmount];
        Debug.Log("Current Savings Amount is " + currentSavingsAmount);
    }

    public void OnMouseDown()
    {
        // activate the back to inbox button
        CreditCardGameController.b.gameObject.SetActive(true);
        newSavingsAmount = ((currentSavingsAmount - (creditCardInterestFactor * priceOfPurchasedItem * 6)) - (25 * 6));
        newSavingsText.text = "Your new projected savings for next 6 months: " + newSavingsAmount;
        Debug.Log("Current savings amount is: " + currentSavingsAmount);
        Debug.Log("New savings amount is: " + newSavingsAmount);

        // now we save this value in our static dictionary defined in the static class
        // for this, we use the dictionary declared solely for this scene, 
        // update only what has been changed and then add
        // it to our "uber" dictionary
        // in order to do that, we need to get the previous scene's dictionary from the uber dictionary 
        // and copy all its keys and values into the new dictionary. Then change the specific values (attributes)
        // manually in this new dictionary when mouse is clicked
        // doing this preserves all values in the previous scenes' dictionaries

        // this is the dictionary for the previous scene
        Dictionary<object, object> scene0Dict = PersistentManagerScript.sceneToPlayerAttributesMap[0];

        // now we copy everything from old dictionary into the new dictionary
        foreach(object key in scene0Dict.Keys)
        {
            scene1Dict.Add(key, scene0Dict[key]);
        }

        // now we update only the current savings attribute in the new dictionary
        // and add(or update) it to the uber dictionary
        // Doing all of the above updates ONLY THIS SCENE'S DICTIONARY IN THE UBER DICTIONARY
        scene1Dict[PlayerAttributeEnums.currentSavingsAmount] = newSavingsAmount;
        PersistentManagerScript.sceneToPlayerAttributesMap[sceneID] = scene1Dict;

        // this is just a test to see if the dictionaries for scenes 0 and 1 hold the same values or not
        // ideally, there is only one value different -> current savings amount and the following
        // print statement must return exactly one "false". This test passes.
        foreach(object key in PersistentManagerScript.sceneToPlayerAttributesMap[0].Keys)
        {
            Debug.Log(PersistentManagerScript.sceneToPlayerAttributesMap[0][key].Equals(PersistentManagerScript.sceneToPlayerAttributesMap[1][key]));
        }




    }
}
