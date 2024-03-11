using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class HardSkillInfo : ICloneable
{
    public SkillType skillType;
    public float buffValue;
public Sprite skillIcon;
public SkillID id;
public string describeText;
    public string skillName;
    public int skillLevel;

public HardSkillInfo(SkillID id, string describeText, float buffValue, Sprite skillIcon,
    int skillLevel,string skillName)
{
    this.id = id;
    this.skillIcon = skillIcon;
    this.buffValue = buffValue;
        this.skillLevel = skillLevel;
        this.describeText = describeText;
        this.skillName = skillName;
}

public object Clone()
{
    return new HardSkillInfo(id, describeText, buffValue, skillIcon, skillLevel,skillName);
}
}
