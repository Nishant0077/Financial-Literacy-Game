using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerSchool : MonoBehaviour {

    public GameObject hoverText1;
    public GameObject hoverText2;
    public GameObject hoverText3;
    public static Button b;

	// Use this for initialization
	void Start () {
        b = GameObject.FindGameObjectsWithTag("BackToInboxSchoolScene")[0].GetComponentInChildren<Button>();
        b.gameObject.SetActive(false);
        hoverText1.SetActive(false);
        hoverText2.SetActive(false);
        hoverText3.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
