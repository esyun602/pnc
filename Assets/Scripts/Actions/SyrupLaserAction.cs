using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupLaserAction : IAction
{
    private const float runningTime = 1.25f;
    private const float handRestoreTime = 0.75f;
    private const float castingTime = 0.5f;
    private float timePassed = 0;
    private bool isActive = false;

    private Vector2 lastUpdatePos;

    private System.Action endCallback;

    private GameObject leftDangerZone;
    private GameObject rightDangerZone;

    public SyrupLaserAction(GameObject LaserDangerLeft, GameObject LaserDangerRight)
    {
        leftDangerZone = Object.Instantiate(LaserDangerLeft);
        rightDangerZone = Object.Instantiate(LaserDangerRight);
        leftDangerZone.gameObject.SetActive(false);
        rightDangerZone.gameObject.SetActive(false);
    }

    void IAction.Trigger(System.Action endCallback)
    {
        timePassed = 0;
        isActive = true;
        if(Cursor.Instance.ViewPortPos.x < 0.5)
        {
            leftDangerZone.SetActive(true);
            leftDangerZone.transform.position = new Vector2(0, lastUpdatePos.y);
        }
        else
        {
            rightDangerZone.SetActive(true);
            rightDangerZone.transform.position = new Vector2(0, lastUpdatePos.y);
        }
        this.endCallback = endCallback;
    }

    void IAction.UpdateFrame(float dt)
    {
        if (!isActive)
        {
            UpdateHandState();
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
            UpdateHandState();
        }
        else if (timePassed > handRestoreTime)
        {
            UpdateHandState();
        }
        else if(timePassed > castingTime && timePassed - dt < castingTime)
        {
            InstantiateLaser();
        }
    }

    private void UpdateHandState()
    {
        if (Cursor.Instance.ViewPortPos.x > 0.5f)
        {
            Chef.Instance.HandManager.SetHandState(ChefHandState.SyrupLaserRight);
        }
        else
        {
            Chef.Instance.HandManager.SetHandState(ChefHandState.SyrupLaserLeft);
        }
        lastUpdatePos = Chef.Instance.HandManager.UpdateHandPosition();
    }

    private void InstantiateLaser()
    {
        leftDangerZone.SetActive(false);
        rightDangerZone.SetActive(false);
        Chef.Instance.HandManager.StartHandAction();
    }
}
