using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using UnityEngine.UI;

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

    [Header("Data Files")]
    [SerializeField]
    private TextAsset QuestionsData;

    private int counter;
    private List<Questions> questions;
    private QuestionsController questionsController;
    public QuestionsController QuestionsController { get => questionsController; set => questionsController = value; }
    public List<Questions> Questions { get => questions; set => questions = value; }

    private void Awake()
    {
        QuestionsController = new QuestionsController(QuestionsData);
    }

    private void OnEnable()
    {
        counter = -1;
        Questions = QuestionsController.Questions;
        AddListeners();
        NextQuestion();
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

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void RightAnswerSelected()
    {
        QuestionsController.RightAnswerCounter++;
        Debug.Log("Right Answer Selected "+QuestionsController.RightAnswerCounter);
        NextQuestion();
    }

    private void LeftAnswerSelected()
    {
        QuestionsController.LeftAnswerCounter++;
        Debug.Log("Left Answer Selected " + QuestionsController.LeftAnswerCounter);
        NextQuestion();
    }

    private void NextQuestion()
    {
        counter++;
        if(counter >= Questions.Count)
        {
            QuestionsController.ShowVideo();
            return;
        }
        Debug.Log("Question Changed "+counter);
        QuestionText.text = Questions[counter].Question;
        LeftAnswerText.text = Questions[counter].LeftAnswer;
        RightAnswerText.text = Questions[counter].RightAnswer;


    }

    



    private void GenerateJson()
    {
        Questions q = new Questions("question", "left Answer", "Right Answer");
        List<Questions> qs = new List<Questions>() { q, q, q };
        string json = JsonConvert.SerializeObject(qs, Formatting.Indented);
        File.WriteAllText( Application.dataPath+"/Resources/Data/QuestionsData.json",json);
    }

    
}
