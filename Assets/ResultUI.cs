using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private GameObject WinObj = null;
    [SerializeField]
    private GameObject LoseObj = null;
    [SerializeField]
    private UILabel ScoreLabel = null;
    
    public void SetResult(bool Win)
    {
        if(null != WinObj)
        {
            WinObj.SetActive(Win);
        }
        if(null != LoseObj)
        {
            LoseObj.SetActive(!Win);
        }
    }    

    public void SetScoreLabel(int Score)
    {
        if(null != ScoreLabel)
        {
            ScoreLabel.text = string.Format("Score : {0}", Score);
        }
    }
}
