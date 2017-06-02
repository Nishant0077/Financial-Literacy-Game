using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SecondChoice4 : MonoBehaviour
{

    int choiceInt = 3;
    string choiceText = "3";

    SecondTextControl tc = new SecondTextControl();
    NextButton nb;
    AudioSource audioSource;
    AudioClip audioClip;
    public string idOfPlayer;


    // Use this for initialization
    void Start()
    {

        nb = GetComponent<NextButton>();
        GetComponent<TextMesh>().text = choiceText;
        audioSource = GameObject.FindGameObjectsWithTag("ClickSound")[0].GetComponent<AudioSource>();
        audioClip = audioSource.clip;

    }

    private void Awake()
    {
        idOfPlayer = PlayerPrefs.GetString("PlayerID");
    }

    // Update is called once per frame
    void Update()
    {
        if (SecondTextControl.currentQuestionIndex > -1)
        {
            GetComponent<TextMesh>().text = choiceText;
            // Debug.Log(firstChoice[TextControl.randQuestions]);
        }
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(audioClip);
        SecondTextControl.selectedAnswer = gameObject.name;


        SecondTextControl.playerChoices.Add(choiceInt);
        // Debug.Log(TextControl.selectedAnswer);

        Debug.Log("The current question index is: " + SecondTextControl.currentQuestionIndex);
        Debug.Log("The player's choice is " + SecondTextControl.selectedAnswer);

      //  Analytics.CustomEvent(idOfPlayer, new Dictionary<string, object>
      //{
      //  { "Index of the current Question: " + SecondTextControl.currentQuestionIndex.ToString(),
      //              "Player's Choice: " + SecondTextControl.selectedAnswer}

      //});

        if (SecondTextControl.currentQuestionIndex < SecondTextControl.numberOfQuestions - 1)
            SecondTextControl.currentQuestionIndex++;

        else if (SecondTextControl.currentQuestionIndex == SecondTextControl.numberOfQuestions - 1)
            tc.DoStuffWhenQuizEnds();

    }
}
