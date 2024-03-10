using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [SerializeField] private NormalSkill normalSkill;
    [SerializeField] private SlimeState slimeState;
    [SerializeField] private Transform createPos;
    [SerializeField] private bool _shouldPause;
    [SerializeField] private float normalInterval = 3f;
    [SerializeField] private bool _shouldShowLog;
    [SerializeField] private float _interval = 0.1f;
    [SerializeField] private float _interval1 = 0.1f;
    [SerializeField] private Text timer;
    [SerializeField] private Text interval;
    [SerializeField] private Text lastTime;
    private float _lastTime;
    private float _lastTime1;
    [SerializeField] private SkillReference skillReference;
    private SlimeController slimeController;
    [SerializeField] private UnityEvent onPassOneSecond;
    private ISkillInvokation skillInvokation;
    private ICritical criticalSkill;
    [SerializeField] private ActiveSkillRef activeSkill;
    [SerializeField] private PassiveSkillRef passiveSkillRef;
    public static SkillController Instance;
	public SlimeState SlimeState { get => slimeState; set => slimeState = value; }
	public ActiveSkillRef ActiveSkill { get => activeSkill; set => activeSkill = value; }
	public PassiveSkillRef PassiveSkillRef { get => passiveSkillRef; set => passiveSkillRef = value; }

	// Start is called before the first frame update
	private void Start()
    {
        slimeController = GetComponent<SlimeController>();
        Instance = this;
    }
    // Update is called once per frame
    private void Update()
    {
        IvokeOnPassASecond();
    }
	private void OnDestroy()
	{
        Instance = null;
	}
	public void AddOnPassOoneSecondListener(UnityAction callBack)
	{
        onPassOneSecond.AddListener(callBack);
	}
	private void OnDisable()
	{
        onPassOneSecond.RemoveAllListeners();
	}
	public bool HasPastInterval()
    {
        if (Time.time > _interval + _lastTime)
        {
            _lastTime += _interval;
            //onPassOneSecond?.Invoke();
            return true;
        }

        SetText(timer, Time.time.ToString());
        SetText(interval, _interval.ToString());
        SetText(lastTime, _lastTime.ToString());
        return false;
    }
    private void IvokeOnPassASecond()
	{
        if (Time.time > _interval1 + _lastTime1)
        {
            _lastTime1 += _interval1;
            onPassOneSecond?.Invoke();
            ActiveSkill.InvokeSkillData(DoSkillThroughInterface, ActiveSkill.slowZone);
            ActiveSkill.InvokeSkillData(DoSkillThroughInterface, ActiveSkill.ringShot);
            ActiveSkill.InvokeSkillData(DoSkillThroughInterface, ActiveSkill.multiShot);
            ActiveSkill.InvokeSkillData(DoSkillThroughInterface, ActiveSkill.rapidFire);
            ActiveSkill.InvokeSkillData(DoSkillThroughInterface, ActiveSkill.destructionShot);
            PassiveSkillRef.InvokeSkillData(DoSkillThroughInterface, PassiveSkillRef.silverGenerator);
            PassiveSkillRef.InvokeSkillData(DoSkillThroughInterface, PassiveSkillRef.healthPSSkill);
            PassiveSkillRef.InvokeSkillData(DoSkillThroughInterface, PassiveSkillRef.range);
        }
    }
    private void DoSkillThroughInterface(ISkillInvokation skillInvokationNew)
	{
        if (skillInvokationNew == null) return;
        skillInvokation = skillInvokationNew;
        skillInvokation.DoSkill();
       
    }
    public void TriggerCriticalState(float set)
	{
        DoCriticalThroughInterface(ActiveSkill.rapidFire, set);
        DoCriticalThroughInterface(ActiveSkill.multiShot, set);

    }
    private void DoCriticalThroughInterface(ICritical criticalSkillSet,float critValue)
    {
        if (criticalSkill == null) return;
        criticalSkill = criticalSkillSet;
        criticalSkill.TriggerCritical(critValue);

    }
    private void SetText(Text text, string value)
    {
        text.text = value;
    }
    public void SetDataForMultiShot(List<EnemyMini> set)
	{
        ActiveSkill.multiShot.SetListEnemy(set);
	}
    public void SetCurrentEnemyForDestrucionShot(EnemyMini enemy)
	{
        ActiveSkill.destructionShot.SetCurrentEnemy(enemy);
	}
    private void MakeNormalSkill(EnemyMini enemy)
    {
        if (SlimeState == SlimeState.UNATTACK || SlimeState == SlimeState.DEAD)
            return;
        var atkDam = slimeController.GetAttackInfo().AttackDamage;
        var bulletSkill = normalSkill.GetSkillAt(0);
        CreateNormalSkill(bulletSkill, createPos.position, Quaternion.identity, atkDam,enemy);
        normalInterval -= Time.deltaTime;
        if (normalInterval <= 0)
        {
            var specialSkill = normalSkill.GetSkillAt(1);
            CreateNormalSkill(specialSkill, createPos.position, Quaternion.identity, atkDam, enemy);
            normalInterval = 3; 
        }
    }

    private void CreateNormalSkill(SkillObject skillObject, Vector3 position, Quaternion rotation, float atkDam, EnemyMini enemy)
    {
        var normal = Instantiate(normalSkill.GetSkill(skillObject), position, rotation);
        normal.GetComponent<Bullet>().SetDamage(atkDam);
        normal.GetComponent<Bullet>().SetCurrentEnemy(enemy);
        //normal.transform.Translate((slimeController.GetCurrentEnemy().GetSelfPos() - transform.position) * 0.5f * Time.deltaTime);
        skillReference.InvokeOnShooting();
        print($" skilltype {normal.GetComponent<Bullet>().GetType()}--- damage {atkDam}");
    }

    public void StartNormal(EnemyMini enemy)
    {
        if (!enemy) return;
        MakeNormalSkill(enemy);
        SetCurrentEnemyForDestrucionShot(enemy);
    }

    public void SetState(SlimeState newState)
    {
        SlimeState = newState;
    }
}

[Serializable]
public class NormalSkill
{
    public List<SkillObject> skillObject;

    public void SetBulletDamage(SkillObject skillObject, float damage)
    {
        skillObject.GetSkillComponet<Bullet>().SetDamage(damage);
    }

    public GameObject GetSkill(SkillObject skillObject)
    {
        return skillObject.skill;
    }

    public SkillObject GetSkillAt(int index)
    {
        return skillObject[index];
    }
}

[Serializable]
public class AdvanceSkill
{
    public SkillObject skillObject;
    public void GenerateSkillWithDefaultNumber(UnityAction<int> callBack)
	{
        skillObject.CreateSkill(callBack);
	}
}
[Serializable]
public class ActiveSkillRef
{
    public RingShot ringShot;
    public MultiShot multiShot;
    public RapidFire rapidFire;
    public DestructionShot destructionShot;
    public SlowZone slowZone;
    public void InvokeSkillData(UnityAction<ISkillInvokation> setSkillMethod, ISkillInvokation skill)
	{
        setSkillMethod?.Invoke(skill);
	}
}
[Serializable]
public class PassiveSkillRef
{
    public CriticalChance criticalChance;
    public RangePSSkill range;
    public SilverGenerator silverGenerator;
    public HealthPSSkill healthPSSkill;
    public void InvokeSkillData(UnityAction<ISkillInvokation> setSkillMethod, ISkillInvokation skill)
    {
        setSkillMethod?.Invoke(skill);
    }
}

[Serializable]
public class SkillObject
{
    public GameObject skill;
    public int numberOfSkill;
    public void CreateSkill(UnityAction<int> callBack)
	{
        callBack?.Invoke(numberOfSkill);
	}

    public T GetSkillComponet<T>()
    {
        var component = skill.GetComponent<T>();
        return component;
    }
}

public enum SlimeState
{
    ATTACK,
    UNATTACK,
    DEAD
}
public enum SkillState
{
    UNLOCKED,
    NOT_UNLOCK
}