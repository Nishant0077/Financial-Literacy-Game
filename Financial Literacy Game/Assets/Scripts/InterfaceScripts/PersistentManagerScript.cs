using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentManagerScript : MonoBehaviour {

	public static PersistentManagerScript Instance { get; private set; }

    public List<Button> sceneButtonList = new List<Button>();
    public static int numberOfSceneButtons = 11;
    public static int mostRecentActiveSceneButtonIndex = 4;
    public static int currentlyClickedSceneButtonindex = 0;
    public bool hasFirstSceneBeenLoaded = false;
    public static bool hasTheGameBeenCompleted = false;

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
