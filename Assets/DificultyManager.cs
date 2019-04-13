using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultyManager : MonoSingleton<DificultyManager>
{
    public GameScene.Difficulty dificulty = GameScene.Difficulty.EASY;

    public bool IsObjCreated = false;

    private void Start()
    {
        if(false == IsObjCreated)
        {
            IsObjCreated = true;
            DontDestroyOnLoad(gameObject);
        }
    }
}
