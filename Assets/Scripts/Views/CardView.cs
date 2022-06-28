using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private TMP_Text colorText;
    [SerializeField]
    private GameObject welcomeView;
    [SerializeField]
    private float restartTimeInSeconds = 10f;

    IEnumerator co;

    private void OnEnable()
    {
        string colorName = GameManager.Instance.QuestionController.ColorName;
        string color = GameConstants.colors[colorName];
        colorText.text = "Pick <color=#" + color + ">" + colorName + "</color> card";
        co = DisableView();
        StartCoroutine(co);
    }

    IEnumerator DisableView()
    {
        yield return new WaitForSeconds(restartTimeInSeconds);
        welcomeView.SetActive(true);
        gameObject.SetActive(false);
    }

    /*private void OnMouseDown()
    {
        if(co != null)
        {
            StopCoroutine(co);
        }
        welcomeView.SetActive(true);
        gameObject.SetActive(false);
    }*/


}