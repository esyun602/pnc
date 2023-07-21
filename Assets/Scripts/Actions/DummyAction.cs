using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DummyAction : IAction
{
    private Action endCallback;
    private float runningTime = 1f;
    private float timePassed = 0;
    private bool isActive;
    private DummyHitBox DummyHitBoxPrefab;

    public bool IsActive => isActive;

    public DummyAction(DummyHitBox hitBox)
    {
        DummyHitBoxPrefab = hitBox;
    }

    void IAction.Trigger(Action endCallback)
    {
        this.endCallback = endCallback;
        var instance = UnityEngine.Object.Instantiate(DummyHitBoxPrefab);
        instance.transform.position = Cursor.Instance.WorldPos;
        timePassed = 0;
        isActive = true;
    }

    void IAction.UpdateFrame(float dt)
    {
        if (!IsActive)
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
