using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HardSkillTemplate", menuName = "Template/HardSkillTemplate")]
public class HardSkillTemplate : ScriptableObject
{
    public List<HardSkillInfo> ListInfo = new();

    public HardSkillTemplate(List<HardSkillInfo> subButtonInfos)
    {
        ListInfo = subButtonInfos;
    }

    public HardSkillTemplate Clone()
    {
        var tempList = new List<HardSkillInfo>();
        foreach (var info in ListInfo) tempList.Add(info.Clone() as HardSkillInfo);
        return new HardSkillTemplate(tempList);
    }
}