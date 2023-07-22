using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullHandController : IHandController
{
    public static NullHandController Instance { get; } = new NullHandController();
    void IHandController.SetActive(bool activeState)
    {

    }
    Vector3 IHandController.UpdatePosition()
    {
        return Vector3.zero;
    }
    void IHandController.StartAction()
    {

    }
    void IHandController.EndAction()
    {

    }
}
