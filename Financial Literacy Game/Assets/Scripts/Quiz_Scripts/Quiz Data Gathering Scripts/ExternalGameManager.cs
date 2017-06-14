using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExternalGameManager : MonoBehaviour
{

    public ExternalQuestion[] questions;
    public static List<ExternalQuestion> unansweredQuestions;
    private ExternalQuestion currentQuestion;

    static List<float> opennessScore = new List<float>();
    static List<float> conscientiousnessScore = new List<float>();
    static List<float> extraversionScore = new List<float>();
    static List<float> agreeablenessScore = new List<float>();
    static List<float> neuroticismScore = new List<float>();

    public static int questionIndex;

    public float opennessLevel = 8;
    public float conscientiousnessLevel = 14;
    public float extraversionLevel = 20;
    public float agreeablenessLevel = 14;
    public float neuroticismLevel = 38;

    public static bool replaceResponse = false;

    private static int backTurns = 0;

    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Slider playerInput;

    private float playerResponse = 0;

    void Start()
    {

        if (unansweredQuestions == null)
        {
            unansweredQuestions = questions.ToList<ExternalQuestion>();
        }

        if (questionIndex == unansweredQuestions.Count)
        {
            opennessLevel += opennessScore.Sum();
            conscientiousnessLevel += conscientiousnessScore.Sum();
            neuroticismLevel += neuroticismScore.Sum();
            extraversionLevel += extraversionScore.Sum();
            agreeablenessLevel += agreeablenessScore.Sum();

            Debug.Log(QuizResults());
            SceneManager.LoadScene("QuizEnd");
        }

        currentQuestion = unansweredQuestions[questionIndex];
        questionText.text = currentQuestion.question;
    }

    public void NextQuestion()
    {

        if (backTurns > 0)
        {
            replaceResponse = true;
            backTurns = backTurns - 1;
        }
        else
        {
            replaceResponse = false;
        }

        if (!currentQuestion.addScore)
        {
            playerResponse = -(playerInput.value);
        }
        else {
            playerResponse = playerInput.value;
        }

        if (!replaceResponse)
        {
            switch (currentQuestion.personality)
            {
                case ExternalQuestion.questionType.Openness:
                    opennessScore.Add(playerResponse);
                    break;
                case ExternalQuestion.questionType.Agreeableness:
                    agreeablenessScore.Add(playerResponse);
                    break;
                case ExternalQuestion.questionType.Conscientiousness:
                    conscientiousnessScore.Add(playerResponse);
                    break;
                case ExternalQuestion.questionType.Neuroticism:
                    neuroticismScore.Add(playerResponse);
                    break;
                case ExternalQuestion.questionType.Extraversion:
                    extraversionScore.Add(playerResponse);
                    break;
                default:
                    throw new System.ArgumentException("Not valid personality");
            }
        }
        else
        {

            Debug.Log(questionIndex);
            Debug.Log("What is going on: " + (questionIndex % 2));
            switch (currentQuestion.personality)
            {
                case ExternalQuestion.questionType.Openness:
                    if (opennessScore.Count != 0)
                    {
                        opennessScore[questionIndex % 10] = playerResponse;
                    }
                    else
                    {
                        opennessScore.Add(playerResponse);
                    }
                    break;
                case ExternalQuestion.questionType.Agreeableness:
                    if (agreeablenessScore.Count != 10)
                    {
                        agreeablenessScore[questionIndex % 10] = playerResponse;
                    }
                    else
                    {
                        agreeablenessScore.Add(playerResponse);
                    }
                    break;
                case ExternalQuestion.questionType.Conscientiousness:
                    if (conscientiousnessScore.Count != 0)
                    {
                        conscientiousnessScore[questionIndex % 10] = playerResponse;
                    }
                    else
                    {
                        conscientiousnessScore.Add(playerResponse);
                    }
                    break;
                case ExternalQuestion.questionType.Neuroticism:
                    if (neuroticismScore.Count != 0)
                    {
                        neuroticismScore[questionIndex % 10] = playerResponse;
                    }
                    else
                    {
                        neuroticismScore.Add(playerResponse);
                    }
                    break;
                case ExternalQuestion.questionType.Extraversion:
                    if (extraversionScore.Count != 0)
                    {
                        extraversionScore[questionIndex % 10] = playerResponse;
                    }
                    else
                    {
                        extraversionScore.Add(playerResponse);
                    }
                    break;
                default:
                    throw new System.ArgumentException("Not valid personality");
            }
        }

        questionIndex = questionIndex + 1;
        Debug.Log(QuizResults());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBack()
    {
        backTurns = backTurns + 1;
        questionIndex = questionIndex - 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        replaceResponse = true;
    }

    public string QuizResults()
    {
        string quizResults = null;
        quizResults = quizResults + "Quiz Results: ";

        int resultIndex = 0;

        quizResults += "\n";
        quizResults += "Openness Responses: \n";
        foreach (float response in opennessScore)
        {
            resultIndex++;
            quizResults += resultIndex + ": " + response + "\n";
        }
        quizResults += "Openness Average: " + opennessLevel + "\n";
        resultIndex = 0;

        quizResults += "\n";
        quizResults += "Conscientiousness Responses: \n";
        foreach (float response in conscientiousnessScore)
        {
            resultIndex++;
            quizResults += resultIndex + ": " + response + "\n";
        }
        quizResults += "Conscientiousness Average: " + conscientiousnessLevel + "\n";
        resultIndex = 0;

        quizResults += "\n";
        quizResults += "Extraversion Responses: \n";
        foreach (float response in extraversionScore)
        {
            resultIndex++;
            quizResults += resultIndex + ": " + response + "\n";
        }
        quizResults += "Extraversion Average: " + extraversionLevel + "\n";
        resultIndex = 0;

        quizResults += "\n";
        quizResults += "Agreeableness Responses: \n";
        foreach (float response in agreeablenessScore)
        {
            resultIndex++;
            quizResults += resultIndex + ": " + response + "\n";
        }
        quizResults += "Agreeableness Average: " + agreeablenessLevel + "\n";
        resultIndex = 0;

        quizResults += "\n";
        quizResults += "Neuroticism Responses: \n";
        foreach (float response in neuroticismScore)
        {
            resultIndex++;
            quizResults += resultIndex + ": " + response + "\n";
        }
        quizResults += "Neuroticism Average: " + neuroticismLevel + "\n";
        resultIndex = 0;

        return quizResults;
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene("FirstQuiz");
    }

    public void LoadSecondQuiz()
    {
        SceneManager.LoadScene("SecondQuiz");
    }
}
