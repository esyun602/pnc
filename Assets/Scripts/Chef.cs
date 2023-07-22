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
    private Transform head;
    [SerializeField]
    private Transform body;
    private float bodyMoveAccel = -1f;
    private float headMoveAccel = -1f;
    private const float roundTime = 2f;
    private float initHeadVelocity => -headMoveAccel * roundTime / 2;
    private float initBodyVelocity => -bodyMoveAccel * roundTime / 2;
    private float currentHeadVelocity;
    private float currentBodyVelocity;
    private float headTimePassed = -roundTime/4;
    private float bodyTimePassed = 0;

    [SerializeField]
    private GameObject dummyHitBoxPrefab;
    [SerializeField]
    private GameObject dummyDropPrefab;
    [SerializeField]
    private GameObject dummyLaserPrefab;
    [SerializeField]

    private GameObject dummyForkPrefab;
    [SerializeField]
    private GameObject dummyRedPrefab;

    private GameObject dummySyrupPrefab;


    private ActionHandler actionHandler;
    public ActionHandler ActionHandler => actionHandler;
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
            new ForkAction(dummyForkPrefab, dummyRedPrefab),
            new SyrupDropAction(dummyDropPrefab, dummySyrupPrefab),
            new SyrupLaserAction(),
        });
        currentHeadVelocity = initHeadVelocity;
        currentBodyVelocity = initBodyVelocity;
    }

    void Update()
    {
        ProcessChangeAction();
        ProcessTriggerAction();
        actionHandler.UpdateFrame(Time.deltaTime);

        UpdateAnimation();
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

    private void UpdateAnimation()
    {
        UpdateBody();
        UpdateHead();
    }

    private void UpdateBody()
    {
        bodyTimePassed += Time.deltaTime;
        if (bodyTimePassed > roundTime)
        {
            bodyTimePassed = 0;
            bodyMoveAccel *= -1;
            currentBodyVelocity = initBodyVelocity;
            body.transform.position = Vector3.zero;
        }
        else
        {
            currentBodyVelocity += bodyMoveAccel * Time.deltaTime;
            body.transform.position += Vector3.up * (currentBodyVelocity * Time.deltaTime);
        }
    }

    private void UpdateHead()
    {
        headTimePassed += Time.deltaTime;
        if(headTimePassed < 0)
        {
            return;
        }

        if (headTimePassed > roundTime)
        {
            headTimePassed = 0;
            headMoveAccel *= -1;
            currentHeadVelocity = initHeadVelocity;
            head.transform.position = Vector3.zero;
        }
        else
        {
            currentHeadVelocity += headMoveAccel * Time.deltaTime;
            head.transform.position += Vector3.up * (currentHeadVelocity * Time.deltaTime);
        }
    }

    public void SetChefHandState(ChefHandState state)
    {
        handManager.SetHandState(state);
    }

}
