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
        StartCoroutine(PlayVideo(GameManager.Instance.QuestionController.Url));
    }

    private IEnumerator PlayVideo(string url)
    {
        yield return new WaitForSeconds(2);
        VideoController.gameObject.SetActive(true);
        Debug.Log(url);
        VideoController.PrepareForUrl(url);

    }

    private void OnDisable()
    {
    }
}
