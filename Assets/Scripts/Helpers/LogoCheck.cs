using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoCheck : MonoBehaviour
{

    [SerializeField]
    private GameObject questionsViewHolder;

    public void enablequestionViewHolder()
    {
        questionsViewHolder.SetActive(true);
    }
}
