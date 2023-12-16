using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SUbButtonTemplate", menuName = "SUbButtonTemplate/SubButtonInfo")]
public class SlimeTemplate : ScriptableObject
{
    public SkillType skillType;
    public List<SubButtonInfo> ListInfo = new();
}
public enum SkillType
{
    ATTACK,
    DEFEND,
    UTILITY
}
