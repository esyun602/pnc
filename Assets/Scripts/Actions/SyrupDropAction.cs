using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupDropAction : IAction
{
    private float runningTime = 5f;
    private float timePassed = 0;
    private bool isActive = false;
    private ObjectPool dummyDropPool;
    private Vector2 startPos;

    private System.Action endCallback;

    public SyrupDropAction(GameObject dummyDrop)
    {
        dummyDropPool = new ObjectPool(dummyDrop, 80);
    }
    void IAction.Trigger(System.Action endCallback)
    {
        timePassed = 0;
        isActive = true;
        startPos = Cursor.Instance.WorldPos;
        Chef.Instance.HandManager.StartHandAction();

        this.endCallback = endCallback;
    }

    void IAction.UpdateFrame(float dt)
    {
        Chef.Instance.HandManager.SetHandState(ChefHandState.SyrupDrop);
        Chef.Instance.HandManager.UpdateHandPosition();
        if (!isActive)
        {
            return;
        }

        timePassed += dt;
        if (timePassed > runningTime)
        {
            Chef.Instance.HandManager.EndHandAction();
            endCallback?.Invoke();
            isActive = false;
        }
        else
        {
            startPos = new Vector2(Cursor.Instance.WorldPos.x, startPos.y);
            var newTransfrom = dummyDropPool.Instantiate().transform;
            newTransfrom.position = startPos;
        }
    }
}