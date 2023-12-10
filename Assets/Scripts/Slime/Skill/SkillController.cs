using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private NormalSkill normalSkill;
    [SerializeField] private float spawnNormalInterval;
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
    private IEnumerator MakeNormalSkill()
	{
        if (slimeState == SlimeState.UNATTACK || slimeState == SlimeState.DEAD)
            yield break;
        var normal = Instantiate(normalSkill.GetSkill(), createPos.position, Quaternion.identity);
        var atkDam = slimeController.GetAttackInfo().AttackDamage;
        normal.GetComponent<Bullet>().SetDamage(atkDam);
        yield return new WaitForSeconds(spawnNormalInterval);
	}
    public void StartNormal()
	{
        StartCoroutine(MakeNormalSkill());
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