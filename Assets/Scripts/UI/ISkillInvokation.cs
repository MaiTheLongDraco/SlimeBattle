using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillInvokation 
{
	public void DoSkill(int number);
	public bool CanTriggerSkill();
}
