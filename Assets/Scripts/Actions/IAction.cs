using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAction
{
    public void Trigger(Action endCallback);
    public void UpdateFrame(float dt);
}
