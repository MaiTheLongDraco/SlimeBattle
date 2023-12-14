using System;
using UnityEngine;

public class SubButtonInfo : ICloneable
{
    [SerializeField] private readonly float CurrencyCost;
    [SerializeField] private readonly Sprite CurrencyIcon;
    [SerializeField] private readonly int id;
    [SerializeField] private readonly string slimePropertyName;
    [SerializeField] private readonly float slimePropertyValue;

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
}