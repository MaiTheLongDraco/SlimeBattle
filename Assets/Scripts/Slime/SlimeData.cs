using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SlimeData", menuName = "SlimeData/DataField")]
public class SlimeData : ScriptableObject, ICloneable
{
    [SerializeField] private Attack _slimeATK;
    [SerializeField] private Defense _slimeDF;
    [SerializeField] private Utility _slimeUti;
    public SlimeData(Attack atk, Defense df,Utility uti)
	{
        _slimeATK = atk;
        _slimeDF = df;
        _slimeUti = uti;
	}
	public Attack SlimeATK { get => _slimeATK; set => _slimeATK = value; }
	public Defense SlimeDF { get => _slimeDF; set => _slimeDF = value; }
	public Utility SlimeUti { get => _slimeUti; set => _slimeUti = value; }
    public void SetATK(Attack set)
	{
        _slimeATK = set;
	}
    public void SetDF(Defense set)
	{
        _slimeDF = set;
	}
    public void SetUti(Utility set)
	{
        _slimeUti = set;
	}
	#region SetInfo
	public void SetAttackDamage(float set)
    {
        SlimeATK.AttackDamage = set;
    }

    public void SetAttackRange(float set)
    {
        SlimeATK.AttackRange = set;
    }

    public void SetAttackSpeed(float set)
    {
        SlimeATK.AttackSpeed = set;
    }

    public void SetAttackDamageBonus(float set)
    {
        SlimeATK.RangeDamageBonus = set;
    }
    public void SetHeath(float set)
    {
        SlimeDF.Heath = set;
    }

    public void SetArmor(float set)
    {
        SlimeDF.Armor = set;
    }

    public void SetHeathRegen(float set)
    {
        SlimeDF.HealthRegen = set;
    }

    public void SetBlockDamage(float set)
    {
        SlimeDF.BlockDamage = set;
    }
    public void SetSilverPerWave(float set)
    {
        SlimeUti.SilverPerWave = set;
    }

    public void SetGoldBonus(float set)
    {
        SlimeUti.GoldBonus = set;
    }

    public void SetSilverBonus(float set)
    {
        SlimeUti.SilverBonus = set;
    }

    public void SetGoldPerWave(float set)
    {
        SlimeUti.GoldPerWave = set;
    }
    #endregion
    #region IncreaseValue
    public void IncreaseAttackDamage(float set)
    {
        SlimeATK.AttackDamage += set;
    }

    public void IncreaseAttackRange(float set)
    {
        SlimeATK.AttackRange += set;
    }

    public void IncreaseAttackSpeed(float set)
    {
        SlimeATK.AttackSpeed += set;
    }

    public void IncreaseAttackDamageBonus(float set)
    {
        SlimeATK.RangeDamageBonus += set;
    }
    public void IncreaseHeath(float set)
    {
        SlimeDF.Heath += set;
    }

    public void IncreaseArmor(float set)
    {
        SlimeDF.Armor += set;
    }

    public void IncreaseHeathRegen(float set)
    {
        SlimeDF.HealthRegen += set;
    }

    public void IncreaseBlockDamage(float set)
    {
        SlimeDF.BlockDamage += set;
    }
    public void IncreaseSilverPerWave(float set)
    {
        SlimeUti.SilverPerWave += set;
    }

    public void IncreaseGoldBonus(float set)
    {
        SlimeUti.GoldBonus += set;
    }

    public void IncreaseSilverBonus(float set)
    {
        SlimeUti.SilverBonus += set;
    }

    public void IncreaseGoldPerWave(float set)
    {
        SlimeUti.GoldPerWave += set;
    }
    #endregion
    public object Clone()
	{
        return new SlimeData(_slimeATK.Clone()as Attack, _slimeDF.Clone() as Defense, _slimeUti.Clone() as Utility);
	}
}
