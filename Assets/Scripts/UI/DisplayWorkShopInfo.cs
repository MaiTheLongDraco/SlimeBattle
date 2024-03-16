using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWorkShopInfo : MonoBehaviour
{
	[SerializeField] private Text tabName;
	[SerializeField] private SkillType currentSkillType;
	[SerializeField] private GameObject upgradePPBtn;
	[SerializeField] private List<UpgradePropertyBtn> ListUpgradePPBtn;
	[SerializeField] private List<UpgradePropertyBtnClickHandler> upgradePropertyBtnClickHandlers;
	[SerializeField] private SlimeData slimeData;
	[SerializeField] private PropertiesTemplate attackTemplate;
	[SerializeField] private PropertiesTemplate defendTemplate;
	[SerializeField] private PropertiesTemplate utilityTemplate;
	// Start is called before the first frame update
	void Start()
	{
		GenerateUpgradeBtn();
		LoadATKData();
	}
	private void GenerateUpgradeBtn()
	{
		for (int i = 0; i < 4; i++)
		{
			var btn = Instantiate(upgradePPBtn, transform);
			ListUpgradePPBtn.Add(btn.GetComponent<UpgradePropertyBtn>());
			upgradePropertyBtnClickHandlers.Add(btn.GetComponent<UpgradePropertyBtnClickHandler>());

		}
	}
	private void LoadData(PropertiesTemplate template)
	{
		tabName.text = template.tabName;
		for (int i = 0; i < ListUpgradePPBtn.Count; i++)
		{
			ListUpgradePPBtn[i].SetCurrencyCostTxt(template.ListInfo[i].CurrencyCost.ToString());
			ListUpgradePPBtn[i].SetCurrencyIcon(template.ListInfo[i].CurrencyIcon);
			ListUpgradePPBtn[i].SetPropertiesIcon(template.ListInfo[i].PropertiesIcon);
			upgradePropertyBtnClickHandlers[i].SetID(i);
			upgradePropertyBtnClickHandlers[i].SetSkillType(template.skillType);
			upgradePropertyBtnClickHandlers[i].SetSlimeData(slimeData);
			switch (template.skillType)
			{
				case SkillType.ATTACK:
					{
						if (i == 0)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.AttackDamage, 2) .ToString()) ;
						}
						else if (i == 1)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.AttackSpeed, 2).ToString());

						}
						else if (i == 2)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.AttackRange, 2).ToString());
						}
						else if (i == 3)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.RangeDamageBonus, 2).ToString());
						}
						break;
					}
				case SkillType.DEFEND:
					{

						if (i == 0)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.Heath, 2).ToString());
						}
						else if (i == 1)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.HealthRegen, 2).ToString());

						}
						else if (i == 2)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.Armor, 2).ToString());
						}
						else if (i == 3)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.BlockDamage, 2).ToString());
						}
						break;
					}
				case SkillType.UTILITY:
					{
						if (i == 0)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.SilverPerWave, 2).ToString());
						}
						else if (i == 1)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.SilverBonus, 2).ToString());

						}
						else if (i == 2)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.GoldBonus, 2).ToString());
						}
						else if (i == 3)
						{
							ListUpgradePPBtn[i].SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.GoldPerWave, 2).ToString());
						}
						break;
					}
			}
			ListUpgradePPBtn[i].SetSlimePropertyName(template.ListInfo[i].slimePropertyName);

		}
		currentSkillType = template.skillType;
	}
	private void HandleUpgradeData(int id, float increaseValue)
	{
		switch (currentSkillType)
		{
			case SkillType.ATTACK:
				{
					switch (id)
					{
						case 0: { slimeData.IncreaseAttackDamage(increaseValue); break; }
						case 1: { slimeData.IncreaseAttackSpeed(increaseValue); break; }
						case 2: { slimeData.IncreaseAttackRange(increaseValue); break; }
						case 3: { slimeData.IncreaseAttackDamageBonus(increaseValue); break; }
					}
					break;
				}
			case SkillType.DEFEND:
				{
					switch (id)
					{
						case 0: { slimeData.IncreaseHeath(increaseValue); break; }
						case 1: { slimeData.IncreaseHeathRegen(increaseValue); break; }
						case 2: { slimeData.IncreaseArmor(increaseValue); break; }
						case 3: { slimeData.IncreaseBlockDamage(increaseValue); break; }
					}
					break;
				}
			case SkillType.UTILITY:
				{
					switch (id)
					{
						case 0: { slimeData.IncreaseSilverPerWave(increaseValue); break; }
						case 1: { slimeData.IncreaseSilverBonus(increaseValue); break; }
						case 2: { slimeData.IncreaseGoldBonus(increaseValue); break; }
						case 3: { slimeData.IncreaseGoldPerWave(increaseValue); break; }
					}
					break;
				}
		}

	}
	public void LoadATKData()
	{
		LoadData(attackTemplate);
	}
	public void LoadDFData()
	{
		LoadData(defendTemplate);
	}
	public void LoadUtilityData()
	{
		LoadData(utilityTemplate);
	}

}
