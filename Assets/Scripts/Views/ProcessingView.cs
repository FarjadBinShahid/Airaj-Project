using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;

public class ProcessingView : MonoBehaviour
{
    private VideoController VideoController;

    private void OnEnable()
    {
        QuestionsController.OnPlayVideo += OnPlayVideo;
    }

    private void OnPlayVideo(string url)
    {
        StartCoroutine(PlayVideo(url));
    }

    private IEnumerator PlayVideo(string url)
    {
        yield return new WaitForSeconds(2);
        VideoController.PrepareForUrl(url);

    }

    private void OnDisable()
    {
        QuestionsController.OnPlayVideo -= OnPlayVideo;
    }
}
