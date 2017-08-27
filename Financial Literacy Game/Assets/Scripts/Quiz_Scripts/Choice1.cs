using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Choice1 : MonoBehaviour {

    int choiceInt = 0;
    string choiceText = "0";

    TextControl tc = new TextControl();
    NextButton nb;
    AudioSource audioSource;
    AudioClip audioClip;
    public string idOfPlayer;          // id of player will be the time they started the quiz


    // Use this for initialization
    void Start ()
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
    void Update () {
        if (TextControl.currentQuestionIndex > -1)
        {
            GetComponent<TextMesh>().text = choiceText;
           // Debug.Log(firstChoice[TextControl.randQuestions]);
        }
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(audioClip);
        TextControl.selectedAnswer = gameObject.name;

      
            TextControl.playerChoices.Add(choiceInt);
        // Debug.Log(TextControl.selectedAnswer);

        Debug.Log("The current question index is: " + TextControl.currentQuestionIndex);
        Debug.Log("The player's choice is " + TextControl.selectedAnswer);

      //      Analytics.CustomEvent(idOfPlayer, new Dictionary<string, object>
      //{
      //  { "Index of the current Question: " + TextControl.currentQuestionIndex.ToString(),
      //              "Player's Choice: " + TextControl.selectedAnswer}
      //});

        if (TextControl.currentQuestionIndex < TextControl.numberOfQuestions - 1)
            TextControl.currentQuestionIndex++;

        else if (TextControl.currentQuestionIndex == TextControl.numberOfQuestions - 1)
            tc.DoStuffWhenQuizEnds();

    }
}
