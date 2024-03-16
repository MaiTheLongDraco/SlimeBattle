using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UpgradeDataInfo : ICloneable
{
    public float CurrencyCost;
    public Sprite CurrencyIcon;
    public Sprite PropertiesIcon;
    public int id;
    public string slimePropertyName;
    public float slimePropertyValue;

    public UpgradeDataInfo(int id, string slimePropertyName, float slimePropertyValue, Sprite currencyIcon, Sprite PropertiesIcon,
        float currencyCost)
    {
        this.id = id;
        this.PropertiesIcon = PropertiesIcon;
        this.slimePropertyName = slimePropertyName;
        this.slimePropertyValue = slimePropertyValue;
        CurrencyIcon = currencyIcon;
        CurrencyCost = currencyCost;
    }

    public object Clone()
    {
        return new UpgradeDataInfo(id, slimePropertyName, slimePropertyValue, CurrencyIcon, PropertiesIcon,CurrencyCost);
    }

    public void IncreaseCurrencyCost(float addingValue)
    {
        CurrencyCost += addingValue;
    }

    public void IncreasePropertyValue(float addingValue)
    {
        slimePropertyValue += addingValue;
    }
}
