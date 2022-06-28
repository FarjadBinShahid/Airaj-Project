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

    private void OnEnable()
    {
        string colorName = GameManager.Instance.QuestionController.ColorName;
        string color = GameConstants.colors[colorName];
        colorText.text = "Pick <color=#" + color + ">" + colorName + "</color> card";
        StartCoroutine(DisableView());
    }

    IEnumerator DisableView()
    {
        yield return new WaitForSeconds(15f);
        welcomeView.SetActive(true);
        gameObject.SetActive(false);
    }


}
