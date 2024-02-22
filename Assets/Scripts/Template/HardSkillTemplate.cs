using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HardSkillTemplate", menuName = "Template/HardSkillTemplate")]
public class HardSkillTemplate : ScriptableObject
{
    public List<HardSkillInfo> ListInfo = new();
    public HardSkillTemplate( List<HardSkillInfo> subButtonInfos)
    {
        ListInfo = subButtonInfos;
    }
    public HardSkillTemplate Clone()
    {
        return new HardSkillTemplate( ListInfo);
    }
}
