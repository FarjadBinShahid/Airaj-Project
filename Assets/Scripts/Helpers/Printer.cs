using LCPrinter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField]
    private int printCopies = 1;
    [SerializeField]
    private string printerName;

    public void PrintImage()
    {
        string imageName = GameManager.Instance.QuestionController.ImageName;
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



}
