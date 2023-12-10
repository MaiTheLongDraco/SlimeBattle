using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [SerializeField] private NormalSkill normalSkill;
    [SerializeField] private SlimeState slimeState;
    [SerializeField] private Transform createPos;
    private SlimeController slimeController;
    // Start is called before the first frame update
    void Start()
    {
        slimeController = GetComponent<SlimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] private bool _shouldPause;

    [SerializeField] private bool _shouldShowLog;
    [SerializeField] private float _interval = 0.1f;
    private float _lastTime;
    [SerializeField] private Text timer;
    [SerializeField] private Text interval;
    [SerializeField] private Text lastTime;
    public bool HasPastInterval()
    {
        if (Time.time > _interval + _lastTime)
        {
            _lastTime += _interval;
           
            return true;
        }
        SetText(timer, Time.time.ToString());
        SetText(interval, _interval.ToString());
        SetText(lastTime, _lastTime.ToString());
        return false;
    }
    private void SetText(Text text,string value)
	{
        text.text = value;
	}
    private void MakeNormalSkill()
	{
        if (slimeState == SlimeState.UNATTACK || slimeState == SlimeState.DEAD)
            return;
        var normal = Instantiate(normalSkill.GetSkill(), createPos.position, Quaternion.identity);
        var atkDam = slimeController.GetAttackInfo().AttackDamage;
        normal.GetComponent<Bullet>().SetDamage(atkDam);
    }
    public void StartNormal()
	{
        var hasPass = HasPastInterval();
        if (hasPass == false) return;
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
    public SkillObject skillObject;
    public void SetBulletDamage(float damage)
	{
        skillObject.GetSkillComponet<Bullet>().SetDamage(damage);
	}
    public GameObject GetSkill()
	{
        return skillObject.skill;
	}
}
[Serializable] 
public class AdvanceSkill
{
    public SkillObject skillObject;
}
[Serializable]
public class SkillObject
{
    public GameObject skill;
    public T GetSkillComponet<T>()
	{
       var component= skill.GetComponent<T>();
        return component;
	}
}
public enum SlimeState
{
    ATTACK,
    UNATTACK,
    DEAD
}