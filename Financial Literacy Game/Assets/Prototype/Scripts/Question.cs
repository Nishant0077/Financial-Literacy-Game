[System.Serializable]
public class Question
{
    public enum questionType
    {
        Openness,
        Conscientiousness,
        Extraversion,
        Agreeableness,
        Neuroticism
    }

    public questionType personality; 
    public string question;
}