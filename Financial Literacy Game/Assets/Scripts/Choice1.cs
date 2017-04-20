﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice1 : MonoBehaviour {

    List<string> firstChoice = new List<string>
        {
            "Earth",
            "Red",
            "6",
            "Football",
            "Egg"
           
        };

     TextControl tc;
     NextButton nb;
    AudioSource audioSource;
    AudioClip audioClip;


    // Use this for initialization
    void Start () {
        tc = GetComponent<TextControl>();
        nb = GetComponent<NextButton>();
        GetComponent<TextMesh>().text = firstChoice[0];
        audioSource = GameObject.FindGameObjectsWithTag("ClickSound")[0].GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }
	
	// Update is called once per frame
	void Update () {
        if (TextControl.currentQuestionIndex > -1)
        {
            GetComponent<TextMesh>().text = firstChoice[TextControl.currentQuestionIndex];
           // Debug.Log(firstChoice[TextControl.randQuestions]);
        }
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(audioClip);
        TextControl.selectedAnswer = gameObject.name;

      
            TextControl.playerChoices.Add(TextControl.selectedAnswer);
            Debug.Log("Answer chosen");

        if (TextControl.currentQuestionIndex < TextControl.numberOfQuestions - 1)
            TextControl.currentQuestionIndex++;

        else if (TextControl.currentQuestionIndex == TextControl.numberOfQuestions - 1)
            TextControl.currentQuestionIndex = 0;

    }
}
