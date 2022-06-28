using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VideoHelper;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;

public class QuestionsController
{
    private List<Questions> questions;
    private TextAsset QuestionsData;
    private int rightAnswerCounter, leftAnswerCounter;
    private VideoController VideoController;
    private string videoName;
    private string imageName;
    private string colorName;

    private int percent;



    public List<Questions> Questions 
    { 
        get => questions;
        set
        {
            questions = value;
            double val = ((double)GameConstants.Percent / 100) * questions.Count;
            percent = (int) val;
        }
    }
    public int RightAnswerCounter { get => rightAnswerCounter; set => rightAnswerCounter = value; }
    public int LeftAnswerCounter { get => leftAnswerCounter; set => leftAnswerCounter = value; }
    public string VideoName 
    {
        get
        {
            if (LeftAnswerCounter > percent)
            {
                videoName = GameConstants.LeftVideoName;
            }
            else if (RightAnswerCounter > percent)
            {
                videoName = GameConstants.RightVideoName;
            }
            else
            {
                videoName = GameConstants.EqualVideoName;
            }
            return videoName;
        }
        set => videoName = value; }

    public string ImageName 
    {
        get
        {
            if (LeftAnswerCounter > percent)
            {
                imageName = GameConstants.LeftImageName;
            }
            else if (RightAnswerCounter > percent)
            {
                imageName = GameConstants.RightImageName;
            }
            else
            {
                imageName = GameConstants.EqualImageName;
            }
            return imageName;
        }
        set => imageName = value; 
    }

    

    public string ColorName 
    {
        get
        {
            if (LeftAnswerCounter > percent)
            {
                colorName = "Yellow";
            }
            else if (RightAnswerCounter > percent)
            {
                colorName = "Blue";
            }
            else
            {
                colorName = "Red";
            }
            return colorName;
        }
        set => colorName = value; 
    }

    public QuestionsController()
    {
        GameManager.OnQuestionControllerInit += InitController;
    }

    private void InitController()
    {
        ChangeKeywordColor();
    }

    private void ChangeKeywordColor()
    {
        
        for(int i = 0; i < Questions.Count; i++)
        {
            Questions[i].Question = KeywordColorChanger(Questions[i].Question, Questions[i].Keyword);
        }

    }

    private string KeywordColorChanger(string question, List<string> words)
    {
        for(int i = 0; i < words.Count; i++)
        {
            question = question.Replace(words[i], "<color=#" + GameConstants.KeywordColorCode + ">" + words[i] + "</color>");
        }
        return question;  
    }

    
}
