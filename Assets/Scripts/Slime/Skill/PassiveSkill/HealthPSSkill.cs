using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPSSkill : MonoBehaviour,ISkillInvokation
{
	[SerializeField] private SkillState state;
	[SerializeField] private DisplaySubBtnInfo defenseInfo;
	[SerializeField] private float increaseValue;
	public SkillState SkillState { get => state; set => state = value; }

	public bool CanTriggerSkill()
	{
		if (state != SkillState.UNLOCKED) return false;
		return true;
	}

	public void DoSkill()
	{
		if(CanTriggerSkill())
		{
			defenseInfo.UpdateValueWithId0(increaseValue);
			SetState(SkillState.NOT_UNLOCK);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		increaseValue = SlimeController.Instance.SlimeDF.Heath*0.3f;
		SetState(SkillState.NOT_UNLOCK);

	}
	public void SetState(SkillState set)
	{
		state = set;
	}

	public void UpgradeSkill()
	{
		print($"====== upgrading {name} skill =====");
	}
}
