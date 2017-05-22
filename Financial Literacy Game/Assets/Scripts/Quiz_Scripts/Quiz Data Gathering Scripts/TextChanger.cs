using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {

    Text quizQuestion;

    List<string> questions = new List<string>
    {
        "I plan out my trips to the mall/grocery store",
        "I tend to have a lot of energy when I go to work",
        "I prefer going out to staying in",
        "My friends say would say I talk a lot",
        "Do you care for other people’s problems",
        "You tend to comply with orders given by authority figures",
        "You feel like you worry more than the average person",
        "If you could, would you completely change who you are",

        "When you watch content online on sites like YouTube or Netflix, " +
        "you tend to watch new shows/movies rather than those that you’ve seen multiple times",

        "You are the risk taker in your friend group"
    };

    public int questionSelector = 0;
    public int testScore = 0;

    // Use this for initialization
    void Start () {
        quizQuestion = gameObject.GetComponent<Text>();
        quizQuestion.text = questions[questionSelector];
 
    }

    // Update is called once per frame
    void Update () {
        if (questionSelector < questions.Count) {
            quizQuestion.text = questions[questionSelector];
        }
        else {
            Debug.Log(" ");
        }
    }
}
