using System;
using UnityEngine;

public abstract class ActionBase : ScriptableObject, IAction
{
	public abstract void Init();
	void IAction.Trigger(Action endCallback)
	{
		Trigger(endCallback);
	}

	protected abstract void Trigger(Action endCallback);

	void IAction.UpdateFrame(float dt)
	{
		UpdateFrame(dt);
	}

	protected abstract void UpdateFrame(float dt);
}