[System.Serializable]
public class ExternalQuestion
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
    public bool addScore;
}