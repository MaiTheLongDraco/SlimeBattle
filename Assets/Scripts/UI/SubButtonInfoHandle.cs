using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubButtonInfoHandle : MonoBehaviour
{
    public Text CurrencyCost;
    public Image CurrencyIcon;
    public Text slimePropertyName;
    public Text slimePropertyValue;
    public void  SetCurrencyCostTxt(string set)
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
}
