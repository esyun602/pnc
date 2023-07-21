using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupLaserAction : IAction
{
    private float runningTime = 5f;
    private float castingTime = 2f;
    private float timePassed = 0;
    private bool isActive = false;

    private DummyLaserHitbox dummyLaserHitbox;
    private DummyLaserHitbox dummyLaserInstance;
    private Vector2 triggerPos;

    private System.Action endCallback;

    public SyrupLaserAction(DummyLaserHitbox dummyLaserHitbox)
    {
        this.dummyLaserHitbox = dummyLaserHitbox;
    }
    void IAction.Trigger(System.Action endCallback)
    {
        timePassed = 0;
        isActive = true;
        triggerPos = Cursor.Instance.WorldPos;

        this.endCallback = endCallback;
    }

    void IAction.UpdateFrame(float dt)
    {
        if (!isActive)
        {
            return;
        }

        timePassed += dt;
        if (timePassed > runningTime)
        {
            dummyLaserInstance.gameObject.SetActive(false);
            endCallback?.Invoke();
            isActive = false;
        }
        else if(timePassed > castingTime && timePassed - dt < castingTime)
        {
            InstantiateLaser();
        }
    }

    private void InstantiateLaser()
    {
        dummyLaserInstance = Object.Instantiate(dummyLaserHitbox);
        dummyLaserInstance.transform.position = new Vector2(0, triggerPos.y);
    }
}
