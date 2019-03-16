using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanHero : MonoBehaviour
{
    [SerializeField]
    private Animator chanAni = null;
    [SerializeField]
    private Transform chanTran = null;
    [SerializeField]
    private ObstacleManager obstacleManager = null;
    [SerializeField]
    private GameScene scene = null;

    [SerializeField]
    public int HP = 100;

    public int MaxHp = 50;

    public float moveVec = 0f;
    public bool attacked = false;

    public void Start()
    {
        HP = MaxHp;
        scene.mainUI.SetHpInfo(HP, MaxHp, (float)HP / MaxHp);
    }

    public void UpdateMoveXValue(float moveVec_)
    {
        moveVec = moveVec_;
    }

    public void LateUpdateFrame(float time_)
    {
        if (scene.isPaused == true)
            moveVec = 0;

        if (moveVec == 1)
            chanTran.transform.localRotation = Quaternion.Euler(0, 90, 0);
        else if (moveVec == -1)
            chanTran.transform.localRotation = Quaternion.Euler(0, -90, 0);

        chanTran.transform.localPosition += new Vector3(moveVec, 0, 0) * time_ * 3;


        Vector3 curPos = chanTran.transform.localPosition;

        if (curPos.x <= -3.46)
            chanTran.transform.localPosition = new Vector3(-3.46f, curPos.y, curPos.z);
        if(curPos.x >= 3.46)
            chanTran.transform.localPosition = new Vector3(3.46f, curPos.y, curPos.z);

        chanAni.SetBool("MoveState", moveVec == 1 || moveVec == -1);
    }

    public void AttackEnd()
    {
        if (null != chanAni)
        {
            chanAni.CrossFade("WAIT00", 1f);
            attacked = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obta")
        {
            obstacleManager.Hit(collision.gameObject);
            HP -= (int)Random.Range(4, 9);
            if(HP <= 0)
            {
                HP = 0;
                scene.isPaused = true;
                scene.isEndGame = true;

                scene.resultUI.gameObject.SetActive(true);
                scene.resultUI.SetResult(false);
            }
            scene.mainUI.SetHpInfo(HP, MaxHp, (float)HP / MaxHp);
        }
    }
}
