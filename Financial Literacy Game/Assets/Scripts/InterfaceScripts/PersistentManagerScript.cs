using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentManagerScript : MonoBehaviour {

    public static PersistentManagerScript Instance { get; private set; }

    public List<Button> sceneButtonList = new List<Button>();
    public static int numberOfSceneButtons = 11;    // number of emails
    public static int mostRecentActiveSceneButtonIndex = 4;  // the button index upto which all buttons must be active
    public static int currentlyClickedSceneButtonindex = 0;  // not sure if I am using this anywhere: TODO
    public bool hasFirstSceneBeenLoaded = false;
    public static bool hasTheGameBeenCompleted = false;  // boolean to check if game has been completed by player

    // This dictionary stores the attributes (also a dictionary) of the player mapped against 
    // that scenes ID. This is kind of like saving the players progress at each scene
    public static Dictionary<int, Dictionary<object, object>> sceneToPlayerAttributesMap = new Dictionary<int, Dictionary<object, object>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
       
    }
}
