using UnityEngine;

public class DefaultChefAnimationController : MonoBehaviour, IChefAnimationController
{
	[SerializeField]
	private Transform head;
	[SerializeField]
	private Transform body;
	[SerializeField]
	private Transform headPhase2;
	[SerializeField]
	private Transform headPhase3;
	[SerializeField]
	private Transform laughHead;

	private Transform currentHead;
	private Transform currentPhaseHead;
	
	[SerializeField] private GameObject [] forkHands;
	[SerializeField] private GameObject LRforkHand;
	
	private float bodyMoveAccel = -1f;
	private float headMoveAccel = -1f;
	private const float roundTime = 2f;
	private float initHeadVelocity => -headMoveAccel * roundTime / 2;
	private float initBodyVelocity => -bodyMoveAccel * roundTime / 2;
	private float currentHeadVelocity;
	private float currentBodyVelocity;
	private float headTimePassed = -roundTime/4;
	private float bodyTimePassed = 0;
	private float laughDuration = 1f;
	private float lastLaughTime = -10f;
	private bool laughing = false;
	
	public void Init()
	{
		bodyMoveAccel = -1f;
		headMoveAccel = -1f;
		headTimePassed = -roundTime / 4;
		bodyTimePassed = 0;
		lastLaughTime = -10f;
		laughing = false;

		currentHeadVelocity = initHeadVelocity;
		currentBodyVelocity = initBodyVelocity;
		currentHead = head;
		currentPhaseHead = head;
		InitializePhase();
	}
	
	public void UpdateAnimation()
	{
		if(Time.time - lastLaughTime < laughDuration)
		{
			if(currentHead != laughHead)
			{
				currentHead.gameObject.SetActive(false);
				laughHead.gameObject.SetActive(true);

				laughHead.position = currentHead.position;
				currentHead = laughHead;
			}
		}
		else if(laughing)
		{
			laughing = false;
			var phaseHead = currentPhaseHead;
			phaseHead.position = currentHead.position;
			phaseHead.gameObject.SetActive(true);
			laughHead.gameObject.SetActive(false);

			currentHead = phaseHead;
		}
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
	
	public void SetPhase(int phase)
	{
		if(phase == 2)
		{
			currentHead.gameObject.SetActive(false);
			headPhase2.position = currentHead.position;
			currentHead = headPhase2;
			currentPhaseHead = headPhase2;
			currentHead.gameObject.SetActive(true);
		}
		else if(phase == 3)
		{
			currentHead.gameObject.SetActive(false);
			headPhase3.position = currentHead.position;
			currentHead = headPhase3;
			currentPhaseHead = headPhase3;
			currentHead.gameObject.SetActive(true);
            
			// 왼.오 포크 손만 끄고, 양손 포크 손 켜기
			forkHands[0].SetActive(false);
			forkHands[1].SetActive(false);
			LRforkHand.SetActive(true);
		}
	}
	
	private void InitializePhase()
	{
		head.gameObject.SetActive(true);
		head.position = Vector3.zero;
		headPhase2.gameObject.SetActive(false);
		headPhase3.gameObject.SetActive(false);
	}
	
	public void SetLaugh()
	{
		if(Chef.Instance.Phase >= 3)
		{
			return;
		}

		lastLaughTime = Time.time;
		laughing = true;
	}
}