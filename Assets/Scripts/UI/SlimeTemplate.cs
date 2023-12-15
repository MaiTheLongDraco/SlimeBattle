using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SUbButtonTemplate", menuName = "SUbButtonTemplate/SubButtonInfo")]
public class SlimeTemplate : ScriptableObject
{
    public List<SubButtonInfo> ListInfo = new();
}