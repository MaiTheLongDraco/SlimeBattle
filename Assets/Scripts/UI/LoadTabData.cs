using System.Collections.Generic;
using UnityEngine;

public class LoadTabData : MonoBehaviour
{
    [SerializeField] private List<TabButton> tabButtons;

    [SerializeField] private List<SubButton> subButtons;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void HandleLoadTabData()
    {
        foreach (var tab in tabButtons)
            switch (tab.TabInfo)
            {
                case TabInfo.ATTACK:
                {
                    SpecifyLoadSubButton(tab);
                    break;
                }
                case TabInfo.DEFENCE:
                {
                    SpecifyLoadSubButton(tab);
                    break;
                }
                case TabInfo.UTILITY:
                {
                    SpecifyLoadSubButton(tab);
                    break;
                }
            }
    }

    private void SpecifyLoadSubButton(TabButton tab)
    {
        foreach (var sub in subButtons)
            if (sub.SubTabType == tab.TabInfo)
                sub.gameObject.SetActive(true);
            else
                sub.gameObject.SetActive(false);
    }
}