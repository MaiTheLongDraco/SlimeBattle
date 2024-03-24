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
        foreach (var skill in listSkillTroop)
		{
            switch(id)			{
                case SkillID.MULTISHOT:
					{
						ActivateSkill(id, skill,SkillController.ActiveSkill.multiShot);
						break;
					}
				case SkillID.RING_SHOT: {
                        ActivateSkill(id, skill, SkillController.ActiveSkill.ringShot);
                        break; }
                case SkillID.RAPID_FIRE: { ActivateSkill(id, skill, SkillController.ActiveSkill.rapidFire); break; }
                case SkillID.SILVER_GENERATOR: { ActivateSkill(id, skill, SkillController.PassiveSkillRef.silverGenerator); break; }
                case SkillID.CRITICAL_CHANCE: { ActivateSkill(id, skill, SkillController.PassiveSkillRef.criticalChance); break; }
                case SkillID.DESTRUCTION_SHOT: { ActivateSkill(id, skill, SkillController.ActiveSkill.destructionShot); break; }
                case SkillID.RANGE: { ActivateSkill(id, skill, SkillController.PassiveSkillRef.range); break; }
                case SkillID.SLOW_ZONE: { ActivateSkill(id, skill, SkillController.ActiveSkill.slowZone); break; }
                case SkillID.HEALTH: { ActivateSkill(id, skill, SkillController.PassiveSkillRef.healthPSSkill); break; }
			}
		}
    }

	private void ActivateSkill(SkillID id, SkillTroop skill,ISkillInvokation skillToActive)
	{
		skill.SetSkillInvoke(skillToActive);
		skill.ActivateSkillWithID(id);
        if (activatedSkill.Contains(skillToActive)) return;
        activatedSkill.Add(skillToActive);
        print($" add this {skillToActive.GetType().ToString()} to list activated skill");
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