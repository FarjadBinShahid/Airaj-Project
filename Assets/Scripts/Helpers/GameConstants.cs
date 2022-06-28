using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameConstants
{
    #region Paths

    public const string ImageAssetFolder = "Images/";
    public const string VideoAssetFolder = "Videos/";
    public const string UIAssetFolder = "UI/";
    public const string QuestionsAssetFolder = "Questions/";
    public const string LeftVideoName = "Left Video.mp4";
    public const string RightVideoName = "Right Video.mp4";
    public const string EqualVideoName = "Equal Video.mp4";
    public const string LeftImageName = "Left Image";
    public const string RightImageName = "Right Image";
    public const string EqualImageName = "Equal Image";

    #endregion

    #region Messages

    #endregion

    public const int VideoSceneIndex = 1;
    public const int Percent = 60;
    public static readonly Color KeywordColor = new Color(0.7137255f,1f, 0.08235294f,1f);
    public const string KeywordColorCode = "B6FF15";
    public const string LeftColor = "FFEC2B";
    public const string RightColor = "00A4FF";
    public const string EqualColor = "FF002E";
    public static Dictionary<string, string> colors = new Dictionary<string, string> 
    {   
        {"Yellow", LeftColor },
        {"Blue", RightColor },
        {"Red", EqualColor } 
    };

}
