using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;

public class MultiDisplayHandler : MonoBehaviour
{
    [SerializeField]
    private VideoPresenter videoPresenter;

    private void Awake()
    {

        for(int i = 1; i < Display.displays.Length; i++)
        {
            Debug.Log(i);
            Display.displays[i].Activate();
            videoPresenter.TargetDisplay = 1;
        }
    }
}
