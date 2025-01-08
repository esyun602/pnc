using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SyrupDropAction : ActionBase
{
    #region serializefield

    [SerializeField] private float runningTime;
    [SerializeField] private float handRestoreTime;
    [SerializeField] private float castingTime;
    [SerializeField] private float dropHeight;
    [SerializeField] private GameObject dummyDropPrefab;
    [SerializeField] private GameObject syrupPrefab;
    
    #endregion
    private float timePassed = 0;
    private bool isActive = false;
    private ObjectPool dummyDropPool;
    private Vector3 currentHandPos;

    private System.Action endCallback;
    

    public override void Init()
    {
        dummyDropPool = new ObjectPool(dummyDropPrefab, 80);
        DummyDrop.SetSyrupPool(new ObjectPool(syrupPrefab, 80));
    }
    protected override void Trigger(System.Action endCallback)
    {
        timePassed = 0;
        isActive = true;

        Chef.Instance.HandManager.StartHandAction();

        this.endCallback = endCallback;
    }

    protected override void UpdateFrame(float dt)
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
