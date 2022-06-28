using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeView : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField]
    private Button btn_Continue;
    [SerializeField]
    private GameObject QuestionsView;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;

    private bool isFirstTime = true;



    private void OnEnable()
    {
        AddListeners();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && isFirstTime)
        {
            isFirstTime = false;
            animator.Play("Logo");
        }

    }

    private void AddListeners()
    {
        btn_Continue?.onClick.AddListener(ContinuePressed);
    }

    private void RemoveListeners()
    {
        btn_Continue?.onClick.RemoveListener(ContinuePressed);
    }

    private void ContinuePressed()
    {
        gameObject.SetActive(false);
        QuestionsView.SetActive(true);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
}
