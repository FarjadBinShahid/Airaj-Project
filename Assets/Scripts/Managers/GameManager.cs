using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data Files")]
    [SerializeField]
    private TextAsset QuestionsData;

    public static GameManager Instance;
    private QuestionsController questionController;
    private List<Questions> questions;

    public static Action OnQuestionControllerInit;

    public QuestionsController QuestionController { get => questionController; set => questionController = value; }
    public List<Questions> Questions { get => questions; set => questions = value; }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            InitQuestionControlller();
        }
    }

    private void InitQuestionControlller()
    {
        QuestionController = new QuestionsController();
        Questions = JsonConvert.DeserializeObject<List<Questions>>(QuestionsData.ToString());
        QuestionController.Questions = Questions;
        OnQuestionControllerInit?.Invoke();
    }

  }