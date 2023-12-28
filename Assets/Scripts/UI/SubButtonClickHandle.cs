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
    [SerializeField] private SlimeData rootDataClone;

    // Start is called before the first frame update
    private void Start()
    {
        slimeController = SlimeController.Instance;
        rootDataClone = slimeController.RootDataClone;
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
        SpecifySlimeValue(parentData.slimePropertyValue);
        displayInfo.SetSlimePropertyValue(parentData.slimePropertyValue.ToString());
    }

    private void SpecifySlimeValue(float set)
    {
        switch (parentInfo.GetTabType())
        {
            case TabInfo.ATTACK:
				{
                    SetNewAttackValue(set);
				}
				break;
            case TabInfo.DEFENCE:
            {
					SetNewDefenseValue(set);
			}
                break;
            case TabInfo.UTILITY:
            {
					SetNewUtilitiesValue(set);
			}
                break;
        }
    }

	private void SetNewAttackValue(float set)
	{
		switch (ButtonID)
		{
			case 0:
				{
					rootDataClone.SetAttackDamage(set);
					print($"new atk dame value {rootDataClone.SlimeATK.AttackDamage}");
				}
				break;
			case 1:
				{
					rootDataClone.SetAttackSpeed(set);
					print($"new atk speed value {rootDataClone.SlimeATK.AttackSpeed}");

				}
				break;
			case 2:
				{
					rootDataClone.SetAttackRange(set);
					print($"new atk range value {rootDataClone.SlimeATK.AttackRange}");

				}
				break;
			case 3:
				{
					rootDataClone.SetAttackDamageBonus(set);
					print($"new atk range dam bonus value {rootDataClone.SlimeATK.RangeDamageBonus}");
				}
				break;
		}
	}
	private void SetNewDefenseValue(float set)
	{
		switch (ButtonID)
		{
			case 0:
				{
					rootDataClone.SetHeath(set);
					print($"new df heath value {rootDataClone.SlimeDF.Heath}");
				}
				break;
			case 1:
				{
					rootDataClone.SetHeathRegen(set);
					print($"new df heath regen value {rootDataClone.SlimeDF.HealthRegen}");

				}
				break;
			case 2:
				{
					rootDataClone.SetArmor(set);
					print($"new df armor range value {rootDataClone.SlimeDF.Armor}");

				}
				break;
			case 3:
				{
					rootDataClone.SetBlockDamage(set);
					print($"new df block dam value {rootDataClone.SlimeDF.BlockDamage}");
				}
				break;
		}
	}
	private void SetNewUtilitiesValue(float set)
	{
		switch (ButtonID)
		{
			case 0:
				{
					rootDataClone.SetSilverPerWave(set);
					print($"new uti silver per wave value {rootDataClone.SlimeUti.SilverPerWave}");
				}
				break;
			case 1:
				{
					rootDataClone.SetSilverBonus(set);
					print($"new uti silver bonus value {rootDataClone.SlimeUti.SilverBonus}");

				}
				break;
			case 2:
				{
					rootDataClone.SetGoldBonus(set);
					print($"new uti gold bonus value {rootDataClone.SlimeUti.GoldBonus}");

				}
				break;
			case 3:
				{
					rootDataClone.SetGoldPerWave(set);
					print($"new uti gold per wave value {rootDataClone.SlimeUti.GoldPerWave}");
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