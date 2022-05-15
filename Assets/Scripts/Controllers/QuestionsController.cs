using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VideoHelper;
using UnityEngine.Video;

public class QuestionsController
{
    private List<Questions> questions;
    private TextAsset QuestionsData;
    private int rightAnswerCounter, leftAnswerCounter;
    private VideoController VideoController;
    private VideoClip LeftVideo, RightVideo, EqualVideo;

    public List<Questions> Questions { get => questions; set => questions = value; }
    public int RightAnswerCounter { get => rightAnswerCounter; set => rightAnswerCounter = value; }
    public int LeftAnswerCounter { get => leftAnswerCounter; set => leftAnswerCounter = value; }

    public QuestionsController(TextAsset questionsData, VideoController videoController, VideoClip leftVideo, VideoClip rightVideo, VideoClip equalVideo)
    {
        QuestionsData = questionsData;
        VideoController = videoController;
        LeftVideo = leftVideo;
        RightVideo = rightVideo;
        EqualVideo = equalVideo;
        InitController();   
    }

    private void InitController()
    {
        GetJsonData();  
    }

    private void GetJsonData()
    {
        Questions = JsonConvert.DeserializeObject<List<Questions>>(QuestionsData.ToString());
        //Debug.Log(Questions.Count);
    }

    public void ShowVideo()
    {
        VideoController.gameObject.SetActive(true);
        if (LeftAnswerCounter > RightAnswerCounter)
        {
            VideoController.PrepareForClip(LeftVideo);
        }
        else if(RightAnswerCounter > LeftAnswerCounter)
        {
            VideoController.PrepareForClip(RightVideo);
        }
        else
        {
            VideoController.PrepareForClip(EqualVideo);
        }
    }
}
