using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillInvokation
{
	public void SetState(SkillState set);
	public SkillState SkillState { get; set; }
	public void DoSkill();
	public bool CanTriggerSkill();
}
