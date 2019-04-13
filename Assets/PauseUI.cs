using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private GameScene scene = null;

    public void HandleOnClickResumeBtn()
    {
        gameObject.SetActive(false);

        scene.isPaused = false;
    }

    public void HandleOnClickExitBtn()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
