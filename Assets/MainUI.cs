using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

    [SerializeField]
    private UILabel gameTimeLabel = null;
    [SerializeField]
    private UIEventListener LeftArrowEventListner = null;
    [SerializeField]
    private UIEventListener RightArrowEventListner = null;
    [SerializeField]
    private UISprite HPSprite = null;
    [SerializeField]
    private UILabel HPLabel = null;
    [SerializeField]
    private GameScene scene = null;
    [SerializeField]
    private PauseUI pauseUI = null;
    [SerializeField]
    private ResultUI resultUI;
    [SerializeField]
    private ChanHero hero;
    [SerializeField]
    private GameObject MoveButtonObj = null;

    [Header("[게임 시작 연출]")]
    [SerializeField]
    private TweenPosition tipLabelTweenPos = null;
    [SerializeField]
    private TweenScale tipLabelTweenScale = null;

    public delegate void EndFinishGameStartEffectDel();
    public EndFinishGameStartEffectDel endFinishGameStartEffectDel = null;

    public float moveVecX = 0f;

    private void Start()
    {
        if (LeftArrowEventListner != null)
        {
            LeftArrowEventListner.onPress = OnPress;
        }

        if (RightArrowEventListner != null)
        {
            RightArrowEventListner.onPress = OnPress;
        }

        if (null != tipLabelTweenPos)
            tipLabelTweenPos.SetOnFinished(OnFinishGameStartEffect);

        if(null != MoveButtonObj)
        {
#if UNITY_ANDROID
            MoveButtonObj.SetActive(true);
#else
            MoveButtonObj.SetActive(false);
#endif
        }
    }

    public void UpdateFrame(float time_)
    {
        scene.gameTime += time_;
        if (null != gameTimeLabel)
            gameTimeLabel.text = ((int)scene.gameTime).ToString();
    }

    private void OnPress(GameObject Obj, bool Press)
    {
        if(scene.isPaused == true)
        {
            moveVecX = 0;
            return;
        }

        if(false == Press)
        {
            moveVecX = 0;
        }
        else
        {
            if (Obj == LeftArrowEventListner.gameObject)
            {
                moveVecX = -1;
            }
            else
            {
                moveVecX = 1;
            }
        }
    }

    private void SetHPBar(float Value)
    {
        if(HPSprite != null)
        {
            HPSprite.fillAmount = Value;

            if(Value > 0.45)
            {
                HPSprite.color = new Color32(0x00, 0xff, 0x02, 255);
            }
            else if(Value <= 0.45 && Value > 0.2)
            {
                HPSprite.color = new Color32(0xF4, 0xFF, 0x00, 255);
            }
            else
            {
                HPSprite.color = new Color32(0xFF, 0x00, 0x00, 255);
            }
        }
    }

    private void SetHPLabel(int HP, int MaxHP)
    {
        if(null != HPLabel)
        {
            HPLabel.text = string.Format("{0}/{1}", HP, MaxHP);
        }
    }

    public void SetHpInfo(int HP, int MaxHP, float Value)
    {
        SetHPBar(Value);
        SetHPLabel(HP, MaxHP);
    }
    
    public void HandleOnClickPauseBtn()
    {
        if (true == scene.isPaused)
            return;

        if (null != pauseUI)
            pauseUI.gameObject.SetActive(true);

        scene.isPaused = true;
    }

    public void PlayGameStartEffect()
    {
        hero.SetHpInit();
        scene.gameTime = 0;
        resultUI.gameObject.SetActive(false);
        tipLabelTweenPos.gameObject.SetActive(true);
        tipLabelTweenPos.enabled = true;
        tipLabelTweenScale.enabled = true;

        if (null != tipLabelTweenPos)
            tipLabelTweenPos.PlayForward();

        if (null != tipLabelTweenScale)
            tipLabelTweenScale.PlayForward();
    }

    public void OnFinishGameStartEffect()
    {
        StartCoroutine(DelayFinishGameStartEffect());
    }

    IEnumerator DelayFinishGameStartEffect()
    {
        yield return new WaitForSeconds(1f);


        tipLabelTweenPos.gameObject.SetActive(false);

        tipLabelTweenPos.ResetToBeginning();
        tipLabelTweenScale.ResetToBeginning();

        if (null != endFinishGameStartEffectDel)
            endFinishGameStartEffectDel();
    }
}
