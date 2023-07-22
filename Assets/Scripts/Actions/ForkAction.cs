using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForkAction : IAction
{
    private Action endCallback;
    private float runningTime = 1f;
    private float timePassed = 0;
    private bool isActive;
    private DummyHitBox DummyHitBoxPrefab;

    public ForkAction(DummyHitBox hitBox)
    {
        DummyHitBoxPrefab = hitBox;
    }

    void IAction.Trigger(Action endCallback)
    {
        this.endCallback = endCallback;
        // 공격 위치로 즉각 이동
        // 빨간색 잠깐 표시
        var instance = UnityEngine.Object.Instantiate(DummyHitBoxPrefab);
        instance.transform.position = Cursor.Instance.WorldPos;
        timePassed = 0;
        isActive = true;
    }

    // 스킬 실행 중
    void IAction.UpdateFrame(float dt)
    {
        if (!isActive)
        {
            return;
        }

        timePassed += dt;
        if(timePassed > runningTime)
        {
            isActive = false;
            endCallback();
        }
    }
}
