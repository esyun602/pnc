using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChefHandState
{
    None = 0,
    SyrupDrop = 1,
    SyrupLaserLeft = 2,
    SyrupLaserRight = 3,
    Fork = 4,
}

public class Chef : MonoBehaviour
{
    [SerializeField]
    private GameObject dummyHitBoxPrefab;
    [SerializeField]
    private GameObject dummyDropPrefab;
    [SerializeField]
    private GameObject dummyLaserPrefab;
    [SerializeField]
    private Object dummyForkPrefab;
    [SerializeField]
    private Object dummyRedPrefab;

    private ActionHandler actionHandler;
    [SerializeField]
    private HandManager handManager;
    public HandManager HandManager => handManager;
    
    public static Chef Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        actionHandler = new ActionHandler(
        new List<IAction>()
        {
            new DummyAction(dummyHitBoxPrefab),
            new SyrupDropAction(dummyDropPrefab),
            new SyrupLaserAction(dummyLaserPrefab),
            new ForkAction(dummyForkPrefab, dummyRedPrefab),
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

    public void SetChefHandState(ChefHandState state)
    {
        handManager.SetHandState(state);
    }

}
