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
    [SerializeField] private List<ActiveSkill> skilUnlock;
    private SlimeController slimeController;
    [SerializeField] private UnityEvent onPassOneSecond;
    // Start is called before the first frame update
    private void Start()
    {
        slimeController = GetComponent<SlimeController>();
    }
    public void UnLockSkillWithID(int id)
	{
        foreach(var skill in skilUnlock)
		{
            if(id==(int)skill.SkillID)
			{
                skill.ActivateSkill();
                print($"---unlock skill {skill.SkillID} is {skill.SkillState}");
			}
		}
	}

    // Update is called once per frame
    private void Update()
    {
        IvokeOnPassASecond();
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
        }
    }

    private void SetText(Text text, string value)
    {
        text.text = value;
    }

    private void MakeNormalSkill()
    {
        if (slimeState == SlimeState.UNATTACK || slimeState == SlimeState.DEAD)
            return;
        var atkDam = slimeController.GetAttackInfo().AttackDamage;
        var bulletSkill = normalSkill.GetSkillAt(0);
        CreateNormalSkill(bulletSkill, createPos.position, Quaternion.identity, atkDam);
        normalInterval -= Time.deltaTime;
        if (normalInterval <= 0)
        {
            var specialSkill = normalSkill.GetSkillAt(1);
            CreateNormalSkill(specialSkill, createPos.position, Quaternion.identity, atkDam);
            normalInterval = 3;
        }
    }

    private void CreateNormalSkill(SkillObject skillObject, Vector3 position, Quaternion rotation, float atkDam)
    {
        var normal = Instantiate(normalSkill.GetSkill(skillObject), position, rotation);
        normal.GetComponent<Bullet>().SetDamage(atkDam);
        skillReference.InvokeOnShooting();
        print($" skilltype {normal.GetComponent<Bullet>().GetType()}--- damage {atkDam}");
    }

    public void StartNormal()
    {
        MakeNormalSkill();
    }

    public void SetState(SlimeState newState)
    {
        slimeState = newState;
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