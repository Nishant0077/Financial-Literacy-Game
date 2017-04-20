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
    public static int currentQuestionIndex = 0;
    public static List<string> playerChoices = new List<string>();
    public TextMesh nextButton;
    public static int numberOfQuestions;
    
    // NextButton nb;

	// Use this for initialization
	void Start () {
        numberOfQuestions = questions.Count;

    }

    // Update is called once per frame
    void Update() {

       
            GetComponent<TextMesh>().text = questions[currentQuestionIndex];

           

           // nb.isClicked = false;
            Debug.Log("Player has attempted these many questions: " + playerChoices.Count);
        }
    
    }

   

