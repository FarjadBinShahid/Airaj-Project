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
    string url;

    private int percent;

   // public static Action<string> OnPlayVideo;


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
    public string Url 
    {
        get
        {
            if (LeftAnswerCounter > percent)
            {
                url = System.IO.Path.Combine(Application.streamingAssetsPath, GameConstants.LeftVideoName);
            }
            else if (RightAnswerCounter > percent)
            {
                url = System.IO.Path.Combine(Application.streamingAssetsPath, GameConstants.RightVideoName);
            }
            else
            {
                url = System.IO.Path.Combine(Application.streamingAssetsPath, GameConstants.EqualVideoName);
            }
            Debug.Log(url);
            return url;
        }
        set => url = value; }

    public QuestionsController()
    {
        GameManager.OnQuestionControllerInit += InitController;
    }

    /*public QuestionsController(TextAsset questionsData, VideoController videoController, GameObject videoCanvas)
    {
        QuestionsData = questionsData;
        VideoController = videoController;
        this.videoCanvas = videoCanvas;
        InitController();   
    }*/

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
