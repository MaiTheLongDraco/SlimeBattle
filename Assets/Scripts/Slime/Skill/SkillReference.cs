using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillReference : MonoBehaviour
{
    [SerializeField] private List<SkillRefGO> listSkillRef;

    public void HandleWithType(int id)
    {
        print($"run ini handle with type {id}");
        foreach (var skill in listSkillRef)
            skill.HandleSpecificSkillType(ActiveSkill, PassiveSkill, id);
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