using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChiefAction : uint
{
    None = 0,
    ChangeAction = 1 << 1,
    TriggerAction = 1 << 2,
}

public class ActionHandler
{
    private List<IAction> actionList;
    private int curIdx;

    public IAction CurrentAction => actionList[curIdx];

    private ChiefAction blockChiefAction = ChiefAction.None;
    private bool IsChangeBlocked => (blockChiefAction & ChiefAction.ChangeAction) == ChiefAction.ChangeAction;
    private bool IsTriggerBlocked => (blockChiefAction & ChiefAction.TriggerAction) == ChiefAction.TriggerAction;

    public ActionHandler(List<IAction> actionList)
    {
        this.actionList = actionList;
        curIdx = 0;
    }

    public void MoveToNextAction()
    {
        if (IsChangeBlocked)
        {
            return;
        }

        curIdx = (curIdx + 1) % actionList.Count;
    }

    public void MoveToPrevAction()
    {
        if (IsChangeBlocked)
        {
            return;
        }

        curIdx = (curIdx - 1) % actionList.Count;
    }

    public void TriggerCurrentAction()
    {
        if (IsTriggerBlocked)
        {
            return;
        }

        BlockTriggerAction();
        CurrentAction.Trigger(RestoreTriggerAction);
    }

    //TODO: TBD
    public void UpdateFrame(float dt)
    {
        CurrentAction.UpdateFrame(dt);
    }

    public void BlockTriggerAction()
    {
        blockChiefAction |= ChiefAction.TriggerAction;
    }

    public void RestoreTriggerAction()
    {
        blockChiefAction &= (~ChiefAction.TriggerAction);
    }
}
