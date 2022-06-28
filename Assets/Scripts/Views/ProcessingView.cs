using LCPrinter;
using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;

public class ProcessingView : MonoBehaviour
{
    [SerializeField]
    private GameObject videoCanvas;
    [SerializeField]
    private VideoController VideoController;
    


    private void OnEnable()
    {
        StartCoroutine(PlayVideo(GameManager.Instance.QuestionController.VideoName));  
    }

    private IEnumerator PlayVideo(string videoName)
    {
        yield return new WaitForSeconds(2);
        VideoController.gameObject.SetActive(true);
        Debug.Log(videoName);
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        VideoController.PrepareForUrl(url);
    }

    

    private void OnDisable()
    {
    }
}
