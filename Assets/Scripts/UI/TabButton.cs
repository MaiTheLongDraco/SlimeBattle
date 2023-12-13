using UnityEngine;

public class TabButton : MonoBehaviour
{
    [SerializeField] private TabInfo tabInfo;

    [SerializeField] private GameObject gridInfo;

    public TabInfo TabInfo
    {
        get => tabInfo;
        set => tabInfo = value;
    }

    public void SetActiveSubButton(bool set)
    {
        gridInfo.SetActive(set);
    }
}

public enum TabInfo
{
    ATTACK,
    DEFENCE,
    UTILITY
}