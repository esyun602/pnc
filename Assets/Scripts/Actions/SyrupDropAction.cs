using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupDropAction : IAction
{
    private const float runningTime = 1.5f;
    private const float handRestoreTime = 1.25f;
    private const float castingTime = 0.25f;
    private float timePassed = 0;
    private bool isActive = false;
    private ObjectPool dummyDropPool;
    private const float dropHeight = 1.7f;
    private Vector3 currentHandPos;

    private System.Action endCallback;

    public SyrupDropAction(GameObject dummyDrop)
    {
        dummyDropPool = new ObjectPool(dummyDrop, 80);
    }
    void IAction.Trigger(System.Action endCallback)
    {
        timePassed = 0;
        isActive = true;

        Chef.Instance.HandManager.StartHandAction();

        this.endCallback = endCallback;
    }

    void IAction.UpdateFrame(float dt)
    {
        Chef.Instance.HandManager.SetHandState(ChefHandState.SyrupDrop);
        currentHandPos = Chef.Instance.HandManager.UpdateHandPosition();
        if (!isActive)
        {
            return;
        }

        timePassed += dt;
        if (timePassed > runningTime)
        {
            endCallback?.Invoke();
            isActive = false;
        }
        else if (timePassed > handRestoreTime && timePassed - dt < handRestoreTime)
        {
            if (Chef.Instance.ActionHandler.IsActionChanged)
            {
                Chef.Instance.HandManager.EndHandAction();
                endCallback?.Invoke();
                isActive = false;
                return;
            }

            Chef.Instance.HandManager.EndHandAction();
        }
        else if (timePassed > castingTime && timePassed < handRestoreTime)
        {
            var newTransfrom = dummyDropPool.Instantiate().transform;
            newTransfrom.position = new Vector2(currentHandPos.x, dropHeight);
        }
    }
}
