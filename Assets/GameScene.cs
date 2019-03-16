using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
    [SerializeField]
    private ChanHero chanHero = null;
    [SerializeField]
    private ObstacleManager obstacleManager = null;
    [SerializeField]
    private MainUI MainUi = null;
    [SerializeField]
    private PauseUI PauseUi = null;
    [SerializeField]
    private ResultUI ResultUi = null;

    public MainUI mainUI { get { return MainUi; } }
    public PauseUI pauseUI { get { return PauseUi; } }
    public ResultUI resultUI { get { return ResultUi; } }

    public bool IsEndGame = false;

    public bool isPaused = false;
    public bool isEndGame
    {
        set
        {
            IsEndGame = value;

            if(true == value)
            {
                ResultUi.gameObject.SetActive(true);
            }
            else
            {
                ResultUi.gameObject.SetActive(false);
            }
        }
        get
        {
            return IsEndGame;
        }
    }

    private void Start()
    {
    }

    void Update ()
    {
        float time = Time.deltaTime;

        if(isPaused == true)
        {
            time = 0;
        }

        obstacleManager.UpdateFrame(time * 3); // 이 값이 클수록 속도는 빨라진다
        mainUI.UpdateFrame(time);
    }

    private void LateUpdate()
    {
        float time = Time.deltaTime;

        if(null != chanHero)
        {
            chanHero.UpdateMoveXValue(mainUI.moveVecX);
            chanHero.LateUpdateFrame(time);
        }
    }
}
