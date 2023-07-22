using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForkAction : IAction
{
    private Action endCallback;
    private float runningTime = 2f;
    private float timePassed = 0;
    private bool isActive;
    private GameObject fork;
    private UnityEngine.Object DummyForkPrefab, redTarget;

    public ForkAction(UnityEngine.Object fork, UnityEngine.Object red)
    {
        DummyForkPrefab = fork;
        redTarget = red;
    }

    void IAction.Trigger(Action endCallback)
    {
        this.endCallback = endCallback;
        // 공격 위치로 포크 즉각 이동
        fork.transform.position = Cursor.Instance.WorldPos;
        // 빨간색 잠깐 표시
        UnityEngine.GameObject.Destroy(UnityEngine.Object.Instantiate(redTarget, Cursor.Instance.WorldPos, Quaternion.identity) as GameObject, 0.2f);
        timePassed = 0;
        isActive = true;
    }
    Vector3 dir;
    // 스킬 실행 중
    void IAction.UpdateFrame(float dt)
    {
        // 첫 포크 생성
        if(!fork)
        {
            fork = (UnityEngine.Object.Instantiate(DummyForkPrefab) as GameObject);
        }

        // 포크 천천히 이동
        fork.transform.position = Vector3.MoveTowards(fork.transform.position, Cursor.Instance.WorldPos, 2f * Time.deltaTime);

        if (!isActive)
        {
            return;
        }

        // 클릭 0.5초 후: 포크를 빠르게 내려찍음
        if(timePassed >= 0.5f && timePassed < 1f)
        {
            //fork.transform
        }

        // 클릭 1초 후: 0.5초간 다시 올라간 후 장착 상태로 돌아감
        else if(timePassed >= 1f)
        {

        }

        timePassed += dt;
        if(timePassed > runningTime)
        {
            isActive = false;
            endCallback();
        }
    }
}
