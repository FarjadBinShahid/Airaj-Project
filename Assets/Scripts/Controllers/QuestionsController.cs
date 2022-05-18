using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VideoHelper;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class QuestionsController
{
    private List<Questions> questions;
    private TextAsset QuestionsData;
    private int rightAnswerCounter, leftAnswerCounter;
    private VideoController VideoController;
    //string url;
    GameObject videoCanvas;

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

    public QuestionsController(TextAsset questionsData, VideoController videoController, GameObject videoCanvas)
    {
        QuestionsData = questionsData;
        VideoController = videoController;
        this.videoCanvas = videoCanvas;
        InitController();   
    }

    private void InitController()
    {
        GetJsonData();
    }

    private void GetJsonData()
    {
        Questions = JsonConvert.DeserializeObject<List<Questions>>(QuestionsData.ToString());
    }

    public void ShowVideo()
    {

        string url;
        videoCanvas.SetActive(true);

        //check which video to play
        if (LeftAnswerCounter > percent)
        {
            url = System.IO.Path.Combine(Application.streamingAssetsPath, GameConstants.LeftVideoName);
        }
        else if(RightAnswerCounter > percent)
        {
            url = System.IO.Path.Combine(Application.streamingAssetsPath, GameConstants.RightVideoName);
        }
        else
        {
            url = System.IO.Path.Combine(Application.streamingAssetsPath, GameConstants.EqualVideoName);
        }
        VideoController.PrepareForUrl(url);

    }
}
