using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupLaserAction : IAction
{
    private float runningTime = 5f;
    private float castingTime = 2f;
    private float timePassed = 0;
    private bool isActive = false;

    private ObjectPool laserPool;
    private PooledObject dummyLaserInstance;
    private Vector2 triggerPos;

    private System.Action endCallback;

    public SyrupLaserAction(GameObject dummyLaserHitbox)
    {
        laserPool = new ObjectPool(dummyLaserHitbox);
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
            if(Cursor.Instance.ViewPortPos.x > 0.5f)
            {
                Chef.Instance.HandManager.SetHandState(ChefHandState.SyrupLaserRight);
            }
            else
            {
                Chef.Instance.HandManager.SetHandState(ChefHandState.SyrupLaserLeft);
            }
            Chef.Instance.HandManager.UpdateHandPosition();
            return;
        }

        timePassed += dt;
        if (timePassed > runningTime)
        {
            dummyLaserInstance.Dispose();
            Chef.Instance.HandManager.EndHandAction();
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
        Chef.Instance.HandManager.StartHandAction();
        dummyLaserInstance = laserPool.Instantiate();
        dummyLaserInstance.transform.position = new Vector2(0, triggerPos.y);
    }
}
