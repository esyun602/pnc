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

    private ChefHandState currentState;
    public IHandController CurrentHandController => GetHandObjectByState(currentState);

    public void SetHandState(ChefHandState state)
    {
        if (currentState == state)
            return;
        GetHandObjectByState(currentState).SetActive(false);
        currentState = state;
        GetHandObjectByState(currentState).SetActive(true);
    }
    
    public void UpdateHandPosition()
    {
        GetHandObjectByState(currentState).UpdatePosition();
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
            default:
                return NullHandController.Instance;
        }
    }
}