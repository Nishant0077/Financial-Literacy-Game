using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject g1;
    public GameObject g2;
    public static Button b;
	// Use this for initialization
	void Start () {

        b = GameObject.FindGameObjectsWithTag("BackToInboxEmployerScene")[0].GetComponentInChildren<Button>();
        b.gameObject.SetActive(false);
        g1.SetActive(false);
        g2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
