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

    static public float opennessLevel = 8;
    static public float conscientiousnessLevel = 14;
    static public float extraversionLevel = 20;
    static public float agreeablenessLevel = 14;
    static public float neuroticismLevel = 38;

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
        if (questionIndex == unansweredQuestions.Count)
        {
            opennessLevel += opennessScore.Sum();
            conscientiousnessLevel += conscientiousnessScore.Sum();
            neuroticismLevel += neuroticismScore.Sum();
            extraversionLevel += extraversionScore.Sum();
            agreeablenessLevel += agreeablenessScore.Sum();

            Debug.Log("HEer");
            sendQuizResults();
            SceneManager.LoadScene("QuizEnd");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

    void sendQuizResults() {
        WWWForm form = new WWWForm();
        form.AddField("timeIDPost", System.DateTime.Now.ToString());

        form.AddField("tqOpennessPost", GameManager.opennessLevel.ToString());
        form.AddField("tqConscientiousnessPost", GameManager.conscientiousnessLevel.ToString());
        form.AddField("tqNeuroticismPost", GameManager.neuroticismLevel.ToString());
        form.AddField("tqAgreeablenessPost", GameManager.agreeablenessLevel.ToString());
        form.AddField("tqExtraversionPost", GameManager.extraversionLevel.ToString());


        form.AddField("tqCreditScorePost", (600 + (20 * (GameManager.opennessLevel - GameManager.extraversionLevel))).ToString());
        form.AddField("tqStartingSalaryPost", (10 + (GameManager.extraversionLevel + GameManager.agreeablenessLevel) / 2).ToString());

        int sPercentage = 5 + (int) GameManager.conscientiousnessLevel * 3;
        int dPercentage = 20 + (int)GameManager.neuroticismLevel * 2;
        int fPercentage = 100 - sPercentage - dPercentage;
        form.AddField("tqSavingPercentagePost", sPercentage);
        form.AddField("tqDiscretionaryPercentagePost", dPercentage);
        form.AddField("tqFixedPercentagePost", fPercentage);
        
        form.AddField("cqOpennessPostPost", opennessLevel.ToString());
        form.AddField("cqConscientiousnessPost", conscientiousnessLevel.ToString());
        form.AddField("cqNeuroticismPost", neuroticismLevel.ToString());
        form.AddField("cqAgreeablenessPost", agreeablenessLevel.ToString());
        form.AddField("cqExtraversionPost", extraversionLevel.ToString());

        form.AddField("cqCreditScorePost", (600 + (20 * (opennessLevel - extraversionLevel))).ToString());
        form.AddField("cqStartingSalaryPost", (10 + (extraversionLevel + agreeablenessLevel) / 2).ToString());

        sPercentage = 5 + (int)conscientiousnessLevel * 3;
        dPercentage = 20 + (int)neuroticismLevel * 2;
        fPercentage = 100 - sPercentage - dPercentage;
        form.AddField("cqSavingPercentagePost", sPercentage);
        form.AddField("cqDiscretionaryPercentagePost", dPercentage);
        form.AddField("cqFixedPercentagePost", fPercentage);
        Debug.Log("Here too");
        WWW www = new WWW("http://thrivefinancialgamedatabase.000webhostapp.com/QuizResults.php", form);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www) {

        yield return www;
        Debug.Log("fuck yeah");

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
