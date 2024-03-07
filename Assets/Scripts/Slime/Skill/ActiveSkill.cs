using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ActiveSkill : MonoBehaviour,ISkillInvokation
{
	[SerializeField] private SkillReference skillReference;
    [SerializeField] private float _interval = 0.1f;
	[SerializeField] private SkillID skillID;
	[SerializeField] private SkillState skillState;
	[SerializeField] private AdvanceSkill advanceSkill;
	private float _lastTime;

	public SkillID SkillID { get => skillID; set => skillID = value; }
	public SkillState SkillState { get => skillState; set => skillState = value; }

	private void Awake()
	{
		skillReference = SkillReference.Instance;
	}
	public void ActivateSkill()
	{
		SkillState = SkillState.UNLOCKED;
	}
	[ContextMenu("DoSkill")]
	private void DoSkill()
	{
		advanceSkill.GenerateSkillWithDefaultNumber(DoSkill);
	}
	public bool CanTriggerSkill()
	{
		if (SkillState == SkillState.UNLOCKED) return true;
		return false;
	}

	public void DoSkill(int numberOfSkil)
	{
		if (!CanTriggerSkill()) return;
		HandleTriggerSkillFuntion(numberOfSkil);
	}
	private void CreateSkillIntance(int number)
	{
		if (SlimeController.Instance == null) return;
		for(int i=0; i<number; i++)
		{
			Instantiate(this.gameObject, SlimeController.Instance.transform.position, Quaternion.identity);
		}
	}
	private void HandleTriggerSkillFuntion(int number)
	{
		switch (SkillID)
		{
			case SkillID.SILVER_GENERATOR: {
					if (Time.time > _interval + _lastTime)
					{
						_lastTime += _interval;
					print("Automatic earns 20 silver each second in battle , afftected by other bonus effects");
					}
					break; }
			case SkillID.MULTISHOT:
				{
					CreateSkillIntance(number);
					print("Multishot for 2 target. sub-projectile deal damage by 30% damage");
					break;
				}
			case SkillID.RING_SHOT:
				{
					var triggerTime = 19;
					if(skillReference.ShootingNumber%triggerTime==0)
					{
						CreateSkillIntance(number);
						print(" Shooting every 19 times will spread 13 project tile around");
					}
					break;
				}
			case SkillID.SLOW_ZONE: {
					print(" Slow enemies in attack range. Deacrease 50% movespeed of enemy");
					break; }
			case SkillID.DESTRUCTION_SHOT:
				{
					print(" Fire one big energy shot a boss dropping its 10% maximum heath");
					break;
				}
			case SkillID.HEALTH:
				{
					print("Increase 30% hp");
					break;
				}
		}
	}

	void ISkillInvokation.DoSkill()
	{
		throw new NotImplementedException();
	}
}
