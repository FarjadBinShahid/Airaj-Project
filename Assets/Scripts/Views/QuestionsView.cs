using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;
using Unity.VideoHelper;
using UnityEngine.Video;
using System;

public class QuestionsView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private TMP_Text QuestionText;
    [SerializeField]
    private Button btn_LeftAnswer;
    [SerializeField]
    private Button btn_RightAnswer;
    [SerializeField]
    private TMP_Text LeftAnswerText;
    [SerializeField]
    private TMP_Text RightAnswerText;
    [SerializeField]
    private GameObject videoCanvas;

    [Header("Data Files")]
    [SerializeField]
    private TextAsset QuestionsData;

    [Header("Video Player")]
    [SerializeField]
    private VideoController videoController;

    private int counter;
    private List<Questions> questions;
    private QuestionsController questionsController;

    public static Action OnVideoEnded;

    public QuestionsController QuestionsController { get => questionsController; set => questionsController = value; }
    public List<Questions> Questions { get => questions; set => questions = value; }
    public VideoController VideoController { get => videoController; set => videoController = value; }

    private void Awake()
    {
        QuestionsController = new QuestionsController(QuestionsData, videoController, videoCanvas);        
    }

    private void OnEnable()
    {
        ResetScreen();
        AddListeners();
        NextQuestion();
    }

    private void Start()
    {
        
    }

    private void AddListeners()
    {
        btn_LeftAnswer.onClick.AddListener(LeftAnswerSelected);
        btn_RightAnswer.onClick.AddListener(RightAnswerSelected);
    }

    private void RemoveListeners()
    {
        btn_RightAnswer.onClick.RemoveAllListeners();
        btn_LeftAnswer.onClick.RemoveAllListeners();
    }

    private void ResetScreen()
    {
        counter = -1;
        Questions = QuestionsController.Questions;
        QuestionsController.LeftAnswerCounter = 0;
        QuestionsController.RightAnswerCounter = 0;
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void RightAnswerSelected()
    {
        QuestionsController.RightAnswerCounter++;
        NextQuestion();
    }

    private void LeftAnswerSelected()
    {
        QuestionsController.LeftAnswerCounter++;
        NextQuestion();
    }

    private void NextQuestion()
    {
        counter++;
        if(counter >= Questions.Count)
        {
            QuestionsController.ShowVideo();
            gameObject.SetActive(false);
            return;
        }
        QuestionText.text = Questions[counter].Question;
        LeftAnswerText.text = Questions[counter].LeftAnswer;
        RightAnswerText.text = Questions[counter].RightAnswer;

    }

    private void ShowReplayScreen(VideoPlayer source)
    {
        gameObject.SetActive(false);
    }

    /*private void GenerateJson()
    {
        Questions q = new Questions("question", "left Answer", "Right Answer");
        List<Questions> qs = new List<Questions>() { q, q, q };
        string json = JsonConvert.SerializeObject(qs, Formatting.Indented);
        File.WriteAllText( Application.dataPath+"/Resources/Data/QuestionsData.json",json);
    }*/

    
}
