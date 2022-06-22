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
    [SerializeField]
    private int printCopies = 1;
    [SerializeField]
    private string printerName;


    private void OnEnable()
    {
        StartCoroutine(PlayVideo(GameManager.Instance.QuestionController.VideoName));
        PrintImage(GameManager.Instance.QuestionController.ImageName);
    }

    private IEnumerator PlayVideo(string videoName)
    {
        yield return new WaitForSeconds(2);
        VideoController.gameObject.SetActive(true);
        Debug.Log(videoName);
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        VideoController.PrepareForUrl(url);
    }

    private void PrintImage(string imageName)
    {
        string url = GameConstants.ImageAssetFolder + imageName;
        Sprite sprite = Resources.Load<Sprite>(url);
        var texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        texture2D.SetPixels(pixels);
        texture2D.Apply();
        Print.PrintTexture(texture2D.EncodeToPNG(), printCopies, printerName);
    }

    private void OnDisable()
    {
    }
}
