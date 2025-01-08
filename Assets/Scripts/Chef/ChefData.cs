using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChefData : ScriptableObject
{
	[SerializeField] private List<ActionBase> actionList;

	public ActionHandler CreateActionHandler()
	{
		return new ActionHandler(actionList);
	}
}