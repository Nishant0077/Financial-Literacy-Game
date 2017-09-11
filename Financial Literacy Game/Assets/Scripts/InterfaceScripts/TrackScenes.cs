using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackScenes : MonoBehaviour {

    public static List<Button> sceneButtonList = new List<Button>();
    int numberOfSceneButtons = 9;   
    public int currentActiveSceneButtonIndex = 4;       // the healthcare scene's button is active at the start
  

	// Use this for initialization
	void Start () {

        // this code will execute ONCE
        if (PersistentManagerScript.Instance.hasFirstSceneBeenLoaded == false)
        {
            for (int i = 0; i < numberOfSceneButtons; i++)
            {
                Button b = GameObject.FindGameObjectsWithTag("Button" + i)[0].GetComponentInChildren<Button>();
                sceneButtonList.Add(b);
            }

            PersistentManagerScript.Instance.hasFirstSceneBeenLoaded = true;
        }

        for (int i = PersistentManagerScript.mostRecentActiveSceneButtonIndex;
            i < PersistentManagerScript.numberOfSceneButtons; i++)
        {
            sceneButtonList[i].gameObject.SetActive(false);
        }
        
        /*

        // make all buttons inactive at the start
        // this code will execute EVERYTIME the scene is visited

        Debug.Log("The number of buttons are :" + sceneButtonList.Count);
        Debug.Log("make all buttons inactive");

        for (int i = 0; i < sceneButtonList.Count; i++)
        {
            sceneButtonList[i].gameObject.SetActive(false);
        }           

        // this code will exceute only ONCE
        
            Debug.Log("make some buttons active");

            for (int i = 0; i < sceneButtonList.Count - 5; i++)   // the last 4 buttons should be active all the time
                                                                  // the first scene of game must have its button active as well
            {
                sceneButtonList[i].gameObject.SetActive(true);
            }   

       

       // Debug.Log(sceneButtonList.Count);   
       */

    }
	
	public void SendEmail()
    {
        Debug.Log(sceneButtonList.Count);

       // sceneButtonList[6].gameObject.SetActive(true);
        
    }
}
