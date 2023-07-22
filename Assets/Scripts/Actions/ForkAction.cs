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

    private Vector3 targetPos;

    public ForkAction(UnityEngine.Object fork, UnityEngine.Object red)
    {
        DummyForkPrefab = fork;
        redTarget = red;
    }

    void IAction.Trigger(Action endCallback)
    {
        this.endCallback = endCallback;

        // 빨간색 잠깐 표시 // 공격 고정 위치 값
        targetPos = new Vector3(Cursor.Instance.WorldPos.x, fork.transform.position.y, 0f);
        UnityEngine.GameObject.Destroy(UnityEngine.Object.Instantiate(redTarget, targetPos, Quaternion.identity) as GameObject, 0.2f);
        
        //targetPos = targetPos + new Vector3(0f, 4f, 0f);
        
        timePassed = 0;
        isActive = true;
    }
    // 스킬 실행 중
    void IAction.UpdateFrame(float dt)
    {
        // 첫 포크 생성
        if(!fork)
        {
            fork = (UnityEngine.Object.Instantiate(DummyForkPrefab) as GameObject);
        }


        if (!isActive)
        {
            // 포크 천천히 이동
            fork.transform.position = new Vector3(Vector3.MoveTowards(fork.transform.position, new Vector3(Cursor.Instance.WorldPos.x, fork.transform.position.y, 0f), 3f * Time.deltaTime).x, fork.transform.position.y, fork.transform.position.z);
            return;
        }

        // 클릭 0.5초 안에: 타겟 향해 좌우 이동
        if(timePassed >= 0f && timePassed < 0.5f)
        {
            //fork.transform.position = new Vector3(Vector3.MoveTowards(fork.transform.position, Cursor.Instance.WorldPos, 3f * Time.deltaTime).x, fork.transform.position.y, fork.transform.position.z);
            fork.transform.position = Vector3.Lerp(fork.transform.position, targetPos, timePassed / 0.5f);
        }

        // 클릭 0.5초 후: 포크를 빠르게 내려 찍음
        else if(timePassed >= 0.5f && timePassed < 1f)
        {
            fork.transform.position =Vector3.Lerp(fork.transform.position, targetPos + new Vector3(0, -3f, 0), (timePassed - 0.5f) / 0.5f);
            //Vector3.MoveTowards(fork.transform.position, targetPos + new Vector3(0, -3f, 0), 10f * Time.deltaTime);
        }

        // 다시 올라간 후 0.5초 안에 장착 상태로 돌아감
        else
        {
            //fork.transform.position = Vector3.MoveTowards(fork.transform.position, new Vector3(Cursor.Instance.WorldPos.x, prevY, 0f), 3f * Time.deltaTime);
            fork.transform.position = Vector3.Lerp(fork.transform.position, new Vector3(Cursor.Instance.WorldPos.x, targetPos.y, 0f), (timePassed - 1f) / 0.5f);
        }

        timePassed += dt;
        if(timePassed > runningTime)
        {
            // 커서 위치로 포크 즉각 이동
            fork.transform.position = new Vector3(Cursor.Instance.WorldPos.x, fork.transform.position.y, 0f);
            isActive = false;
            endCallback();
        }
    }
}
