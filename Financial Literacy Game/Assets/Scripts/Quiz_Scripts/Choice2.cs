using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Choice2 : MonoBehaviour
{

    List<string> secondChoice = new List<string>
        {
            "1", "1","1","1","1", "1", "1","1","1","1", "1"            
            
        };

     TextControl tc;
    AudioSource audioSource;
    AudioClip audioClip;
    public string idOfPlayer;

    // Use this for initialization
    void Start()
    {
        //nb = GetComponent<NextButton>();
        tc = GetComponent<TextControl>();
        GetComponent<TextMesh>().text = secondChoice[0];
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
        if (TextControl.currentQuestionIndex > -1)
        {
            GetComponent<TextMesh>().text = secondChoice[TextControl.currentQuestionIndex];
            //Debug.Log(secondChoice[TextControl.randQuestions]);
        }
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(audioClip);
        TextControl.selectedAnswer = gameObject.name;
        
            TextControl.playerChoices.Add(TextControl.selectedAnswer);

        Debug.Log("The current question index is: " + TextControl.currentQuestionIndex);
        Debug.Log("The player's choice is " + TextControl.selectedAnswer);

        Analytics.CustomEvent(idOfPlayer, new Dictionary<string, object>
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
