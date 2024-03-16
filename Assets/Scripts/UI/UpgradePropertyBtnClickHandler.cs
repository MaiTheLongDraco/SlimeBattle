using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradePropertyBtnClickHandler : MonoBehaviour
{
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private int id;
    [SerializeField] private float increaseValue;
    [SerializeField] private SlimeData slimeData;
    [SerializeField] private SkillType currentSkillType;
    [SerializeField] private UpgradePropertyBtn upgradePropertyBtn;
    private const float UpgradeRate = 0.0625f;
	private void Start()
	{
        upgradePropertyBtn = GetComponent<UpgradePropertyBtn>();
        AddListener(UpgradeSlimeValue);
	}
	public void SetID(int set)
	{
        id = set;
	}
    public void SetSkillType(SkillType set)
    {
        currentSkillType = set;
    }
    public void SetSlimeData(SlimeData data)
    {
        slimeData = data;
    }
    public void UpgradeSlimeValue()
	{
        switch(currentSkillType)
		{
            case SkillType.ATTACK: { 
                    switch(id)
					{
                        case 0: { increaseValue=slimeData.SlimeATK.AttackDamage * UpgradeRate / 2; slimeData.IncreaseAttackDamage(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.AttackDamage,2).ToString()); break; }
                        case 1: { increaseValue = slimeData.SlimeATK.AttackSpeed * UpgradeRate / 2; slimeData.IncreaseAttackSpeed(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.AttackSpeed,2).ToString()); break; }
                        case 2: { increaseValue = slimeData.SlimeATK.AttackRange * UpgradeRate / 2; slimeData.IncreaseAttackRange(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.AttackRange,2).ToString()); break; }
                        case 3: { increaseValue = slimeData.SlimeATK.RangeDamageBonus * UpgradeRate / 2; slimeData.IncreaseAttackDamageBonus(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeATK.RangeDamageBonus,2).ToString()); break; }
                    }
                    break; }
            case SkillType.DEFEND: {
                    switch (id)
                    {
                        case 0: { increaseValue = slimeData.SlimeDF.Heath * UpgradeRate / 2; slimeData.IncreaseHeath(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.Heath,2).ToString()); break; }
                        case 1: { increaseValue = slimeData.SlimeDF.HealthRegen * UpgradeRate / 2; slimeData.IncreaseHeathRegen(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.HealthRegen,2).ToString()); break; }
                        case 2: { increaseValue = slimeData.SlimeDF.Armor * UpgradeRate / 2; slimeData.IncreaseArmor(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.Armor,2).ToString()); break; }
                        case 3: { increaseValue = slimeData.SlimeDF.BlockDamage * UpgradeRate / 2; slimeData.IncreaseBlockDamage(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeDF.BlockDamage,2).ToString()); break; }
                    }
                    break; }
            case SkillType.UTILITY: {
                    switch (id)
                    {
                        case 0: { increaseValue = slimeData.SlimeUti.SilverPerWave * UpgradeRate / 2; slimeData.IncreaseSilverPerWave(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.SilverPerWave,2).ToString()); break; }
                        case 1: { increaseValue = slimeData.SlimeUti.SilverBonus * UpgradeRate / 2; slimeData.IncreaseSilverBonus(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.SilverBonus,2).ToString()); break; }
                        case 2: { increaseValue = slimeData.SlimeUti.GoldBonus * UpgradeRate / 2; slimeData.IncreaseGoldBonus(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.GoldBonus,2).ToString()); break; }
                        case 3: { increaseValue = slimeData.SlimeUti.GoldPerWave * UpgradeRate / 2; slimeData.IncreaseGoldPerWave(increaseValue); upgradePropertyBtn.SetSlimePropertyValue(Math.Round(slimeData.SlimeUti.GoldPerWave,2).ToString()); break; }
                    }
                    break; }
        }
    } 
    public void SetIncreaseValue(float set)
	{
        increaseValue = set;
	}
    public void AddListener(UnityAction callBacl)
	{
        upgradeBtn.onClick.AddListener(callBacl);
	}
    public void InvokeId(UnityAction<int,float> callBack)
	{
        callBack?.Invoke(id, increaseValue);
	}        
    
}
