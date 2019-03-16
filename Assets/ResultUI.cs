using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private GameObject WinObj = null;
    [SerializeField]
    private GameObject LoseObj = null;
    
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
}
