using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCardGameController : MonoBehaviour {

    public static Button b;

	// Use this for initialization
	void Start () {

        b = GameObject.FindGameObjectsWithTag("BackToInboxCreditCardScene")[0].GetComponentInChildren<Button>();
        b.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
