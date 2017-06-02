using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondTextControl : MonoBehaviour
{

    List<string> questions = new List<string>
    {
        "I am the life of the party.",
        "I fell little concerns for others.",
        "I am always prepared.",
        "I get stressed out easily.",
        "I have a rich vocabulary.",
        "I don't talk a lot.",
        "I am interested in people.",
        "I leave my belongings around.",

        "I am relaxed most of the time.",

        "I have difficulty understanding abstract ideas.",
        "I feel comfortable around people.",
        "I insult people.",
        "I pay attention to details.",
        "I worry about things.",
        "I have a vivid imagination.",
        "I keep in the background.",
        "I sympathize with others' feelings",
        "I make a mess of things.",
        "I seldom feel blue.",
        "I am not interested in abstract ideas.",
        "I start conversations.",
        "I am not intersted in other people's problems.",
        "I get chores done right away.",
        "I have excellent ideas.",
        "I have little to say.",
        "I have a soft heart.",
        "I often forget to put things back in their proper place.",
        "I get upset easily.",
        "I do not have a good imagination.",
        "I talk to a lot of different people at parties.",
        "I am not really interested in others.",
        "I like order",
        "I change my mood a lot.",
        "I am quick to understand things.",
        "I don't like to draw attention to myself.",
        "I take time out for others.",
        "I shirk my duties.",
        "I have frequent mood swings.",
        "I use difficult words.",
        "I don't mind being the center of attention.",
        "I feel other's emotions.",
        "I follow a schedule.",
        "I  get irritated easily.",
        "I spend time reflecting on things.",
        "I am quiet around strangers.",
        "I make people feel at ease.",
        "I am exacting in my work.",
        "I often feel blue.",
        "I am full of ideas."
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
    void Start()
    {
        numberOfQuestions = questions.Count;
        Debug.Log(PlayerPrefs.GetString("PlayerID"));

    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<TextMesh>().text = questions[currentQuestionIndex];

    }

    public void DoStuffWhenQuizEnds()
    {
        // calculate and store averages
        int extroversionScore = tallyScore(20, 0, true);
        int agreeablenessScore = tallyScore(14, 1, false);
        int conscientiousnessScore = tallyScore(14, 2, true);
        int neuroticimScore = tallyScore(38, 3, false);
        int opennessScore = tallyScore(8, 4, true);

        choiceAverages.Add(extroversionScore);
        choiceAverages.Add(agreeablenessScore);
        choiceAverages.Add(conscientiousnessScore);
        choiceAverages.Add(neuroticimScore);
        choiceAverages.Add(opennessScore);

        // print out the averages, for testing purposes only
        for (int i = 0; i < choiceAverages.Count; i++)
        {
            Debug.Log(choiceAverages[i] + " ");
        }

        // change the scene to "Game Over Scene"
        SceneManager.LoadScene(gameOverScene);
    }

    public int tallyScore(int baseScore, int startPoint, bool startOperation) {
        int finalScore = 0;
        bool addNumber = startOperation;
        
        for (int i = startPoint; i < playerChoices.Count; i = i + 5) {
            if (addNumber)
            {
                Debug.Log(playerChoices[i]);
                finalScore += playerChoices[i];
                addNumber = false;
            }
            else {
                Debug.Log(playerChoices[i]);
                finalScore -= playerChoices[i];
                addNumber = true;
            }
        }

        Debug.Log("fuck " + finalScore);
        return baseScore + finalScore;
    }
}



