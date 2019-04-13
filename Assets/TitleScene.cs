using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void HandleOnClickEasyBtn()
    {
        DificultyManager.Instant.dificulty = GameScene.Difficulty.EASY;
        EnterGame();
    }

    public void HandleOnClicNormalBtn()
    {
        DificultyManager.Instant.dificulty = GameScene.Difficulty.NORMAL;
        EnterGame();
    }

    public void HandleOnClickHardBtn()
    {
        DificultyManager.Instant.dificulty = GameScene.Difficulty.HARD;
        EnterGame();
    }

    public void HandleOnClicHellBtn()
    {
        DificultyManager.Instant.dificulty = GameScene.Difficulty.HELL;
        EnterGame();
    }

    private void EnterGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
