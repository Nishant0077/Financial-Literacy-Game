using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {

    public List<Button> sceneButtonList = new List<Button>();
    private int numberOfSceneButtons = 11;   // number of emails
    public int currentActiveSceneButtonIndex = 4;

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
        }
    }

    void SetSomeButtonsInactiveAtTheStart()
    {
        // setting all buttons to inactive initially
        for (int i = 0; i < sceneButtonList.Count; i++)
        {
            sceneButtonList[i].gameObject.SetActive(false);
        }
    }

    void SetButtonsActiveAsThePlayerProgresses()
    {
        // setting some buttons active depending on where we are in the game
        // the current active button index is in the persistent manager class
        Debug.Log("Current active scene index is: " + PersistentManagerScript.mostRecentActiveSceneButtonIndex);
        for (int i = 0; i < PersistentManagerScript.mostRecentActiveSceneButtonIndex + 1; i++)
        {
            sceneButtonList[i].gameObject.SetActive(true);
        }
    }

    // Use this for initialization
    void Start () {

        PopulateTheListWithButtonsInTheScene();
        SetSomeButtonsInactiveAtTheStart();
        SetButtonsActiveAsThePlayerProgresses();

    }	
	
}
