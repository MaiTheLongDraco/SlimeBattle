using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private Attack _slimeATK;
    [SerializeField] private Defense _slimeDF;
    [SerializeField] private Utility _slimeUti;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,_slimeATK.AttackRange);
    }
   
}
[Serializable]
public class Attack
{
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float RangeDamageBonus;

    public void SetAttackDamage(float set)
    {
        AttackDamage = set;
    }
    public void SetAttackRange(float set)
    {
        AttackRange = set;
    }
    public void SetAttackSpeed(float set)
    {
        AttackSpeed = set;
    }
    public void SetAttackDamageBonus(float set)
    {
        RangeDamageBonus = set;
    }
}
[Serializable]
public class Defense
{
    public float Heath;
    public float Armor;
    public float HealthRegen;
    public float BlockDamage;
    public void SetHeath(float set)
    {
        Heath = set;
    }
    public void SetArmor(float set)
    {
        Armor = set;
    }
    public void SetHeathRegen(float set)
    {
        HealthRegen = set;
    }
    public void SetBlockDamage(float set)
    {
        BlockDamage = set;
    }
}
[Serializable]
public class Utility
{
    public float SilverPerWave;
    public float GoldBonus;
    public float SilverBonus;
    public float GoldPerWave;
    public void SetSilverPerWave(float set)
    {
        SilverPerWave = set;
    }
    public void SetGoldBonus(float set)
    {
        GoldBonus = set;
    }
    public void SetSilverBonus(float set)
    {
        SilverBonus = set;
    }
    public void SetGoldPerWave(float set)
    {
        GoldPerWave = set;
    }
}

[Serializable]
public class Ultimate
{
   
}