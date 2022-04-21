using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class QuestionsController
{
    private List<Questions> questions;
    private TextAsset QuestionsData;
    private int rightAnswerCounter, leftAnswerCounter;
    public List<Questions> Questions { get => questions; set => questions = value; }
    public int RightAnswerCounter { get => rightAnswerCounter; set => rightAnswerCounter = value; }
    public int LeftAnswerCounter { get => leftAnswerCounter; set => leftAnswerCounter = value; }

    public QuestionsController(TextAsset questionsData)
    {
        QuestionsData = questionsData;
        InitController();   
    }

    private void InitController()
    {
        GetJsonData();  
    }

    private void GetJsonData()
    {
        Questions = JsonConvert.DeserializeObject<List<Questions>>(QuestionsData.ToString());
        Debug.Log(Questions.Count);
    }

    public void ShowVideo()
    {
        if(LeftAnswerCounter > RightAnswerCounter)
        {
            Debug.Log("left Video");
        }
        else if(RightAnswerCounter > LeftAnswerCounter)
        {
            Debug.Log("right Video");
        }
        else
        {
            Debug.Log("equal Video");
        }
    }
}
