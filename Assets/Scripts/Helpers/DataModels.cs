using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataModels
{

}


#region Questions
public class Questions
{
    public string Question;
    public string LeftAnswer;
    public string RightAnswer;
    public List<string> Keyword = new List<string>();
    public Hint Hint;

    public Questions()
    {
    }

    public Questions(Questions value)
    {
        Question = value.Question;
        LeftAnswer = value.LeftAnswer;
        RightAnswer = value.RightAnswer;
        Keyword = value.Keyword;
        Hint = value.Hint;
    }

    public Questions(string question, string leftAnswer, string rightAnswer, List<string> keyword, Hint hint)
    {
        Question = question;
        LeftAnswer = leftAnswer;
        RightAnswer = rightAnswer;
        Keyword = keyword;
        this.Hint = hint;
    }
}

public class Hint
{
    public string Heading;
    public string Text;

    public Hint()
    {
    }

    public Hint(string heading, string text)
    {
        Heading = heading;
        Text = text;
    }
}
#endregion