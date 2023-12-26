using System;
using UnityEngine;
using UnityEngine.UI;

public class SubButtonClickHandle : MonoBehaviour
{
    private const float UpgradeRate = 0.0625f;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private DisplaySubBtnInfo parentInfo;
    [field: SerializeField] public int ButtonID { get; private set; }
    [SerializeField] private SubButtonInfo parentData;
    [SerializeField] private SubButtonInfoHandle displayInfo;
    [SerializeField] private SlimeController slimeController;

    // Start is called before the first frame update
    private void Start()
    {
        slimeController = SlimeController.Instance;
        upgradeButton = GetComponentInChildren<Button>();
        parentInfo = GetComponentInParent<DisplaySubBtnInfo>();
        displayInfo = GetComponent<SubButtonInfoHandle>();
        upgradeButton.onClick.AddListener(UpgradeSlimeValue);
    }

    public void UpgradeSlimeValue()
    {
        var increasingAmount = parentData.slimePropertyValue * UpgradeRate / 2;
        parentData.slimePropertyValue += increasingAmount;
        parentData.slimePropertyValue = (float)Math.Round(parentData.slimePropertyValue, 2);
        displayInfo.SetSlimePropertyValue(parentData.slimePropertyValue.ToString());
    }

    private void SpecifySlimeValue()
    {
        switch (parentInfo.GetTabType())
        {
            case TabInfo.ATTACK:
            {
                switch (ButtonID)
                {
                    case 0:
                    {
                    }
                        break;
                }
            }
                break;
            case TabInfo.DEFENCE:
            {
            }
                break;
            case TabInfo.UTILITY:
            {
            }
                break;
        }
    }

    public void InitParentData(SubButtonInfo set)
    {
        parentData = set;
        print(
            $"sub button info {parentData.slimePropertyValue} -- {parentData.slimePropertyName} --  {parentData.CurrencyCost} -- id {parentData.id}");
    }

    public void SetButtonId(int set)
    {
        ButtonID = set;
    }
}