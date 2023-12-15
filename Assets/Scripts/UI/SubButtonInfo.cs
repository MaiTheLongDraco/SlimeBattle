using System;
using UnityEngine;

[Serializable]
public class SubButtonInfo : ICloneable
{
    public float CurrencyCost;
    public Sprite CurrencyIcon;
    public int id;
    public string slimePropertyName;
    public float slimePropertyValue;

    public SubButtonInfo(int id, string slimePropertyName, float slimePropertyValue, Sprite currencyIcon,
        float currencyCost)
    {
        this.id = id;
        this.slimePropertyName = slimePropertyName;
        this.slimePropertyValue = slimePropertyValue;
        CurrencyIcon = currencyIcon;
        CurrencyCost = currencyCost;
    }

    public object Clone()
    {
        return new SubButtonInfo(id, slimePropertyName, slimePropertyValue, CurrencyIcon, CurrencyCost);
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