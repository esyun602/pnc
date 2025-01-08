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
    private int phase = 1;
    public int Phase => phase;
    private IChefAnimationController animationController;

    private ActionHandler actionHandler;
    public ActionHandler ActionHandler => actionHandler;
    [SerializeField]
    private HandManager handManager;
    public HandManager HandManager => handManager;

    [SerializeField] private ChefData chefData;
    
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
        animationController = GetComponent<IChefAnimationController>();
        animationController.Init();

        actionHandler = chefData.CreateActionHandler();
        InitializePhase();
    }

    void Update()
    {
        animationController.UpdateAnimation();
        if (!GameManager.Instance.IsInGame)
        {
            return;
        }

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

    public void SetNextPhase()
    {
        animationController.SetPhase(++phase);
    }
    
    private void InitializePhase()
    {
        phase = 1;
    }
    
    public void SetLaugh()
    {
        animationController.SetLaugh();
    }
}
