using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private GameScene scene = null;

    public void HandleOnClickResumeBtn()
    {
        gameObject.SetActive(false);

        scene.isPaused = false;
    }
}
