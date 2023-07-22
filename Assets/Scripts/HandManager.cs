using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField]
    private LaserHandController leftHandWithSyrupLaser;
    [SerializeField]
    private LaserHandController rightHandWithSyrupLaser;
    [SerializeField]
    private DropHandController dropHand;
    [SerializeField]
    private ForkHandController leftHandWithFork;

    private ChefHandState currentState;
    public ChefHandState CurrentState => currentState;
    public IHandController CurrentHandController => GetHandObjectByState(currentState);

    public void SetHandState(ChefHandState state)
    {
        if (currentState == state)
            return;
        GetHandObjectByState(currentState).SetActive(false);
        currentState = state;
        GetHandObjectByState(currentState).SetActive(true);
    }
    
    public Vector3 UpdateHandPosition()
    {
        return GetHandObjectByState(currentState).UpdatePosition();
    }

    public void StartHandAction()
    {
        GetHandObjectByState(currentState).StartAction();
    }

    public void EndHandAction()
    {
        GetHandObjectByState(currentState).EndAction();
    }

    private IHandController GetHandObjectByState(ChefHandState state)
    {
        switch (state)
        {
            case ChefHandState.None:
                return NullHandController.Instance;
            case ChefHandState.SyrupLaserLeft:
                return leftHandWithSyrupLaser;
            case ChefHandState.SyrupLaserRight:
                return rightHandWithSyrupLaser;
            case ChefHandState.SyrupDrop:
                return dropHand;
            case ChefHandState.Fork:
                return leftHandWithFork;
            default:
                return NullHandController.Instance;
        }
    }
}
