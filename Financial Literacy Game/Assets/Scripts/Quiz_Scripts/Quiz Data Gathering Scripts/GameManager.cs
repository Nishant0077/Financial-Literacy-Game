using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Question[] questions;
    public static List<Question> unansweredQuestions;
    private Question currentQuestion;

    static List<float> opennessScore = new List<float>();
    static List<float> conscientiousnessScore = new List<float>();
    static List<float> extraversionScore = new List<float>();
    static List<float> agreeablenessScore = new List<float>();
    static List<float> neuroticismScore = new List<float>();

    public static int questionIndex;

    public float opennessLevel = 0;
    public float conscientiousnessLevel = 0;
    public float extraversionLevel = 0;
    public float agreeablenessLevel = 0;
    public float neuroticismLevel = 0;

    public static bool replaceResponse = false;

    private static int backTurns = 0;

    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Slider playerInput;

    void Start() {

        if (unansweredQuestions == null) {
            unansweredQuestions = questions.ToList<Question>();
        }

        if (questionIndex == unansweredQuestions.Count)
        {
            opennessLevel = opennessScore.Average();
            conscientiousnessLevel = conscientiousnessScore.Average();
            neuroticismLevel = neuroticismScore.Average();
            extraversionLevel = extraversionScore.Average();
            agreeablenessLevel = agreeablenessScore.Average();
            SceneManager.LoadScene("GameOver");
        }

        currentQuestion = unansweredQuestions[questionIndex];
        questionText.text = currentQuestion.question;
    }

    public void NextQuestion() {

        if (backTurns != 0) {
            replaceResponse = true;
            backTurns = backTurns - 1;
        }

        if (!replaceResponse)
        {
            switch (currentQuestion.personality)
            {
                case Question.questionType.Openness:
                    opennessScore.Add(playerInput.value);
                    break;
                case Question.questionType.Agreeableness:
                    agreeablenessScore.Add(playerInput.value);
                    break;
                case Question.questionType.Conscientiousness:
                    conscientiousnessScore.Add(playerInput.value);
                    break;
                case Question.questionType.Neuroticism:
                    neuroticismScore.Add(playerInput.value);
                    break;
                case Question.questionType.Extraversion:
                    extraversionScore.Add(playerInput.value);
                    break;
                default:
                    throw new System.ArgumentException("Not valid personality");
            }
        }
        else {
            switch (currentQuestion.personality)
            {
                case Question.questionType.Openness:
                    if (opennessScore.Count != 0)
                    {
                        opennessScore[questionIndex % 2] = playerInput.value;
                    }
                    else{
                        opennessScore.Add(playerInput.value);
                    }
                    break;
                case Question.questionType.Agreeableness:
                    if (agreeablenessScore.Count != 0)
                    {
                        agreeablenessScore[questionIndex % 2] = playerInput.value;
                    }
                    else {
                        agreeablenessScore.Add(playerInput.value);
                    }
                    break;
                case Question.questionType.Conscientiousness:
                    if (conscientiousnessScore.Count != 0)
                    {
                        conscientiousnessScore[questionIndex % 2] = playerInput.value;
                    }
                    else {
                        conscientiousnessScore.Add(playerInput.value);
                    }
                    break;
                case Question.questionType.Neuroticism:
                    if (neuroticismScore.Count != 0)
                    {
                        neuroticismScore[questionIndex % 2] = playerInput.value;
                    }
                    else {
                        neuroticismScore.Add(playerInput.value);
                    }
                    break;
                case Question.questionType.Extraversion:
                    if (extraversionScore.Count != 0)
                    {
                        extraversionScore[questionIndex % 2] = playerInput.value;
                    }
                    else {
                        extraversionScore.Add(playerInput.value);
                    }
                    break;
                default:
                    throw new System.ArgumentException("Not valid personality");
            }
        }

        questionIndex = questionIndex + 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBack() {
        backTurns = backTurns + 1;
        questionIndex = questionIndex - 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        replaceResponse = true;
    }
}
