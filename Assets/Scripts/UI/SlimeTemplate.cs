using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SUbButtonTemplate", menuName = "SUbButtonTemplate/SubButtonInfo")]
public class SlimeTemplate : ScriptableObject
{
    public SkillType skillType;
    public List<SubButtonInfo> ListInfo = new();
    public SlimeTemplate (SkillType skillType, List<SubButtonInfo> subButtonInfos)
	{
        this.skillType = skillType;
        ListInfo = subButtonInfos;
	}
    public SlimeTemplate Clone()
	{
        return new SlimeTemplate(skillType, ListInfo);
	}
}
public enum SkillType
{
    ATTACK,
    DEFEND,
    UTILITY
}
