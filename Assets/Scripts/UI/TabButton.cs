using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    [SerializeField] private TabInfo tabInfo;
    [SerializeField] private List<SubButton> subButtons;

    public TabInfo TabInfo
    {
        get => tabInfo;
        set => tabInfo = value;
    }

    public void SpecifyLoadSubButton()
    {
        print($" tab type {TabInfo}");
        foreach (var sub in subButtons)
            if (sub.SubTabType == TabInfo)
                sub.gameObject.SetActive(true);
            else
                sub.gameObject.SetActive(false);
    }
}

public enum TabInfo
{
    ATTACK,
    DEFENCE,
    UTILITY
}