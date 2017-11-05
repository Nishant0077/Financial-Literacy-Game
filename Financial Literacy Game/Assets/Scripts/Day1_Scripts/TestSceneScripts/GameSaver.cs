using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour {
    int sceneID;
    Dictionary<object, object> sceneToSaveDict = new Dictionary<object, object>();
    Dictionary<object, object> PreviousSceneDict; 

    public GameSaver(int sceneID)
    {
        this.sceneID = sceneID;
    }

    //we use the dictionary declared solely for the scene to be saved, 
    // update only what has been changed and then add
    // it to our "uber" dictionary
    // in order to do that, we need to get the previous scene's dictionary from the uber dictionary 
    // and copy all its keys and values into the new dictionary. Then change the specific values (attributes)
    // in this new dictionary by passing a map of the attribute enums and their corresponding values
    // doing this preserves all values in the previous scenes' dictionaries
    public void SaveGame(Dictionary<PlayerAttributeEnums, object> attributesToSave)
    {
        PreviousSceneDict = PersistentManagerScript.sceneToPlayerAttributesMap[sceneID - 1];

        // now we copy everything from old dictionary into the new dictionary
        foreach (object key in PreviousSceneDict.Keys)
        {
            sceneToSaveDict.Add(key, PreviousSceneDict[key]);
        }

        // all attributes to be saved are now updated in the dictionary for the scene to be saved
        foreach(PlayerAttributeEnums attribute in attributesToSave.Keys)
        {
            sceneToSaveDict[attribute] = attributesToSave[attribute];
        }

        // the uber dictionary is updated
        PersistentManagerScript.sceneToPlayerAttributesMap[sceneID] = sceneToSaveDict;
    }
}
