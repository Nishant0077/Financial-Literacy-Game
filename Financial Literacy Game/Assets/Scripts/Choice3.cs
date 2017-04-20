using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice3 : MonoBehaviour
{

    List<string> thirdChoice = new List<string>
        {
            "Saturn",
            "Pink",
            "8",
            "Hockey",
            "Both"
            
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
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = thirdChoice[TextControl.randQuestions];
          //  Debug.Log(firstChoice[TextControl.randQuestions]);
        }
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(audioClip);
        TextControl.selectedAnswer = gameObject.name;
        
            TextControl.playerChoices.Add(TextControl.selectedAnswer);
        Debug.Log("Answer chosen");
        TextControl.randQuestions = -1;

    }
}
