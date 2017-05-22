using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonControl : MonoBehaviour {

    public GameObject textBox;
    TextChanger textChangerAccessor;

     private static int questionSelector;
         
	// Use this for initialization
	void Start () {
        textChangerAccessor = gameObject.GetComponent<TextChanger>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ButtonIsClicked() {
        textChangerAccessor.questionSelector++;
        string thisPosition = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        Debug.Log(thisPosition);
    }
}
