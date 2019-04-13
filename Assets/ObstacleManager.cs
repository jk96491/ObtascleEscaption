using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private Transform Trans = null;
    [SerializeField]
    private GameObject ObstacleObj = null;
    [SerializeField]
    private List<Obstacle> obstacles = new List<Obstacle>();
    [SerializeField]
    private List<Transform> startPosTranses = new List<Transform>();
    [SerializeField]
    private List<bool> usedPosition = new List<bool>();
    [SerializeField]
    private GameScene gameScene = null;

    public void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject ins = GameObject.Instantiate(ObstacleObj) as GameObject;

            if(Trans != null)
            {
                ins.transform.SetParent(Trans);

                Obstacle curObstacle = ins.GetComponent<Obstacle>();
                

                //curObstacle.SetPosition(new Vector3(startPosTranses[posIndex].localPosition.x, 1.8f, 0));
                curObstacle.SetActive(false);
                obstacles.Add(curObstacle);

                usedPosition.Add(false);
            }
        }
    }

    public void UpdateFrame(float time_)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if(obstacles[i].active == false)
            {
                float rand = Random.Range(0, 100f);
               
                if(rand < 5)
                {
                    GameScene.DifficultyManager difficulty = gameScene.GetCurrentDifficultyInfo();

                    if(obstacles[i].delayCount > difficulty.delayCount) // 이 값이 클수록 적게 떨어진다
                    {
                        int posIndex = Random.Range(0, 11);

                        if(usedPosition[posIndex] == true)
                        {
                            continue;
                        }
                        else
                        {
                            obstacles[i].SetPosition(new Vector3(startPosTranses[posIndex].localPosition.x, 2.3f, 0));
                            obstacles[i].SetActive(true);
                            obstacles[i].delayCount = 0;
                            obstacles[i].usedPosIndex = posIndex;
                            usedPosition[posIndex] = true;
                        }

                    }
                    else
                    {
                        obstacles[i].delayCount++;
                    }

                }
            }
            else
            {
                obstacles[i].MovePosition(new Vector3(0, -1, 0) * time_);
                Vector3 curPos = obstacles[i].GetPosition();

                if (curPos.y <= -2.3f)
                {
                    ResetObstacles(obstacles[i]);
                }
            }
        }
    }

    public void Hit(GameObject Obj)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if(obstacles[i].gameObject == Obj)
            {
                ResetObstacles(obstacles[i]);
                break;
            }
        }
    }

    private void ResetObstacles(Obstacle obstacles)
    {
        obstacles.SetActive(false);
        obstacles.delayCount = 0;
        usedPosition[obstacles.usedPosIndex] = false;
    }

    public void ResetAllObtascle()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            ResetObstacles(obstacles[i]);
        }
    }
}
