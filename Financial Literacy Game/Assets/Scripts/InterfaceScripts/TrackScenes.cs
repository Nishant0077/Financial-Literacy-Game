using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackScenes : MonoBehaviour {

    public static List<Button> sceneButtonList = new List<Button>();
   

    public int currentActiveSceneButtonIndex = 4;       // the healthcare scene's button is active at the start
   // public bool sendNewEmail = false;

	// Use this for initialization
	void Start () {

        Button[] buttonArray = FindObjectsOfType(typeof(Button)) as Button[];

        Debug.Log(buttonArray.Length);

        for (int i = 0; i < buttonArray.Length; i++)
        {
            
        }
      
        for (int i = 0; i < sceneButtonList.Count - 5; i++)   // the last 4 buttons should be active all the time
            // the first scene must have its button active as well
        {
            sceneButtonList[i].gameObject.SetActive(false);
        }
	}
	
	public void SendEmail()
    {
        sceneButtonList[currentActiveSceneButtonIndex + 1].gameObject.SetActive(true);
        Debug.Log("next scene is active");
    }
}
