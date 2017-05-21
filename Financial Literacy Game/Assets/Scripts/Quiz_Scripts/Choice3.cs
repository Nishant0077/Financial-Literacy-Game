using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Choice3 : MonoBehaviour
{

    List<string> thirdChoice = new List<string>
        {
            "2", "2","2","2","2","2","2","2","2","2","2"

        };

     TextControl tc;
     NextButton nb;
    AudioSource audioSource;
    AudioClip audioClip;

    // Use this for initialization
    void Start()
    {
        tc = GetComponent<TextControl>();
        nb = GetComponent<NextButton>();
        GetComponent<TextMesh>().text = thirdChoice[0];
        audioSource = GameObject.FindGameObjectsWithTag("ClickSound")[0].GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (TextControl.currentQuestionIndex > -1)
        {
            GetComponent<TextMesh>().text = thirdChoice[TextControl.currentQuestionIndex];
          //  Debug.Log(firstChoice[TextControl.randQuestions]);
        }
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(audioClip);
        TextControl.selectedAnswer = gameObject.name;
        
            TextControl.playerChoices.Add(TextControl.selectedAnswer);
        Debug.Log("Answer chosen");

        Debug.Log("The current question index is: " + TextControl.currentQuestionIndex);
        Debug.Log("The player's choice is " + TextControl.selectedAnswer);

        Analytics.CustomEvent("Quiz Data", new Dictionary<string, object>
      {
        { "Index of the current Question: " + TextControl.currentQuestionIndex.ToString(),
                    "Player's Choice: " + TextControl.selectedAnswer}

      });

        if (TextControl.currentQuestionIndex < TextControl.numberOfQuestions - 1)
            TextControl.currentQuestionIndex++;

        else if (TextControl.currentQuestionIndex == TextControl.numberOfQuestions - 1)
            TextControl.currentQuestionIndex = 0;

    }
}
