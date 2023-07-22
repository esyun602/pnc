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
    [SerializeField]
    private Transform headPhase2;
    [SerializeField]
    private Transform headPhase3;

    private Transform currentHead;
    private int phase = 1;

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
    private GameObject dummyLaserLeftDangerPrefab;
    [SerializeField]
    private GameObject dummyLaserRightDangerPrefab;
    [SerializeField]
    private GameObject dummyForkPrefab;
    [SerializeField]
    private GameObject dummyRedPrefab;
    [SerializeField]
    private GameObject dummySyrupPrefab;

    [SerializeField]
    private SkillIconHandler iconHandler;
    public SkillIconHandler IconHandler => iconHandler;

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
        bodyMoveAccel = -1f;
        headMoveAccel = -1f;
        headTimePassed = -roundTime / 4;
        bodyTimePassed = 0;

        actionHandler = new ActionHandler(
        new List<IAction>()
        {
            new ForkAction(dummyForkPrefab, dummyRedPrefab),
            new SyrupDropAction(dummyDropPrefab, dummySyrupPrefab),
            new SyrupLaserAction(dummyLaserLeftDangerPrefab, dummyLaserRightDangerPrefab),
        });
        currentHeadVelocity = initHeadVelocity;
        currentBodyVelocity = initBodyVelocity;
        currentHead = head;
        InitializePhase();
    }

    void Update()
    {
        UpdateAnimation();
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
            currentHead.transform.position = Vector3.zero;
        }
        else
        {
            currentHeadVelocity += headMoveAccel * Time.deltaTime;
            currentHead.transform.position += Vector3.up * (currentHeadVelocity * Time.deltaTime);
        }
    }

    public void SetChefHandState(ChefHandState state)
    {
        handManager.SetHandState(state);
    }

    public void SetNextPhase()
    {
        phase++;
        if(phase == 2)
        {
            currentHead.gameObject.SetActive(false);
            headPhase2.position = currentHead.position;
            currentHead = headPhase2;
            currentHead.gameObject.SetActive(true);
        }
        else if(phase == 3)
        {
            currentHead.gameObject.SetActive(false);
            headPhase3.position = currentHead.position;
            currentHead = headPhase3;
            currentHead.gameObject.SetActive(true);
        }
    }

    private void InitializePhase()
    {
        phase = 1;
        head.gameObject.SetActive(true);
        head.position = Vector3.zero;
        headPhase2.gameObject.SetActive(false);
        headPhase3.gameObject.SetActive(false);
    }

}
