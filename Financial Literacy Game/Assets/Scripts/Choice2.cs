using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice2 : MonoBehaviour
{

    List<string> secondChoice = new List<string>
        {
            "Mars",
            "Blue",
            "4",
            "Cricket",
            "Chicken"
            
        };

     TextControl tc;
    AudioSource audioSource;
    AudioClip audioClip;

    // Use this for initialization
    void Start()
    {
        //nb = GetComponent<NextButton>();
        tc = GetComponent<TextControl>();
        GetComponent<TextMesh>().text = secondChoice[0];
        audioSource = GameObject.FindGameObjectsWithTag("ClickSound")[0].GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (TextControl.randQuestions > -1)
        {
            GetComponent<TextMesh>().text = secondChoice[TextControl.randQuestions];
            //Debug.Log(secondChoice[TextControl.randQuestions]);
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
