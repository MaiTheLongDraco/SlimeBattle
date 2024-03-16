using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PropertiesTemplate", menuName = "PropertiesTemplate/UpgradeDataInfo")]
public class PropertiesTemplate : ScriptableObject
{
    public SkillType skillType;
    public string tabName;
    public List<UpgradeDataInfo> ListInfo = new();
    public PropertiesTemplate(SkillType skillType,string tabName, List<UpgradeDataInfo> subButtonInfos)
    {
        this.skillType = skillType;
        this.tabName = tabName;
        ListInfo = subButtonInfos;
    }
    public PropertiesTemplate Clone()
    {
        return new PropertiesTemplate(skillType,tabName, ListInfo);
    }
}
