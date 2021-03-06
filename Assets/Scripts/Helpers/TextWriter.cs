using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextWriter : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private TMP_Text uiText;
    [SerializeField]
    private float timePerCharacter;

    [Header("Audio Source")]
    [SerializeField]
    private bool enableAudio = false;
    [SerializeField]
    private AudioSource audioSource;

    private string textToWrite;
    private int characterIndex;
    private float timer;
    private bool isTextDone = true;

    public Action OnTextComplete;
    public Action OnTextSkip;

    private void FixedUpdate()
    {
        if (!isTextDone)
        {

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                try
                {
                    uiText.text = textToWrite.Substring(0, characterIndex);
                }
                catch (Exception e)
                {
                    Debug.Log(uiText.gameObject.name);
                }
                if (characterIndex >= textToWrite.Length)
                {
                    isTextDone = true;
                    if (enableAudio)
                    {
                        audioSource.Pause();
                    }
                    OnTextComplete?.Invoke();

                    return;
                }


                if (characterIndex + 1 < textToWrite.Length)
                {
                    if (textToWrite[characterIndex + 1] == '<')
                    {
                        if (textToWrite[characterIndex + 1] == '<' && textToWrite[characterIndex + 2] == '/')
                        {
                            characterIndex += 8;
                        }
                        else
                        {
                            characterIndex += 15;
                        }
                    }
                }
            }
        }
    }

    public void AddWriter(string textToWrite)
    {
        this.textToWrite = textToWrite;
        characterIndex = 0;
        isTextDone = false;
        if (enableAudio)
        {
            audioSource.Play();
        }
    }

    public void SkipAnimation(string text)
    {
        isTextDone = true;
        if (enableAudio)
        {
            audioSource.Pause();
        }
        uiText.text = text;
        OnTextSkip?.Invoke();
    }

}
