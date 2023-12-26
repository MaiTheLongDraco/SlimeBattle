using System;
using UnityEngine;
using UnityEngine.UI;

public class SubButtonClickHandle : MonoBehaviour
{
    private const float upgradeRate = 0.0625f;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private DisplaySubBtnInfo parentInfo;
    [field: SerializeField] public int ButtonID { get; private set; }
    [SerializeField] private SubButtonInfo parentData;
    [SerializeField] private SubButtonInfoHandle displayInfo;

    // Start is called before the first frame update
    private void Start()
    {
        upgradeButton = GetComponentInChildren<Button>();
        parentInfo = GetComponentInParent<DisplaySubBtnInfo>();
        displayInfo = GetComponent<SubButtonInfoHandle>();
        upgradeButton.onClick.AddListener(UpgradeSlimeValue);
    }

    public void UpgradeSlimeValue()
    {
        var increasingAmount = parentData.slimePropertyValue * upgradeRate / 2;
        parentData.slimePropertyValue += increasingAmount;
        parentData.slimePropertyValue = (float)Math.Round(parentData.slimePropertyValue, 2);
        displayInfo.SetSlimePropertyValue(parentData.slimePropertyValue.ToString());
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