using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForkAction : IAction
{
    private Action endCallback;
    private float runningTime = 2f;
    private float handRestoreTime = 1f;
    private float timePassed = 0;
    private bool isActive;
    private GameObject fork, redTarget;
    private DummyFork[] forks;
    private BoxCollider2D[] forkCol = new BoxCollider2D[3];

    private Vector3 targetPos;


    public ForkAction(GameObject _fork, GameObject _red)
    {
        fork = _fork;
        redTarget = _red;
        forks = fork.GetComponentsInChildren<DummyFork>();
        forkCol[0] = forks[0].gameObject.GetComponent<BoxCollider2D>();
        forkCol[1] = forks[1].gameObject.GetComponent<BoxCollider2D>();
        forkCol[2] = forks[2].gameObject.GetComponent<BoxCollider2D>();
        forkCol[0].enabled = false;
        forkCol[1].enabled = false;
        forkCol[2].enabled = false;
        forks[1].gameObject.SetActive(false);
        forks[2].gameObject.SetActive(false);
    }

    void IAction.Trigger(Action endCallback)
    {
        this.endCallback = endCallback;

        // 빨간색 잠깐 표시
        targetPos = new Vector3(Cursor.Instance.WorldPos.x , fork.transform.position.y, 0f)
                     + new Vector3(0f, -2.3f, 0f);
        redTarget.SetActive(true);
        redTarget.transform.position = targetPos;
        // 공격 고정 위치 값
        targetPos = targetPos + new Vector3(0f, 2.3f, 0f);

        SoundManager.Instance.ForkSound();
        
        timePassed = 0;
        isActive = true;
    }
    // 스킬 실행 중
    void IAction.UpdateFrame(float dt)
    {
        

        Chef.Instance.HandManager.SetHandState(ChefHandState.Fork);
        // 첫 포크 생성
        if(fork.activeSelf == false)
        {
            fork.SetActive(true);
        }


        if (!isActive)
        {
            // 포크 천천히 이동

            // 팔 움직임
            Chef.Instance.HandManager.UpdateHandPosition();
            // fork.transform.position = Vector3.MoveTowards(fork.transform.position,
            //                                 new Vector3(Cursor.Instance.WorldPos.x + 3.25f, fork.transform.position.y, 0f), 3f * Time.deltaTime);
            return;
        }

        // 클릭 0.5초 안에: 타겟 향해 좌우 이동
        if(timePassed >= 0f && timePassed < 0.5f)
        {
            fork.transform.position = Vector3.Lerp(fork.transform.position, targetPos, 
                                                    timePassed / 0.5f);
            
        }

        // 클릭 0.5초 후: 포크를 빠르게 내려 찍음
        else if(timePassed >= 0.5f && timePassed < 1f)
        {
            fork.transform.position =Vector3.Lerp(fork.transform.position, targetPos + new Vector3(0, -4f, 0), 
                                                    (timePassed - 0.5f) / 0.5f);
            redTarget.SetActive(false);
            forkCol[0].enabled = true;
            forkCol[1].enabled = true;
            forkCol[2].enabled = true;
        }

        // 다시 올라간 후 0.5초 안에 장착 상태로 돌아감
        else
        {
            // 팔 움직임
            Chef.Instance.HandManager.UpdateHandPosition();
            fork.transform.position = Vector3.Lerp(fork.transform.position, new Vector3(Cursor.Instance.WorldPos.x , targetPos.y, 0f), 
                                                    (timePassed - 1f) / 0.5f);
            forkCol[0].enabled = false;
            forkCol[1].enabled = false;
            forkCol[2].enabled = false;
        }

        timePassed += dt;
        if(timePassed > runningTime)
        {
            // 커서 위치로 포크 즉각 이동
            //fork.transform.position = new Vector3(Cursor.Instance.WorldPos.x + 3.25f, fork.transform.position.y, 0f);
            isActive = false;
            endCallback();
        }

        // 스킬 사용했는데 넘어가야 할 때
        else if (timePassed > handRestoreTime && timePassed - dt < handRestoreTime)
        {
            if (Chef.Instance.ActionHandler.IsActionChanged)
            {
                // 높이 돌아오기
                fork.transform.position = new Vector3(fork.transform.position.x, targetPos.y, 0);
                forkCol[0].enabled = false;
                forkCol[1].enabled = false;
                forkCol[2].enabled = false;
                Chef.Instance.HandManager.EndHandAction();
                endCallback?.Invoke();
                isActive = false;
                return;
            }

            Chef.Instance.HandManager.EndHandAction();
        }
    }
}
