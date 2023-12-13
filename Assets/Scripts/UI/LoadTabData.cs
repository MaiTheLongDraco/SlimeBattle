using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTabData : MonoBehaviour
{
    [SerializeField] private List<Button> tabButtons;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void HandleLoadTabData()
    {
        foreach (var tab in tabButtons)
        {
            var tabType = GetTabType(tab);
            switch (tabType)
            {
                case TabInfo.ATTACK:
                {
                    break;
                }
                case TabInfo.DEFENCE: break;
                case TabInfo.UTILITY: break;
            }
        }
    }

    private TabInfo GetTabType(Button button)
    {
        return button.GetComponent<TabButton>().TabInfo;
    }
}