using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChefAction : uint
{
    None = 0,
    ChangeAction = 1 << 1,
    TriggerAction = 1 << 2,
}

public class ActionHandler
{
    private List<ActionBase> actionList;
    private int curIdx;

    public IAction CurrentAction => actionList[curIdx];
    private IAction currentTriggeredAction = null;

    private ChefAction blockChiefAction = ChefAction.None;
    private bool IsChangeBlocked => (blockChiefAction & ChefAction.ChangeAction) == ChefAction.ChangeAction;
    private bool IsTriggerBlocked => (blockChiefAction & ChefAction.TriggerAction) == ChefAction.TriggerAction;
    public bool IsActionChanged => currentTriggeredAction != CurrentAction;

    public ActionHandler(List<ActionBase> actionList)
    {
        this.actionList = actionList;
        curIdx = 0;
        foreach (var action in actionList)
        {
            action.Init();
        }
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

        curIdx = (curIdx + actionList.Count - 1) % actionList.Count;
    }

    public void TriggerCurrentAction()
    {
        if (IsTriggerBlocked)
        {
            return;
        }

        BlockTriggerAction();
        currentTriggeredAction = CurrentAction;
        CurrentAction.Trigger(EndActionCallback);
    }

    //TODO: TBD
    public void UpdateFrame(float dt)
    {
        (currentTriggeredAction ?? CurrentAction).UpdateFrame(dt);
    }

    public void BlockTriggerAction()
    {
        blockChiefAction |= ChefAction.TriggerAction;
    }

    private void EndActionCallback()
    {
        currentTriggeredAction = null;
        RestoreTriggerAction();
    }

    public void RestoreTriggerAction()
    {
        blockChiefAction &= (~ChefAction.TriggerAction);
    }
}
