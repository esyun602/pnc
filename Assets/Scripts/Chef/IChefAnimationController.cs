using UnityEngine;

public interface IChefAnimationController
{
	public void Init();
	public void UpdateAnimation();
	public void SetPhase(int phase);
	public void SetLaugh();
}
