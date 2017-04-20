using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextControl : MonoBehaviour {

    List<string> questions = new List<string>
    {
        "Which planet do you live on?",
        "What color is your blood?",
        "What is 2 + 2?",
        "What is your favourite sport?",
        "Who came first; chicken or egg?"
    };

    public static string selectedAnswer;
    public static int randQuestions = -1;
    public static List<string> playerChoices = new List<string>();
    public TextMesh nextButton;
    
    // NextButton nb;

	// Use this for initialization
	void Start () {
       // NextButton.isClicked = false;
	}

    // Update is called once per frame
    void Update() {

        if (randQuestions == -1)
        {
            randQuestions = Random.Range(0, questions.Count);
            //nb.isClicked = false;
            
        }

        if (randQuestions > -1)
        {
            GetComponent<TextMesh>().text = questions[randQuestions];
           // nb.isClicked = false;
            Debug.Log("Player has attempted these many questions: " + playerChoices.Count);
        }
    
    }

   
}
