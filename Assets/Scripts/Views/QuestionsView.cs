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
using System.Linq;

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
    private Button btn_ResetQuestions;
    [SerializeField]
    private TMP_Text LeftAnswerText;
    [SerializeField]
    private TMP_Text RightAnswerText;
    [SerializeField]
    private TMP_Text HintHeadingText;
    [SerializeField]
    private TMP_Text HintTextText;
    [SerializeField]
    private GameObject Hint;
    [SerializeField]
    private Image QuestionNumberImage;
    [SerializeField]
    private GameObject ProcessingView;


    [Header("Text Writer")]
    [SerializeField]
    private TextWriter questionTextWriter;
    [SerializeField]
    private TextWriter leftAnswerTextWriter;
    [SerializeField]
    private TextWriter rightAnswerTextWriter;
    private int counter;
    private List<Questions> questions;
    private Dictionary<string, Sprite> questionSprites = new Dictionary<string, Sprite>();
    private QuestionsController questionsController;

    public QuestionsController QuestionsController { get => questionsController; set => questionsController = value; }
    public List<Questions> Questions { get => questions; set => questions = value; }

    private void Awake()
    {
        questionSprites = Resources.LoadAll(GameConstants.UIAssetFolder + GameConstants.QuestionsAssetFolder, typeof(Sprite)).Cast<Sprite>().ToDictionary(x => x.name, y => y);
              
        QuestionsController = GameManager.Instance.QuestionController;
        
    }

    private void OnEnable()
    {
        ResetScreen();
        AddListeners();
    }

    private void Start()
    {
        //GenerateJson();
    }

    private void AddListeners()
    {
        questionTextWriter.OnTextComplete += delegate 
        { 
            TextCompleted(leftAnswerTextWriter, Questions[counter].LeftAnswer);
        };
        leftAnswerTextWriter.OnTextComplete += delegate 
        { 
            TextCompleted(rightAnswerTextWriter, Questions[counter].RightAnswer); 
        };
        rightAnswerTextWriter.OnTextComplete += delegate 
        { 
            btn_RightAnswer.interactable = true; 
            btn_LeftAnswer.interactable = true;
            btn_ResetQuestions.interactable = true; 
        };
        btn_LeftAnswer.onClick.AddListener(LeftAnswerSelected);
        btn_RightAnswer.onClick.AddListener(RightAnswerSelected);
        btn_ResetQuestions.onClick.AddListener(ResetScreen);
    }

    private void RemoveListeners()
    {
        btn_RightAnswer.onClick.RemoveAllListeners();
        btn_LeftAnswer.onClick.RemoveAllListeners();
        btn_ResetQuestions.onClick.RemoveAllListeners();
    }

    private void ResetScreen()
    {
        counter = -1;
        Questions = QuestionsController.Questions;
        QuestionsController.LeftAnswerCounter = 0;
        QuestionsController.RightAnswerCounter = 0;
        NextQuestion();
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
        LeftAnswerText.text = String.Empty;
        RightAnswerText.text = String.Empty;
        btn_LeftAnswer.interactable = false;
        btn_RightAnswer.interactable = false;
        btn_ResetQuestions.interactable = false;
        counter++;
        if(counter >= Questions.Count)
        {
            ProcessingView.SetActive(true);
            //QuestionsController.ShowVideo();
            gameObject.SetActive(false);
            return;
        }
        QuestionNumberImage.sprite = questionSprites[(counter+1).ToString()];
        questionTextWriter.AddWriter(Questions[counter].Question);

        if (Questions[counter].Hint != null)
        {
            Hint.SetActive(true);
            HintHeadingText.text = Questions[counter].Hint.Heading;
            HintTextText.text = Questions[counter].Hint.Text;
        }
        else
        {
            Hint.SetActive(false);
        }

    }

    private void TextCompleted(TextWriter textWriter, string text)
    {
        textWriter.AddWriter(text);
    }

    

    private void ShowReplayScreen(VideoPlayer source)
    {
        gameObject.SetActive(false);
    }


    // used to generate dummy json from model
    private void GenerateJson()
    {
        Questions q = new Questions("question", "left Answer", "Right Answer",new List<string> {"asd", "sdasd" } ,new Hint("Heading", "Text"));
        List<Questions> qs = new List<Questions>() { q, q, q };
        string json = JsonConvert.SerializeObject(qs, Formatting.Indented);
        File.WriteAllText( Application.dataPath+"/Resources/Data/QuestionsData1.json",json);
    }

    
}
