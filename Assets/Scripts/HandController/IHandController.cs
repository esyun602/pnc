using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandController
{
    public void StartAction();
    public void EndAction();
    public Vector3 UpdatePosition();
    public void SetActive(bool activeState);
}
