using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;


public class TextControl : MonoBehaviour {

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

        "When you watch content online on sites like YouTube or Netflix," + '\n' +
        "you tend to watch new shows/movies rather than those that you’ve seen multiple times",

        "You are the risk taker in your friend group"
    };

    List<string> personalityCatagories = new List<string>
    {
        "Opennes to Experience",
        "Conscientiousness",
        "Extroversion",
        "Agreeableness",
        "Neuroticism"
    };

    public static string selectedAnswer;
    public static int currentQuestionIndex = 0;
    public static List<int> playerChoices = new List<int>();
    public static List<int> choiceAverages = new List<int>();
    public TextMesh nextButton;
    public static int numberOfQuestions;
    public static bool isOver = false;
    string gameOverScene = "gameOverScene";
    
    // NextButton nb;

	// Use this for initialization
	void Start () {
        numberOfQuestions = questions.Count;
        Debug.Log(PlayerPrefs.GetString("PlayerID"));

    }

    // Update is called once per frame
    void Update() {       

            GetComponent<TextMesh>().text = questions[currentQuestionIndex];    
        
        }

    public void DoStuffWhenQuizEnds()
    {       
        // calculate and store averages
            for (int i = 0; i < playerChoices.Count; i++)
            {
                if (i % 2 == 0)
                {
                    int average = (playerChoices[i] + playerChoices[i + 1]) / 2;
                    choiceAverages.Add(average);
                }
            }
        
            // print out the averages, for testing purposes only
        for (int i = 0; i < choiceAverages.Count; i++)
        {
            Analytics.CustomEvent(PlayerPrefs.GetString("PlayerID"), new Dictionary<string, object>
          {
            {personalityCatagories[i].ToString() + " Score: ",
            choiceAverages[i]
        }

    });
        }

        // change the scene to "Game Over Scene"
        SceneManager.LoadScene("intermScene");
    }
    
    }

   

