using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackScenes : MonoBehaviour {

    public static List<Button> sceneButtonList = new List<Button>();
    int numberOfSceneButtons;   
    public int currentActiveSceneButtonIndex = 4;       // the healthcare scene's button is active at the start

    void PopulateTheListWithButtonsInTheScene()
    {
        // this code will execute ONCE
        if (PersistentManagerScript.Instance.hasFirstSceneBeenLoaded == false)
        {
            for (int i = 0; i < numberOfSceneButtons; i++)
            {
                // all buttons' names must be of the form "ButtonX" where X is the index of the button
                Button b = GameObject.FindGameObjectsWithTag("Button" + i)[0].GetComponentInChildren<Button>();
                sceneButtonList.Add(b);
            }

            numberOfSceneButtons = sceneButtonList.Count;
            // PersistentManagerScript.Instance.hasFirstSceneBeenLoaded = true;
        }
    }
    
    void SetSomeButtonsInactiveAtTheStart()
    {
        // all buttons after the healthcare scene must be inactive at the start
        for (int i = PersistentManagerScript.mostRecentActiveSceneButtonIndex;
            i < PersistentManagerScript.numberOfSceneButtons; i++)
        {
            sceneButtonList[i].gameObject.SetActive(false);
        }
    }

    void DisablePreviousButtons()
    {
        // the previous buttons must be disabled so that the player cannot go back without finishing the game
        if (PersistentManagerScript.hasTheGameBeenCompleted == false)
        {
            for (int i = 0; i < PersistentManagerScript.mostRecentActiveSceneButtonIndex; i++)
            {
                Debug.Log("Button with index " + i + " must be disabled");
                sceneButtonList[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    // Use this for initialization
    void Start () {

        PopulateTheListWithButtonsInTheScene();
        SetSomeButtonsInactiveAtTheStart();
        DisablePreviousButtons();

        if (PersistentManagerScript.mostRecentActiveSceneButtonIndex == 
            PersistentManagerScript.numberOfSceneButtons - 1)
        {
            PersistentManagerScript.hasTheGameBeenCompleted = true;
            Debug.Log("GAME OVER");
        }
            

    }
	
	
}
