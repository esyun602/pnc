using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullHandController : IHandController
{
    public static NullHandController Instance { get; } = new NullHandController();
    void IHandController.SetActive(bool activeState)
    {

    }
    void IHandController.UpdatePosition()
    {

    }
    void IHandController.StartAction()
    {

    }
    void IHandController.EndAction()
    {

    }
}
