using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.UI;

public class ReplayView : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField]
    private Button btn_ReplayVideo;
    [SerializeField]
    private Button btn_SurveyAgain;


    [Header("Video Player")]
    [SerializeField]
    private VideoController videoController;

    [Header("Question View")]
    [SerializeField]
    private GameObject questionView;

    private void OnEnable()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        btn_ReplayVideo.onClick.AddListener(ReplayVideo);
        btn_SurveyAgain.onClick.AddListener(SurveyAgain);
    }

    private void RemoveListeners()
    {
        btn_SurveyAgain.onClick.RemoveAllListeners();
        btn_ReplayVideo.onClick.RemoveAllListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void ReplayVideo ()
    {
        videoController.gameObject.SetActive(true);
        videoController.VideoPlayer.Play();
        gameObject.SetActive(false);
    }

    private void SurveyAgain()
    {
        questionView.SetActive(true);
        gameObject.SetActive(false);
    }

}
