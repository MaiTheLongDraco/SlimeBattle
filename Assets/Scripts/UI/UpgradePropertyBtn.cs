using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePropertyBtn : MonoBehaviour
{
    public Text CurrencyCost;
    public Image CurrencyIcon;
    public Image PropertiesIcon;
    public Text slimePropertyName;
    public Text slimePropertyValue;
    public void SetCurrencyCostTxt(string set)
    {
        CurrencyCost.text = set;
    }
    public void SetSlimePropertyName(string set)
    {
        slimePropertyName.text = set;
    }
    public void SetSlimePropertyValue(string set)
    {
        slimePropertyValue.text = set;
    }
    public void SetCurrencyIcon(Sprite newSprite)
    {
        CurrencyIcon.sprite = newSprite;
    }
    public void SetPropertiesIcon(Sprite newSprite)
    {
        PropertiesIcon.sprite = newSprite;
    }
}
