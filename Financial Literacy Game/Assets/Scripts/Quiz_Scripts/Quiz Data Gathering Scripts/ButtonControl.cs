using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadFirstScene()
    {
        SceneManager.LoadScene("FirstQuiz");
    }

    public void LoadSecondQuiz()
    {
        SceneManager.LoadScene("SecondQuiz");
    }
}
