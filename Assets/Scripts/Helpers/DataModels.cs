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

    public Questions()
    {
    }

    public Questions(Questions value)
    {
        Question = value.Question;
        LeftAnswer = value.LeftAnswer;
        RightAnswer = value.RightAnswer;
    }

    public Questions(string question, string leftAnswer, string rightAnswer)
    {
        Question = question;
        LeftAnswer = leftAnswer;
        RightAnswer = rightAnswer;
    }
}

#endregion