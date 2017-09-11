using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {

    public List<Button> sceneButtonList = new List<Button>();
    public int numberOfSceneButtons = 9;
    public int currentActiveSceneButtonIndex = 4;

    // Use this for initialization
    void Start () {

        // populating the list with the buttons in the scene
        for (int i = 0; i < numberOfSceneButtons; i++)
        {
            Button b = GameObject.FindGameObjectsWithTag("Button" + i)[0].GetComponentInChildren<Button>();
            sceneButtonList.Add(b);
        }

        Debug.Log(sceneButtonList.Count);

        // setting all buttons to inactive initially
        for (int i = 0; i < sceneButtonList.Count; i++)
        {
            sceneButtonList[i].gameObject.SetActive(false);
        }

        // setting some buttons active depending on where we are in the game
        // the current active button index is in the persistent manager class
        Debug.Log("Current active scene index is: " + PersistentManagerScript.mostRecentActiveSceneButtonIndex);
        for (int i = 0; i < PersistentManagerScript.mostRecentActiveSceneButtonIndex + 1; i++)
        {
            sceneButtonList[i].gameObject.SetActive(true);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
