using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillReference : MonoBehaviour
{
    [SerializeField] private List<SkillRefGO> listSkillRef;
    [SerializeField] private List<SkillTroop> listSkillTroop;
    [SerializeField] private List<ISkillInvokation> activatedSkill;
    [SerializeField] private int passTime;
    [SerializeField] private int defeatNumber;
    [SerializeField] private int shootingNumber;
    [SerializeField] private int activeTroopTime;
    [SerializeField] private UnityEvent onDefeatEnemy;
    [SerializeField] private UnityEvent onShooting;
    [SerializeField] private UnityEvent onTriggerActiveTroopTime;
    [SerializeField] private SkillController skillController; 
    public static SkillReference Instance;

	public int PassTime { get => passTime; set => passTime = value; }
	public int DefeatNumber { get => defeatNumber; set => defeatNumber = value; }
	public int ShootingNumber { get => shootingNumber; set => shootingNumber = value; }
	public SkillController SkillController { get {
			if (skillController == null)
			{
                skillController = SlimeController.Instance.SkillController;
            }
            return skillController;
                } set => skillController = value; }

	public void HandleWithType(int id)
    {
        print($"run ini handle with type {id}");
        foreach (var skill in listSkillRef)
            skill.HandleSpecificSkillType(ActiveSkill, PassiveSkill, id);
    }
    public void HandleWithTypeSkillTroop(SkillID id)
    {
        print($"run ini handle with type {id}");
            switch(id)			{
                case SkillID.MULTISHOT:
					{
						TriggerActivate(id,SkillController.ActiveSkill.multiShot);
						break;
					}
				case SkillID.RING_SHOT: {
                        TriggerActivate(id, SkillController.ActiveSkill.ringShot);
                        break; }
                case SkillID.RAPID_FIRE: { TriggerActivate(id, SkillController.ActiveSkill.rapidFire); break; }
                case SkillID.SILVER_GENERATOR: { TriggerActivate(id, SkillController.PassiveSkillRef.silverGenerator); break; }
                case SkillID.CRITICAL_CHANCE: { TriggerActivate(id, SkillController.PassiveSkillRef.criticalChance); break; }
                case SkillID.DESTRUCTION_SHOT: { TriggerActivate(id, SkillController.ActiveSkill.destructionShot); break; }
                case SkillID.RANGE: { TriggerActivate(id, SkillController.PassiveSkillRef.range); break; }
                case SkillID.SLOW_ZONE: { TriggerActivate(id, SkillController.ActiveSkill.slowZone); break; }
                case SkillID.HEALTH: { TriggerActivate(id, SkillController.PassiveSkillRef.healthPSSkill); break; }
			}
		
    }

	private void TriggerActivate(SkillID id,ISkillInvokation skillInvokation)
	{
        if (activatedSkill.Contains(skillInvokation))
        {
            skillInvokation.UpgradeSkill();
        }
        foreach (var skill1 in listSkillTroop)
		{
			ActivateSkill(id, skill1, skillInvokation);
		}
        
    }

	private void ActivateSkill(SkillID id, SkillTroop skill,ISkillInvokation skillToActive)
	{
		skill.SetSkillInvoke(skillToActive);
		skill.ActivateSkillWithID(id);
        if (activatedSkill.Contains(skillToActive)) {
            return;
        } 
        activatedSkill.Add(skillToActive);
        print($" add this {skillToActive.GetType().ToString()} to list activated skill");
        foreach(var skillAT in activatedSkill)
		{
            print($" {skillAT.GetType()} in list activated");
		}
	}

	private void Awake()
	{
        Instance = this;
	}
    
	private void Start()
	{
        activatedSkill = new List<ISkillInvokation>();
        SkillController.AddOnPassOoneSecondListener(IncreasePassTime);
        onDefeatEnemy.AddListener(IncreaseDefeatNumber);
        onShooting.AddListener(IncreaseShootingNumber);
	}
	public void InvokeOnDefeatEnemy()
	{
        onDefeatEnemy?.Invoke();
	}
    public void InvokeOnShooting()
    {
        onShooting?.Invoke();
    }
    public void IncreasePassTime()
	{
        PassTime++;
	}
    private void IncreaseDefeatNumber()
	{
        DefeatNumber++;
        if(DefeatNumber % activeTroopTime==0)
		{
            onTriggerActiveTroopTime?.Invoke();
		}
	}
    private void IncreaseShootingNumber()
	{
        ShootingNumber++;
	}
    private void PassiveSkill()
    {
        print(" run into get passive skill");
    }

    private void ActiveSkill(SkillRefGO skill, int id)
    {
        if ((int)skill.SkillID == id)
        {
            print($" skill ID {(int)skill.SkillID}");
            Instantiate(skill.SkillGO);
        }
    }

    private void CreateSkillInstance(GameObject skillGO)
    {
        if (skillGO == null || SlimeController.Instance == null)
            return;
        Instantiate(skillGO, SlimeController.Instance.transform.position, Quaternion.identity);
    }

	
}

[Serializable]
public class SkillRefGO
{
    public Skill skillType;
    public SkillID SkillID;
    [SerializeField] private GameObject skillGO;

    public GameObject SkillGO
    {
        get => skillGO;
        set => skillGO = value;
    }

    public void HandleSpecificSkillType(UnityAction<SkillRefGO, int> activeCB, UnityAction passiveCB, int id)
    {
        switch (skillType)
        {
            case Skill.ACTIVE_SKILL:
            {
                activeCB.Invoke(this, id);
                break;
            }
            case Skill.PASSIVE_SKILL:
            {
                passiveCB.Invoke();
                break;
            }
        }
    }
}
[Serializable]
public class SkillTroop
{
    public SkillID SkillID;
    public ISkillInvokation skillInvokation;
    public void SetSkillInvoke(ISkillInvokation skill)
	{
        skillInvokation = skill;
	}
    public void ActivateSkillWithID(SkillID skillID)
	{
        if(SkillID==skillID)
		{
            skillInvokation.SetState(SkillState.UNLOCKED);
        }
	}
    public void UpgradeSkill()
	{
        skillInvokation.UpgradeSkill();
    }
}
public enum SkillID
{
    MULTISHOT = 111,
    RING_SHOT = 112,
    RAPID_FIRE = 113,
    SILVER_GENERATOR = 114,
    CRITICAL_CHANCE = 115,
    DESTRUCTION_SHOT = 116,
    HEALTH = 117,
    RANGE = 118,
    SLOW_ZONE = 119
}

public enum Skill
{
    ACTIVE_SKILL,
    PASSIVE_SKILL
}