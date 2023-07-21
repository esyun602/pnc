using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    //TODO: pooling
    [SerializeField]
    private GameObject dummyHitBoxPrefab;
    [SerializeField]
    private GameObject dummyDropPrefab;
    [SerializeField]
    private GameObject dummyLaserPrefab;

    private ActionHandler actionHandler;

    private void Start()
    {
        actionHandler = new ActionHandler(
        new List<IAction>()
        {
            new DummyAction(dummyHitBoxPrefab),
            new SyrupDropAction(dummyDropPrefab),
            new SyrupLaserAction(dummyLaserPrefab),
        });
    }

    void Update()
    {
        ProcessChangeAction();
        ProcessTriggerAction();
        actionHandler.UpdateFrame(Time.deltaTime);
    }

    private void ProcessChangeAction()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            actionHandler.MoveToPrevAction();
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            actionHandler.MoveToNextAction();
        }
    }

    private void ProcessTriggerAction()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            actionHandler.TriggerCurrentAction();
        }
    }


}
