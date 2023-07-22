using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIconHandler : MonoBehaviour
{
    private ChefHandState currentState;
    [SerializeField]
    private GameObject forkEnabled;
    [SerializeField]
    private GameObject forkDisabled;
    [SerializeField]
    private GameObject dropEnabled;
    [SerializeField]
    private GameObject dropDisabled;
    [SerializeField]
    private GameObject laserEnabled;
    [SerializeField]
    private GameObject laserDisabled;

    public void SetSkillIconByHandState(ChefHandState chefHandState)
    {
        switch (chefHandState)
        {
            case ChefHandState.Fork:
                SetForkState(true);
                break;
            case ChefHandState.SyrupDrop:
                SetDropState(true);
                break;
            case ChefHandState.SyrupLaserLeft:
            case ChefHandState.SyrupLaserRight:
                SetLaserState(true);
                break;
            default:
                break;
        }
    }

    private void SetForkState(bool activeState)
    {
        InitializeState();
        forkEnabled.SetActive(activeState);
        forkDisabled.SetActive(!activeState);
    }
    private void SetDropState(bool activeState)
    {
        InitializeState();
        dropEnabled.SetActive(activeState);
        dropDisabled.SetActive(!activeState);
    }
    private void SetLaserState(bool activeState)
    {
        InitializeState();
        laserEnabled.SetActive(activeState);
        laserDisabled.SetActive(!activeState);
    }

    private void InitializeState()
    {
        forkEnabled.SetActive(false);
        forkDisabled.SetActive(true);
        dropEnabled.SetActive(false);
        dropDisabled.SetActive(true);
        laserEnabled.SetActive(false);
        laserDisabled.SetActive(true);
    }

    private void Update()
    {
        if(currentState != Chef.Instance.HandManager.CurrentState)
        {
            currentState = Chef.Instance.HandManager.CurrentState;
            SetSkillIconByHandState(Chef.Instance.HandManager.CurrentState);

        }
    }
}
