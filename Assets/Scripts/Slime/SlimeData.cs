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

	public object Clone()
	{
        return new SlimeData(_slimeATK, _slimeDF, _slimeUti);
	}
}
